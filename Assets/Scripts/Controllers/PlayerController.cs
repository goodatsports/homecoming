using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public enum Directions
{
	Right,
	Up,
	Left,
	Down
}

public class PlayerController : MonoBehaviour
{
	public InputMaster Controls;
	public CinemachineVirtualCameraBase CM;
	public CinemachineTransposer CM_Transpose;
	public CinemachineComposer CM_Composer;
	public InputAction MovementAction, 
		UseAction, SwingAction, RopeAction, ShowPointerAction, InventoryAction, 
		DialogCursorAction, DialogConfirmAction;
	public PointerController Pointer;
	public InventoryController Inventory;
	

	private MapController Map;
	private Tilemap GroundMap, ObstacleMap;
	private Vector3 ActionQuadrant;
	private bool CanMove = true;
	private Vector3 OFFSET_VECTOR = new Vector3(0.5f, 0.5f, 0);
	private Vector3[] DirectionVectors = { Vector3.right, Vector3.up, Vector3.left, Vector3.down };
	private const float MOVE_DECAY = .125f;


    #region Initialization
    void Awake()
    {
		Controls = new InputMaster();
		Pointer = GameObject.Find("Pointer").GetComponent<PointerController>();
		Inventory = GameObject.Find("Player Inventory").GetComponent<InventoryController>();


		// Input action mapping
		MovementAction = Controls.Player.Movement;
		UseAction = Controls.Player.Use;
		SwingAction = Controls.Player.Swing;
		RopeAction = Controls.Player.ThrowRope;
		ShowPointerAction = Controls.Player.ShowPointer;
		InventoryAction = Controls.Player.ToggleInventory;

		DialogCursorAction = Controls.Dialog.MoveCursor;
		DialogConfirmAction = Controls.Dialog.Confirm;

		ShowPointerAction.started += ctx => { Pointer.Show(); };
		ShowPointerAction.canceled += ctx => { Pointer.Hide(); };
		UseAction.started += ctx => { Interact(); };
		SwingAction.started += ctx => { SwingAxe(); };
		RopeAction.started += ctx => { ThrowRope(); };
		InventoryAction.started += ctx => { Inventory.Toggle(); };
		
		
		//Events subscriptions
		GameEvents.current.onNPCDialogStart += Busy;
        GameEvents.current.onNPCDialogEnd += Ready;
		GameEvents.current.onInventoryOpen += Busy;
		GameEvents.current.onInventoryClose += Ready;
		GameEvents.current.onWaitForDialogChoice += WaitingOnDialogChoice;
		GameEvents.current.onDialogChoiceMade += DialogChoiceMade;
		GameEvents.current.onShoppingStart += Shopping;


	}

    private void OnEnable()
    {
		Controls.Player.Enable();
    }
	void Start() {
		Map = GameObject.Find("Map").GetComponentInChildren(typeof(MapController)) as MapController;
		if (Map.ObstacleMap == null) Debug.Log("No obstacle map found!");

		GroundMap = Map.GroundMap;
		ObstacleMap = Map.ObstacleMap;
		Physics2D.queriesStartInColliders = false;
	}

	void LateUpdate() {
		Vector2 directionInput = MovementAction.ReadValue<Vector2>();
		ActionQuadrant = Pointer.GetMouseQuadrant();
		if (directionInput != Vector2.zero && CanMove) {
			StartCoroutine(Move(directionInput));
		}
	}
	#endregion


	#region States

	// Various control states for player which are controlled via GameEvents messages
	void Busy() {
		StopAllCoroutines();
		MovementAction.Disable();
	}
	void Ready() {
		StopAllCoroutines();
		Controls.Player.Enable();
		MovementAction.Enable();
		CanMove = true;
	}
	void WaitingOnDialogChoice() { 
		Controls.Player.Disable();
		Controls.Dialog.Enable();

    }
	void DialogChoiceMade(Response sentence) {
		Controls.Player.Use.Enable();
		Controls.Dialog.Disable();

	}
	void Shopping() {
		Controls.Player.Disable();
		Controls.Dialog.Disable();
		
    }
    #endregion

    public void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.name == "ShopEntrance" && CM.Follow == transform)
		{

			Transform target = GameObject.Find("Shop LookAt Target").GetComponent<Transform>();
			Debug.Log("shop transform: " + target.name);
			CM.Follow = target;
		} 
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
		CM.Follow = transform;
		CM.LookAt = transform;
	
    }

	public IEnumerator Move(Vector2 inputVector)
	{
		MovePlayer(inputVector);
		CanMove = false;
		yield return new WaitForSeconds(MOVE_DECAY);
		CanMove = true;
	}

	public void Interact()
    {
		//Layer mask; only looking for hits on character layer.
		int characterLayer = 1 << 8;

		foreach (Vector3 dir in DirectionVectors) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, characterLayer);
			if (hit.collider) {
				Interactable target = hit.collider.GetComponent<Interactable>();
				if (target != null) {
					target.Interact();
				}
				return;
			}
		}
    }

	public void SwingAxe()
    {
		{
			var tileAddress = Map.WorldToCell(transform.position + ActionQuadrant);
			CustomTile tile = ObstacleMap.GetTile<TreeTile>(tileAddress);
			if (tile != null) Map.ChopTree(tileAddress);
			
		}
    }

	public void ThrowRope() {
		int ropeLength = 3;
		int i = 1;
		while (i <= ropeLength) { 
			Vector3Int targetTile = Map.WorldToCell(transform.position + ActionQuadrant * i);
			if (Map.HasRopeable(targetTile)) {
				print("has ropeable!");
				MoveSprite(targetTile);
				return;
			}

			if (Map.HasObstacle(targetTile)) {
				print($"obstacle found at {targetTile}: {Map.ObstacleMap.GetTile(targetTile)}");
				return;
            }

			Map.PaintRope(targetTile, ActionQuadrant);
			i++;
        }
    }

	void MovePlayer(Vector2 input)
	{
		// Don't look for collisions on Trigger layer
		int layerMask = ~(1 << 10);
		Vector3 inputVector = Mathf.Abs(input.x) > Mathf.Abs(input.y) ? new Vector3(input.x, 0) : new Vector3(0, input.y);
		Vector3Int destinationTile = Map.WorldToCell(transform.position + inputVector);
		RaycastHit2D hit = Physics2D.Raycast(transform.position, inputVector, 1f, layerMask);

		if (hit.collider == null)
		{
			Vector3Int oldTile = Map.WorldToCell(transform.position);
			transform.position = GroundMap.CellToWorld(destinationTile) + OFFSET_VECTOR;

			//Setting collision tile at new address and removing old one.
			ObstacleMap.SetTile(destinationTile, TileBase.CreateInstance<CustomTile>());
			TileBase.Destroy(ObstacleMap.GetTile<CustomTile>(oldTile));

		}
	}

	// Need to refactor; basically moves the player while skipping usual movement flow; to be used when Rope hits a 
	// usable tile and "teleports" player
	void MoveSprite(Vector3Int destination) {
		Vector3Int oldTile = Map.WorldToCell(transform.position);
		transform.position = GroundMap.CellToWorld(destination) + OFFSET_VECTOR;

		//Setting collision tile at new address and removing old one.
		ObstacleMap.SetTile(destination, TileBase.CreateInstance<CustomTile>());
		TileBase.Destroy(ObstacleMap.GetTile<CustomTile>(oldTile));

	}
}