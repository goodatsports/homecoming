using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPCController : NPCController
{ 
    bool HadInitialConvo = false;
    bool HadMissionTurnInConvo = false;
    public Dialog InitialDialog;
    public Dialog WaitingOnItemDialog;
    public Dialog PostMissionDialog;
    public Item MountainTearPrefab;
    public PlayerController Player;
    protected override void Awake() {
        Dialog = InitialDialog;
        base.Awake();
    }

    public override void Interact() {
        if (Player.HasTear() && !HadMissionTurnInConvo) {
            Dialog = PostMissionDialog;
            HadMissionTurnInConvo = true;
        }
        base.Interact();
    }

    protected override void OnStateChange() {
        if (States.Count == 1) {
            GameEvents.current.NPCDialogEnd();
            if (!HadInitialConvo) {
                HadInitialConvo = true;
                Dialog = WaitingOnItemDialog;
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
