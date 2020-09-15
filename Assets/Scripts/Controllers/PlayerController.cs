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
	public InputAction MovementAction, UseAction, SwingAction, ShowPointerAction, InventoryAction;
	public PointerController Pointer;
	public const float MOVE_DECAY = 0.125f;

	private int Money;
	public Item[] Inventory;

	private MapController Map;
	private Tilemap GroundMap, ObstacleMap;
	private Vector3 ActionQuadrant;
	private bool CanMove = true;
	private Vector3 OFFSET_VECTOR = new Vector3(0.5f, 0.5f, 0);


    void Awake()
    {
		Controls = new InputMaster();
		Pointer = GameObject.Find("Pointer").GetComponent<PointerController>();

		MovementAction = Controls.Player.Movement;
		UseAction = Controls.Player.Use;
		SwingAction = Controls.Player.Swing;
		ShowPointerAction = Controls.Player.ShowPointer;
		InventoryAction = Controls.Player.ToggleInventory;

		ShowPointerAction.started += ctx => { Pointer.Show(); };
		ShowPointerAction.canceled += ctx => { Pointer.Hide(); };
		UseAction.started += ctx => { Interact(); };
		SwingAction.started += ctx => { Swing(); };
		InventoryAction.started += ctx => { ToggleInventory(); };
		
		

		GameEvents.current.onNPCDialogStart += InConversation;
		GameEvents.current.onNPCDialogEnd += OutConversation;
	}

	private void OnEnable()
    {
		Controls.Enable();
    }

    void Start()
	{
		Map = GameObject.Find("Map").GetComponentInChildren(typeof(MapController)) as MapController;
		if (Map.ObstacleMap == null) Debug.Log("No obstacle map found!");

		GroundMap = Map.GroundMap;
		ObstacleMap = Map.ObstacleMap;
		Physics2D.queriesStartInColliders = false;

		Money = 0;
		Inventory = new Item[] {
			new Item("Barthe's Einhander", "it's big!", 30),
			new Item("eggs", "eggs", 1)
		};

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		Vector2 directionInput = MovementAction.ReadValue<Vector2>();
		ActionQuadrant = Pointer.GetMouseQuadrant();
		if (directionInput != Vector2.zero && CanMove)
        {
			StartCoroutine(Move(directionInput));
        }
   
	}

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

	public void InConversation() {
		print("In conversation");
		CanMove = false;
    }
	public void OutConversation() {
		print("out conversation");
		CanMove = true;
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
		RaycastHit2D hit = Physics2D.Raycast(transform.position, ActionQuadrant, 1f, characterLayer);
		if (hit.collider)
		{
			Interactable target = hit.collider.GetComponent<Interactable>();
			if (target != null) {
				target.Interact();
			}
			return;
		}
    }

	public void Swing()
    {
		{
			var tileAddress = Map.WorldToCell(transform.position + ActionQuadrant);
			CustomTile tile = ObstacleMap.GetTile<TreeTile>(tileAddress);
			if (tile != null) Map.ChopTree(tileAddress);
		}
    }

	public void ToggleInventory() {
		foreach (Item i in Inventory) {
			print(i.Name);
			print(i.Description);
			print(i.Value);
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
			transform.position = GroundMap.CellToWorld(destinationTile) + OFFSET_VECTOR;
		}
	}
}
