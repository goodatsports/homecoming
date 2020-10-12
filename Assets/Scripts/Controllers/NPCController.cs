using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NPCStates
{
    Talking,
    Shopping
}

public class NPCController : Interactable
{
    public string Name;
    public Dialog Dialog;
    public DialogController DialogController;
    protected Stack<NPCStates> States;
    protected bool Interacting = false;
    public Queue<Dialog> RecurringDialog;


    protected virtual void Awake()
    {
        Dialog.name = Name;
        States = new Stack<NPCStates>();
    }
    public override void Interact()
    {
        if (States.Count == 0) {
            print("Interact: no states in stack");
            AddState(NPCStates.Talking);
        } else {
            print("Interact: state change");
            OnStateChange();
        }
        //Interact(Dialog);
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
        if (States.Count == 0) {
            GameEvents.current.NPCDialogEnd();
            return;
        }
        else {
            if (States.Peek() == NPCStates.Talking) {
                print("advancing dialog");
                AdvanceDialog();
            }
        }    
    }

    protected void AdvanceDialog() {
        if (!Interacting) {
            print("Advance Dialog: first interaction");
            Interacting = true;
            GameEvents.current.NPCDialogStart();
            DialogController.StartDialog(Dialog);
        }
        else if (DialogController.HasSentences) {
            DialogController.DisplayNextSentence();
            print("Advance Dialog: next sentence");

        }
        if (!DialogController.HasSentences) {
            Interacting = false;
            print("popping state");
            PopState();
        }
    }

    //public void Interact(Dialog newDialog) {
    //    if (!Interacting) {
    //        print("NPC controller: first interaction");
    //        Interacting = true;
    //        GameEvents.current.NPCDialogStart();
    //        DialogController.StartDialog(newDialog);
    //    }
    //    else if (DialogController.HasSentences) {
    //        DialogController.DisplayNextSentence();
    //        print("NPC controller: next sentence");

    //    }
    //    if (!DialogController.HasSentences) {
    //        print("NPC controller: end dialog");

    //        Interacting = false;
    //        GameEvents.current.NPCDialogEnd();
    //    }
    //}
}
