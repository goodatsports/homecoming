using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    public GameObject UI;
    private TextMeshProUGUI Text;
    private string Name;
    private SpriteRenderer[] Markers;
    public bool isVisible;
    private void Awake() {
        Text = UI.GetComponentInChildren<TextMeshProUGUI>();
        Markers = gameObject.GetComponentsInChildren<SpriteRenderer>();

        if (Text == null) print("Target Text UI not found!");
        Hide();
    }

    public void Hide() {
        UI.SetActive(false);
        for (int i = 0; i < Markers.Length; i++) {
            Markers[i].enabled = false;
        }
        isVisible = false;
    }

    public void Show() {
        UI.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.2f, 1f));
        UI.SetActive(true);
        for (int i = 0; i < Markers.Length; i++) {
            Markers[i].enabled = true;
        }
        isVisible = true;
    }

    public void UpdateTarget(Interactable newTarget) {
        Text.text = $"{newTarget.Verb} {newTarget.Name}";
    }

}
