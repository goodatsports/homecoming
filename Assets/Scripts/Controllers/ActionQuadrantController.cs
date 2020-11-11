using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQuadrantController : MonoBehaviour
{
    public PointerController Pointer;
    private SpriteRenderer[] Arrows;
    private Vector3 Direction;


    // Start is called before the first frame update
    void Awake()
    {
        Pointer = GameObject.Find("Pointer").GetComponent<PointerController>();
        Arrows = gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    public void SetActiveQuadrant(Vector3 dir)
    {
        Direction = dir;
        if (dir == Vector3.up) UpdateArrows(Directions.Up);
        if (dir == Vector3.right) UpdateArrows(Directions.Right);
        if (dir == Vector3.left) UpdateArrows(Directions.Left);
        if (dir == Vector3.down) UpdateArrows(Directions.Down);
    }

    public Vector3 GetActiveQuadrant() {
        return Direction;
    }

    private void UpdateArrows(Directions activeQuadrant)
    {
        for (int i = 0; i < Arrows.Length; i++)
        {
            if (i != (int)activeQuadrant)
            {
                Arrows[i].color = Color.white;
            }
        }
        Arrows[(int)activeQuadrant].color = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
