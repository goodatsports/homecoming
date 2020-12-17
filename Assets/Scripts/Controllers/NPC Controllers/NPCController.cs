using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public enum NPCStates
{
    Talking,
    Shopping,
    Walking,
    Standing
}

public class NPCController : Interactable
{
    protected enum Sides
    {
        Left,
        Right
    }

    public Dialog Dialog;
    public DialogController DialogController;
    public MapController Map;
    public Vector3 Destination;
    public bool DoesRoam = false;
    public int RoamRange = 2;

    public Queue<Dialog> RecurringDialog;
    protected Stack<NPCStates> States;
    protected bool Interacting = false;

    protected Vector3 Home;
    protected Vector3 MaxDistance;
    protected bool Moving = false;
    protected Sides Facing = Sides.Right;

    // Used to offset timing for moving tiles for each NPC
    public float TimingOffset;
    protected float minTiming = 0.75f;
    protected float maxTiming = 3f;
    private Vector3 OFFSET_VECTOR = new Vector3(0.5f, 0.5f, 0);

    protected virtual void Awake()
    {
        if (Verb == "") Verb = "Talk to";
        for (int i = 0; i < Dialog.sentences.Length; i++) {
            Dialog.sentences[i].Content = Dialog.sentences[i].Content.Replace("\\n", "\n");
        }

        Dialog.name = Name;
        States = new Stack<NPCStates>();
        Map = GameObject.Find("Map").GetComponent<MapController>();
        DialogController = GameObject.Find("Dialog Controller").GetComponent<DialogController>();
        Home = transform.position;
        MaxDistance = new Vector3((float)RoamRange, (float)RoamRange);

        if (DoesRoam) States.Push(NPCStates.Walking); 
            else States.Push(NPCStates.Standing);
    }

    protected void Start() {
        UpdateMapPosition(Map.WorldToCell(transform.position));
    }


    public override void Interact() {
        StopAllCoroutines();
        if (States.Peek() == NPCStates.Walking || States.Peek() == NPCStates.Standing) {
            Moving = false;
            AddState(NPCStates.Talking);
        } else {
            OnStateChange();
        }
    }

    protected void SetDestination() {
        float newX = (int)(Home.x + UnityEngine.Random.Range(-1 * (float)RoamRange, (float)RoamRange)) + OFFSET_VECTOR.x;
        float newY = (int)(Home.y + UnityEngine.Random.Range(-1 * (float)RoamRange, (float)RoamRange)) + OFFSET_VECTOR.y;

        Destination = new Vector3(newX, newY, Home.z);
    }

    protected IEnumerator Roam() {
        Moving = true;
        TimingOffset = UnityEngine.Random.Range(minTiming, maxTiming);
        SetDestination();

        Vector3 distanceVector = Destination - transform.position;
        float closestX, closestY;

        while (distanceVector != Vector3.zero) {
            distanceVector = Destination - transform.position;
            Vector3 normalDistance = distanceVector.normalized;

            // Move on axis with shortest distance to destination
            if (Mathf.Abs(normalDistance.x) > Mathf.Abs(normalDistance.y)) {
                closestX = normalDistance.x > 0 ? Mathf.Ceil(normalDistance.x) : Mathf.Floor(normalDistance.x);
                closestY = 0f;

            }
            else {
                closestX = 0f;
                closestY = normalDistance.y > 0 ? Mathf.Ceil(normalDistance.y) : Mathf.Floor(normalDistance.y);
            }

            Vector3 closestTile = new Vector3(closestX, closestY);
            yield return StartCoroutine(Move(closestTile));
        }

        Moving = false;
        yield return new WaitForSeconds(TimingOffset);
    }

    protected IEnumerator Move(Vector3 destination) {
        
        // If next tile has obstacle, set a new destination and break
        if (Map.HasObstacle(transform.position + destination)) {
            SetDestination();
            yield return null;
        }
        else {

            // Change side NPC is facing if switching directions on the X axis
            if (Facing == Sides.Left && destination.x > 0) {
                Facing = Sides.Right;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            if (Facing == Sides.Right && destination.x < 0) {
                Facing = Sides.Left;
                transform.rotation = Quaternion.Euler(0, 180, 0);

            }

            Vector3Int oldTile = Map.WorldToCell(transform.position);
            transform.position += Map.GroundMap.CellToWorld(Vector3Int.FloorToInt(destination));

            Vector3Int newTile = Map.WorldToCell(transform.position);
            UpdateMapPosition(newTile, oldTile);

            yield return new WaitForSeconds(TimingOffset);
        }
    }

    private void UpdateMapPosition(Vector3Int newAddress) {
        Map.ObstacleMap.SetTile(newAddress, TileBase.CreateInstance<CustomTile>());
    }

    private void UpdateMapPosition(Vector3Int newAddress, Vector3Int oldAddress) {
        //Setting collision tile at new address and removing old one.
        Map.ObstacleMap.SetTile(newAddress, TileBase.CreateInstance<CustomTile>());
        TileBase.Destroy(Map.ObstacleMap.GetTile<CustomTile>(oldAddress));
    }

    protected void AddState(NPCStates newState) {
        States.Push(newState);
        OnStateChange();
    }

    protected void PopState() {
        States.Pop();
        OnStateChange();
    }

    protected virtual void OnStateChange() {
        if (States.Count == 1) {
            GameEvents.current.NPCDialogEnd();
            return;
        }
        else {
            if (States.Peek() == NPCStates.Talking) {
                AdvanceDialog();
            }

            if (States.Peek() == NPCStates.Walking) {
            }
        }    
    }

    protected void AdvanceDialog() {
        if (!Interacting) {
            Interacting = true;
            GameEvents.current.NPCDialogStart();
            DialogController.StartDialog(Dialog);
        }
        else if (DialogController.HasSentences) {
            DialogController.DisplayNextSentence();

        }
        if (!DialogController.HasSentences) {
            Interacting = false;
            PopState();
        }
    }

    protected void Update() {
        if (States.Peek() == NPCStates.Walking && !Moving) {
            StartCoroutine(Roam());
        }
    }
}
