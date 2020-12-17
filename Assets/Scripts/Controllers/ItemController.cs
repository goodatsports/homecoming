using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class ItemController : Interactable
{
    public Item ItemPrefab;
    public DialogController DialogController;
    public Dialog PickupDialog;
    public SpriteRenderer ItemSprite;
    public TextMeshPro NameUI;
    public TextMeshPro Description;

    private bool Interacting = false;
    

    private void Awake() {
        Verb = "Pick up";
        if (ItemSprite == null) {
            ItemSprite = gameObject.GetComponent<SpriteRenderer>();
        }
        if (ItemPrefab != null) {
            PickupDialog = new Dialog(new string[] { $"You found a {ItemPrefab.name}!", "You put it in your satchel." }, ItemPrefab.name);
            ItemSprite.sprite = ItemPrefab.Sprite;
        }
    }
  
    public override void Interact() {
        if (!Interacting) {
            Interacting = true;
            DialogController.StartDialog(PickupDialog);
        }
        else if (DialogController.HasSentences) {
            DialogController.DisplayNextSentence();

        }
        if (!DialogController.HasSentences) {
            Interacting = false;
            PickUp();
        }
    }
    private void PickUp() {
        GameObject go = GameObject.Find("Player");
        print("obj: " + go);
        InventoryController PlayerInventory = 
            go.transform.Find("Player Inventory").GetComponent<InventoryController>();
        print("inv: " + PlayerInventory);
        PlayerInventory.AddItem(ItemPrefab);
        Destroy(gameObject);
    }

    public void SetSprite(Sprite sprite) {
        ItemSprite.sprite = sprite;
    }

    public void SetName(string name) {
        NameUI.text = name;
    }

    public void SetDescription(string description) {
        Description.text = description;
    }
}
