using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    enum Sides
    {
        Left,
        Right
    }

    public SpriteRenderer Sprite;
    private Vector3 Home;
    private Vector3 MaxDistance;
    public Vector3 Destination;
    private bool Moving = false;
    private Sides Facing = Sides.Right;
    // Start is called before the first frame update
    void Start() 
    {
        Sprite = transform.GetComponent<SpriteRenderer>();
        Home = transform.position;
        MaxDistance = new Vector3(2f, 2f);
        SetDestination();
    }


    void SetDestination() {
        float newX = (int)(Home.x + Random.Range(-2f, 2f)) + 0.5f;
        float newY = (int)(Home.y + Random.Range(-2f, 2f))+ 0.5f;

        Destination = new Vector3(newX, newY, Home.z);
    }

    Vector3 PickDirection() {
        return Vector3.zero;
    }

    IEnumerator Roam() {
        Moving = true;
        SetDestination();

        Vector3 distanceVector = Destination - transform.position;
        float closestX, closestY;

        while (distanceVector != Vector3.zero) {
            distanceVector = Destination - transform.position;
            Vector3 normalDistance = distanceVector.normalized;

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
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Move(Vector3 destination) {
        // Change side horse is facing if switching directions on the X axis
        if (Facing == Sides.Left && destination.x > 0) {
            Facing = Sides.Right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (Facing == Sides.Right && destination.x < 0) {
            Facing = Sides.Left;
            transform.rotation = Quaternion.Euler(0, 180, 0);
          
        }
        transform.position += new Vector3(destination.x, destination.y);

        yield return new WaitForSeconds(2f);
    }

    void UpdateFacing() {

    }

    void Update()
    {
        if (!Moving) {
            StartCoroutine(Roam());
        }
    }
}
