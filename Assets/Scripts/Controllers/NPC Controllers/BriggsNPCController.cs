using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriggsNPCController : NPCController
{
    private int TimesTalkedTo = 0;
    public Dialog InitialDialog;
    public Dialog WaitingOnTreesDialog, TreesCompleteDialog;
    public GameManager Manager;
    private bool hadInitialConversation = false;


    protected override void Awake() {
        base.Awake();
        WaitingOnTreesDialog = new Dialog(new Sentence[] {
            new Sentence("You chop all those trees yet?",
                new Choice(new string[] { "yes", "no" },
                    new Response[] { new Response("No you didn't! Get back out there!"),
                                     new Response("I appreciate the honesty, but why even talk to me, then?") })) },
            Name);

        TreesCompleteDialog = new Dialog(new Sentence[] {
            new Sentence("Appreciate the work, you've made the path to the Bridge a lot easier for us."),
            new Sentence("Might want to head into the vale through the clearing you made and talk to Garnum."),
            new Sentence("He's a hunter with a cabin, haven't seen him in a couple seasons, but he should know the path to the Bridge.")},
            Name);

    }


    bool CheckTreesProgress() {
        return Manager.AllTreesChopped();
    }

    public override void Interact() {
        print("INTERACTING");
        print($"HASINTERACTED: {hadInitialConversation}");
        print("INTERACTING: " + Interacting);
        if (!Interacting) {
            if (CheckTreesProgress()) {
                Dialog = TreesCompleteDialog;
            }
            else Dialog = WaitingOnTreesDialog;
        }
        base.Interact();
    }

    //public override void Interact() {
    //    if (TimesTalkedTo != 0) {
    //        base.Interact();
    //    } else {
    //        if (!Interacting) {
    //            Interacting = true;
    //            GameEvents.current.NPCDialogStart();
    //            DialogController.StartDialog(InitialDialog);
    //        }
    //        else if (DialogController.HasSentences) {
    //            DialogController.DisplayNextSentence();
    //        }
    //        if (!DialogController.HasSentences) {
    //            Interacting = false;
    //            GameEvents.current.NPCDialogEnd();
    //            TimesTalkedTo++;
    //        }
    //    }
    //}
}

