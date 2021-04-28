using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string Name;
    public string Verb = "";
    public virtual void Interact()
    {
        Debug.Log($"Interaction started with {transform.name}");
    }
}
