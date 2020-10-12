using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public SpriteRenderer Sprite;
    private Vector3 Home;
    private Vector3 MaxDistance;
    public Vector3 Destination;
    private bool Moving = false;
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
            print("horse distance: " + distanceVector);
            Vector3 normalDistance = distanceVector.normalized;
            print("normalized distance: " + normalDistance);

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
        print("horse reached destination");
        Moving = false;
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Move(Vector3 destination) {
        print("moving horse: " + destination);
        Sprite.transform.position += new Vector3(destination.x, destination.y);
        yield return new WaitForSeconds(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Moving) {
            print("horse roaming...");
            StartCoroutine(Roam());
        }
    }
}
