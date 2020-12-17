using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPCController : NPCController
{
    public ShopController ShopController;
    public InventoryController Inventory;
    public GameObject AxeOnTable;

    // Start is called before the first frame update
    protected override void Awake() {
        base.Awake();
        //GameEvents.current.onShoppingStart += Shop;
        //GameEvents.current.onShoppingEnd += OnShopClose;
        GameEvents.current.onAxeTrade += TradeAxe;

    }

    protected override void OnStateChange() {
        if (States.Count == 1) {
            GameEvents.current.NPCDialogEnd();
            if (!Inventory.HasItem("Axe")) SetPostTradeDialog();
            return;
        }
        else {
            NPCStates currentState = States.Peek();

            if (currentState == NPCStates.Shopping) {
                ShopController.Open();
            }
            else if (currentState == NPCStates.Talking) {
                AdvanceDialog();
            }
        }
    }

    public void TradeAxe() {
        Inventory.RemoveItem("Axe");
        AxeOnTable.SetActive(false);
    }

    public void SetPostTradeDialog() {
        Dialog = new Dialog(new Sentence[] {
            new Sentence("No refunds!") },
            Name);
    }

    public void Shop() {
        AddState(NPCStates.Shopping);
    }
    public void OnBuyPrompt(Item item) {
        //StartCoroutine(DialogController.AddDialog(new Dialog(new string[] {
        //    $"you want to buy {item.Name}?"
        //}, Name)));
        StartCoroutine(DialogController.AddDialog(new Dialog(new Sentence[] {
            new Sentence($"You want to buy {item.name}?",
                new Choice(new string[] { "yes", "no" },
                    new Response[] { new Response("cool", true, 2), 
                                     new Response("not so cool", true, 3) })) },
            Name)));
        AddState(NPCStates.Talking);

        print("States: " + States);
    }

    void OnShopClose() {
        PopState();
        DialogController.AddDialog(new Dialog(new string[] { "Bye" }, Name));
    }
}
