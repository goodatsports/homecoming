using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriggsNPCController : NPCController
{
    private int TimesTalkedTo = 0;
    public Dialog InitialDialog;
    // Start is called before the first frame update
    void Start() {
        InitialDialog = new Dialog(new string[] { "Hail", "Binch" }, Name);       
    }

    public override void Interact() {
        if (TimesTalkedTo != 0) {
            base.Interact();
        } else {
            if (!Interacting) {
                Interacting = true;
                GameEvents.current.NPCDialogStart();
                DialogController.StartDialog(InitialDialog);
            }
            else if (DialogController.HasSentences) {
                DialogController.DisplayNextSentence();
            }
            if (!DialogController.HasSentences) {
                Interacting = false;
                GameEvents.current.NPCDialogEnd();
                TimesTalkedTo++;
            }
        }
    }
}

