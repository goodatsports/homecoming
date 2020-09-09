using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Interactable
{
    public string Name;
    public Dialog Dialog;
    public DialogController2 DialogController;

    public void Awake()
    {
        //DialogController = gameObject.GetComponent<DialogController2>();
    }
    public override void Interact()
    {
        DialogController.StartDialog(Dialog);
        Debug.Log($"Howdy partner, my name is {Name}.");
    }
}
