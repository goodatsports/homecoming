using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GharnamNPCController : NPCController
{
   public bool HadInitialConvo = false;
    public bool HadMissionTurnInConvo = false;
    public bool HadFinalConvo = false;
    public Dialog InitialDialog;
    public Dialog WaitingOnItemDialog;
    public Dialog PostMissionDialog;
    public Dialog FinalDialog;
    public Item MountainTearPrefab;
    public PlayerController Player;
    protected override void Awake() {
        Dialog = InitialDialog;
        base.Awake();
    }

    public override void Interact() {
        if (Player.HasTear() && !HadMissionTurnInConvo) {
            Dialog = PostMissionDialog;
        }
        base.Interact();
    }

    protected override void OnStateChange() {
        if (States.Count == 1) {
            GameEvents.current.NPCDialogEnd();
            if (!HadInitialConvo) {
                HadInitialConvo = true;
            }
            print("dialog == postmissiondialog ?: " + (Dialog == PostMissionDialog));
            if (Dialog == PostMissionDialog) {
                HadMissionTurnInConvo = true;
                Dialog = FinalDialog;
            }
            else if (!HadMissionTurnInConvo) Dialog = WaitingOnItemDialog;
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
