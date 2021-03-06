using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaliaNPCController : NPCController
{
    bool HadInitialConvo = false;
    public Dialog InitialDialog;
    public Dialog WaitingOnQuestDialog;
    public Dialog FinalDialog;

    // Set dialog for if Player finishes final quest before talking to Dalia
    public Dialog OneShotDialog;

    public PlayerController Player;
    protected override void Awake() {
        Name = "Dalia";
        Dialog = InitialDialog;
        GameEvents.current.onCorrectFlowerPlacement += SetFinalDialog;
        base.Awake();
    }

    public void SetFinalDialog() {
        if (HadInitialConvo) {
            Dialog = FinalDialog;
        } else {
            Dialog = OneShotDialog;
        }
    }

    public override void Interact() {
        if (!Player.HasTear() && HadInitialConvo) {
            Dialog = FinalDialog;
        } 
        base.Interact();
    }

    protected override void OnStateChange() {
        if (States.Count == 1) {
            GameEvents.current.NPCDialogEnd();
            if (!HadInitialConvo) {
                HadInitialConvo = true;
                Dialog = WaitingOnQuestDialog;
            }
            return;
        }
        else {
            if (States.Peek() == NPCStates.Talking) {
                AdvanceDialog();
            }

            if (States.Peek() == NPCStates.Walking) {
            }
        }
    }
}
