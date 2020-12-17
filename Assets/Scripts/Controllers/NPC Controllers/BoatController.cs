using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : NPCController
{

    private void LateUpdate() {
        float posY = transform.position.y + Mathf.Sin(Time.time * 2f) * 0.0002f;
        transform.position = new Vector3(transform.position.x, posY);
    }
}