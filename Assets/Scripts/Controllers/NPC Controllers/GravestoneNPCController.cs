using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TO-DO: 
/// - Add GameEvents IDs for getting/leaving Mountain Tear at grave, pass which Gravestone it happened to. (via current player Target? new grave ID field?)
/// - Flip GraveHasTear field of target Gravestone
/// - Add/Remove Mountain Tear from Player Inventory (from GameEvents/PlayerContoller)
/// </summary>
public class GravestoneNPCController : NPCController
{
    // Change Dialog to have choices based on whether Player or Grave has flower in front of it
    public Dialog PlayerHasTearDialog;
    public Dialog GraveHasTearDialog;
    public PlayerController Player;
    public bool HasTear = false;
    private Dialog DefaultDialog;

    protected override void Awake() {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        DefaultDialog = Dialog;

        // Copy default dialog into other dialog options as a base, then add sentences with choice to either add/remove flower
        PlayerHasTearDialog = Dialog.Copy();
        GraveHasTearDialog = Dialog.Copy();

        PlayerHasTearDialog.AddSentence(new Sentence("Place Mountain Tear on grave?",
            new Choice(new string[] { "Yes", "No" }, new Response[] { 
                new Response("You place the flower gingerly in front of the gravestone.", true, 9),
                new Response("You step away from the grave.")})));

        GraveHasTearDialog.AddSentence(new Sentence("The Mountain Tear sits in front of the grave."));
        GraveHasTearDialog.AddSentence(new Sentence("Take the Mountain Tear?",
            new Choice(new string[] { "Yes", "No" }, new Response[] {
                new Response("You take the flower from the grave and place it in your satchel.", true, 10),
                new Response("You step away from the grave.")})));


        base.Awake();
    }

    public override void Interact() {
        if (Player.HasTear()) {
            Dialog = PlayerHasTearDialog;
        }
        else if (HasTear) Dialog = GraveHasTearDialog;
        else Dialog = DefaultDialog;
        base.Interact();
    }
}
