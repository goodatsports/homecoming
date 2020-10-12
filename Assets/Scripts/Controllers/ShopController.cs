using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.InputSystem;

public enum Sides
{
    Left,
    Right
}

public class ShopController : MonoBehaviour
{
    public InventoryController PlayerInventory, NPCInventory;
    //public DialogController ShopDialog;
   
    public InputMaster Controls;
    public InputAction MoveCursorAction, ItemInteractAction, EndShopAction;
    public ShopNPCController ShopNPC;
    public bool isActive = false;

    // Used to determine which inventory is active in the shop based on which side of the screen it's on 
    // Left == Player
    // Right == NPC
    private Sides activeInventory;

   

    // Start is called before the first frame update
    void Awake()
    {
        Controls = new InputMaster();
        MoveCursorAction = Controls.Shopping.MoveShopCursor;
        EndShopAction = Controls.Shopping.EndShopping;
        ItemInteractAction = Controls.Shopping.ItemInteract;

        MoveCursorAction.started += ctx => { MoveCursor(ctx.ReadValue<float>()); };
        EndShopAction.started += ctx => { Close(); };
        ItemInteractAction.started += ctx => { Interact(); };
    }


    public void Open() {
        isActive = true;
        activeInventory = Sides.Left;
        Controls.Enable();
        UpdatePositions();
        NPCInventory.ShopToggle();
        PlayerInventory.ShopToggle();
        PlayerInventory.Active();
       
    }

    public void Close() {
        isActive = false;
        GameEvents.current.ShoppingEnd();
        Controls.Disable();
        NPCInventory.ShopToggle();
        PlayerInventory.ShopToggle();

    }

    void UpdatePositions() {
        PlayerInventory.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.3333f, 0.5f, 8f));
        NPCInventory.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.6667f, 0.5f, 8f));
    }


    void MoveCursor(float input) {
      if (input > 0 && activeInventory == Sides.Left) {
            activeInventory = Sides.Right;
            NPCInventory.SetActiveItem(PlayerInventory.GetActiveItem());
            NPCInventory.Active();
            PlayerInventory.Inactive();
        }
      if (input < 0 && activeInventory == Sides.Right) {
            activeInventory = Sides.Left;
            PlayerInventory.SetActiveItem(NPCInventory.GetActiveItem());
            PlayerInventory.Active();
            NPCInventory.Inactive();
        }
    }

    void BuyPrompt() {
        //ShopDialog.StartDialog(new Dialog(new string[]{"Buy item?"}, ""));
        ShopNPC.OnBuyPrompt(NPCInventory.GetActiveItemInfo());
        //print("buy: " + NPCInventory.GetActiveItemInfo());
    }

    void SellPrompt() {
        print("sell: " + PlayerInventory.GetActiveItemInfo());
    }

    void Interact() {
        // Sell if in Player inventory, Buy if in NPC's
        if (activeInventory == Sides.Left) {
            SellPrompt();
        } else {
            BuyPrompt();
        }
    }
}
