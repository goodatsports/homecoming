using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Interactable
{
    public string Name;
    public Dialog Dialog;
    public DialogController2 DialogController;
    protected bool Interacting = false;

    public void Awake()
    {
        Dialog.name = Name;
    }
    public override void Interact()
    {
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
            GameEvents.current.NPCDialogEnd();
        }
    }
}
