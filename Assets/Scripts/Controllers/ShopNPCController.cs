using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPCController : NPCController
{
    public ShopController ShopController;
    public InventoryController Inventory;
    // Start is called before the first frame update
    protected override void Awake() {
        base.Awake();
        GameEvents.current.onShoppingStart += Shop;
        GameEvents.current.onShoppingEnd += OnShopClose;

    }

    public override void Interact() {
        if (ShopController.isActive) {
        }
        else {
            base.Interact();
        }
    }

    public void Shop() {
        Interacting = false;
        ShopController.Open();

    }
    public void OnBuyPrompt(Item item) {
        Interact(new Dialog(new string[] {
            $"you want to buy {item.Name}?"
        }, Name));
    }

    void OnShopClose() {
        new Dialog(new string[] { "Bye", "Binch" }, Name);
        Interact(new Dialog(new string[] { "Bye", "Binch" }, Name));
    }
}
