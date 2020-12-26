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
	public TargetController TargetUI;
	public Interactable Target;
	public InventoryController Inventory;
	public SpriteRenderer Sprite;

	public Item AxePrefab;
	public Item RopePrefab;
	public Sprite UnarmoredSprite;
	
	private MapController Map;
	private Tilemap GroundMap, ObstacleMap;

	private Vector3 ActionQuadrant;
	

	[SerializeField]
	public bool CanMove = true;
	public bool CanRope = true;
	private bool MoveActionHeld = false;

	private Vector3 OFFSET_VECTOR = new Vector3(0.5f, 0.5f, 0);
	private List<Vector3> DirectionVectors = new List<Vector3>() {
		Vector3.right, Vector3.up, Vector3.left, Vector3.down
	};

	private Vector2 DirectionInput;
	private const float MOVE_DECAY = .125f;
	private const float ROPE_DECAY = .075f;
	private int ROPE_LENGTH = 5;


    #region Initialization
    void Awake()
    {
		Controls = new InputMaster();
		Pointer = GameObject.Find("Pointer").GetComponent<PointerController>();
		Inventory = GameObject.Find("Player Inventory").GetComponent<InventoryController>();
		Sprite = gameObject.GetComponent<SpriteRenderer>();


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

		MovementAction.started += ctx => {
			MoveActionHeld = true;
		};

		MovementAction.canceled += ctx => {
			MoveActionHeld = false;
		};

		UseAction.started += ctx => { Interact(); };
		SwingAction.started += ctx => {  };

		RopeAction.started += ctx => {
			if (CanRope && Inventory.HasItem("Rope")) {
				UseAction.Disable();
				MovementAction.Disable();
				CanRope = false;
				ThrowRope();
			}
		};

		InventoryAction.started += ctx => { Inventory.Toggle(); };
		
		//Events subscriptions
		GameEvents.current.onNPCDialogStart += Busy;
        GameEvents.current.onNPCDialogEnd += Ready;
		GameEvents.current.onInventoryOpen += Busy;
		GameEvents.current.onInventoryClose += Ready;
		GameEvents.current.onWaitForDialogChoice += WaitingOnDialogChoice;
		GameEvents.current.onDialogChoiceMade += DialogChoiceMade;
		GameEvents.current.onShoppingStart += Shopping;
		GameEvents.current.onAxeTrade += TradeForAxe;
		GameEvents.current.onGharnamQuestComplete += GetRope;


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

	void Update() {
		DirectionInput = MovementAction.ReadValue<Vector2>();
		ActionQuadrant = Pointer.GetMouseQuadrant();

		// Look for targets when able to move / not currently doing an action
		if (MovementAction.enabled) {
			UpdateTarget();
		}

		if (MoveActionHeld && CanMove && DirectionInput != Vector2.zero) {
			StartCoroutine(Move(DirectionInput));

		}
	}

	void LateUpdate() {
		
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
		// Refactor: change to based on time since last move, or something more elegant than this
		CanMove = false;
		MovePlayer(inputVector);
		yield return new WaitForSeconds(MOVE_DECAY);
		CanMove = true;
	}

	public void GetRope() {
		Inventory.AddItem(RopePrefab);
    }

	public void TradeForAxe() {
		Inventory.Clear();
		Inventory.AddItem(AxePrefab);
		Sprite.sprite = UnarmoredSprite;
    }

	public void Interact()
    {
		if (Target != null) {
			Target.Interact();
			TargetUI.Hide();
        }	
    }

	public bool HasAxe() {
		return Inventory.HasItem("Axe");
    }

	public bool HasRope() {
		return Inventory.HasItem("Rope");
    }

	public bool HasTear() {
		return Inventory.HasItem("Mountain Tear");
    }

	private Interactable GetTarget() {

		//Layer mask; only looking for hits on character layer.
		int characterLayer = 1 << 8;

		// First check current action direction for hits
		RaycastHit2D hit = Physics2D.Raycast(transform.position, ActionQuadrant, 1f, characterLayer);
		if (hit.collider) {
			Interactable possibleTarget = hit.collider.GetComponent<Interactable>();
			if (possibleTarget != null) {
				return possibleTarget;
			}
		}

		// If no targets found in current action direction, check all other directions
		foreach(Vector3 dir in DirectionVectors) {
			if (dir != ActionQuadrant) {
				hit = Physics2D.Raycast(transform.position, dir, 1f, characterLayer);
				if (hit.collider) {
					Interactable possibleTarget = hit.collider.GetComponent<Interactable>();
					if (possibleTarget != null) {
						return possibleTarget;
					}
				}
			}
        }
		return null;
	}

	private void UpdateTarget() {
		string oldTarget = "";
		if (Target != null) oldTarget = Target.name;

		Target = GetTarget();

		if (Target != null) {
			TargetUI.transform.position = Target.transform.position;

			// Update UI if new target
			if (Target.name != oldTarget) {
				TargetUI.UpdateTarget(Target);

			}
			TargetUI.Show();
		}
		else if (TargetUI.isVisible) TargetUI.Hide();
	}

	#region Rope Handling

	void ThrowRope() {
		Vector3 direction = ActionQuadrant;
		for (int i = 1; i <= ROPE_LENGTH; i++) {
			Vector3Int targetTile = Map.WorldToCell(transform.position + direction * i);
			
			// If a ropeable target is hit, begin animating movement towards target
			if (Map.HasRopeable(targetTile)) {
				StartCoroutine(AnimateRopeMovement(targetTile, direction));
				return;
			}

			// If rope hits obstacle, end rope state
			if (Map.HasObstacle(targetTile)) {
				StartCoroutine(ResetInputFlags());
				StartCoroutine(ClearRopeTiles(Map.WorldToCell(transform.position), direction, ROPE_LENGTH));
				return;
			}

			else {
				StartCoroutine(PaintRope(targetTile, direction));
			}
		}
		// Rope went full distance without hitting a target, return player control and remove rope tiles
		StartCoroutine(ResetInputFlags());
		StartCoroutine(ClearRopeTiles(Map.WorldToCell(transform.position), direction, ROPE_LENGTH));
	}

	IEnumerator AnimateRopeMovement (Vector3Int targetTile, Vector3 direction) {
		Vector3Int playerTile = Map.WorldToCell(transform.position);
		Vector3Int startTile = playerTile;
		while (Vector3.Distance(playerTile, targetTile) > 1) { 
			yield return MoveSprite(playerTile + Vector3Int.FloorToInt(direction));
			Map.ClearRopeTile(playerTile);
			playerTile = Map.WorldToCell(transform.position);
		}

		// jank; clears player's destination tile since it doesn't happen in above loop
		Map.ClearRopeTile(playerTile);
		StartCoroutine(ResetInputFlags());
    }

	IEnumerator PaintRope(Vector3Int targetTile, Vector3 direction) {
		Map.PaintRope(targetTile, direction);
		yield return new WaitForSeconds(0.25f);
    }

	IEnumerator ClearRopeTiles(Vector3Int startTile, Vector3 direction, int distance) {
		while (distance >= 0) {
			Vector3Int target = startTile + Vector3Int.FloorToInt(direction) * distance;
			Map.ClearRopeTile(target);
			distance--;
			yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion

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
	IEnumerator MoveSprite(Vector3Int destination) {
        Vector3Int oldTile = Map.WorldToCell(transform.position);
		transform.position = GroundMap.CellToWorld(destination) + OFFSET_VECTOR;

		//Setting collision tile at new address and removing old one.
		ObstacleMap.SetTile(destination, TileBase.CreateInstance<CustomTile>());
		TileBase.Destroy(ObstacleMap.GetTile<CustomTile>(oldTile));
		yield return new WaitForSeconds(0.1f);
	}

	// Reset flags for taking movement and rope input after a delay
	IEnumerator ResetInputFlags() {
		yield return new WaitForSeconds(ROPE_DECAY);
		MovementAction.Enable();
		UseAction.Enable();
		CanRope = true;
		yield return null;
    }
}