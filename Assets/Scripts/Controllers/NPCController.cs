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
    private Stack<NPCStates> States;
    protected bool Interacting = false;

    protected virtual void Awake()
    {
        Dialog.name = Name;
        States = new Stack<NPCStates>();
    }
    public override void Interact()
    {
        Interact(Dialog);
    }
    
    void AddState(NPCStates newState) {
        States.Push(newState);
        CheckState();
    }

    void PopState() {
        States.Pop();
        CheckState();
    }

    void CheckState() {
        if (States.Count == 0) {
            GameEvents.current.NPCDialogEnd();
            return;
        }
        else {
            NPCStates currentState = States.Peek();

            if (currentState == NPCStates.Shopping) {

            }
            else if (currentState == NPCStates.Talking) {

            }
        }    
    }

    void AdvanceDialog() {

    }

    public void Interact(Dialog newDialog) {
        if (!Interacting) {
            print("NPC controller: first interaction");
            Interacting = true;
            GameEvents.current.NPCDialogStart();
            DialogController.StartDialog(newDialog);
        }
        else if (DialogController.HasSentences) {
            DialogController.DisplayNextSentence();
            print("NPC controller: next sentence");

        }
        if (!DialogController.HasSentences) {
            print("NPC controller: end dialog");

            Interacting = false;
            GameEvents.current.NPCDialogEnd();
        }
    }
}
