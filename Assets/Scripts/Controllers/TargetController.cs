using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private SpriteRenderer[] Markers;
    private void Awake() {
        Markers = gameObject.GetComponentsInChildren<SpriteRenderer>();

    }
    public void Hide() {
        for (int i = 0; i < Markers.Length; i++) {
            Markers[i].enabled = false;
        }
    }

    public void Show() {
        for (int i = 0; i < Markers.Length; i++) {
            Markers[i].enabled = true;
        }
    }

 

}
