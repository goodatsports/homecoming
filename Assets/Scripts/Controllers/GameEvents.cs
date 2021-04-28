using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    public PlayerController Player;
    public Item TearPrefab;
    private int nextScene;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null) Player = playerObject.GetComponent<PlayerController>();
    }

    // Player Inventory events

    public event Action onInventoryOpen;
    public event Action onInventoryClose;

    public void InventoryOpen() {
        onInventoryOpen?.Invoke();
    }
    public void InventoryClose() {
        onInventoryClose?.Invoke();
    }

    // Dialog events

    public event Action onNPCDialogStart;
    public event Action onNPCDialogEnd;
    public event Action onWaitForDialogChoice;
    public event Action<Response> onDialogChoiceMade;
    public void NPCDialogStart() {
        onNPCDialogStart?.Invoke();
    }

    public void NPCDialogEnd() {
        onNPCDialogEnd?.Invoke();
    }

    public void WaitForDialogChoice() {
        onWaitForDialogChoice?.Invoke();
    }
    public void DialogChoiceMade(Response choice) {
        onDialogChoiceMade?.Invoke(choice);
    }

   // Events

    public event Action onShoppingStart;
    public event Action onShoppingEnd;

    public event Action onShoppingItemBuyNo;
    public event Action onShoppingItemBuyYes;

    public event Action onGilroyStoryEnd;
    public event Action onGilroyStoryReset;

    public event Action onAxeTrade;
    public event Action onGharnamQuestComplete;

    public event Action onCorrectFlowerPlacement;
    public string GraveQuestName = "Orrina";

    public event Action onGameEnd;

    public void ShoppingEnd() {
        onShoppingEnd?.Invoke();
    }

    public void DialogChoiceEvent(int eventId) {
        switch (eventId) {
            // Init shopping 
            case 1:
                onShoppingStart?.Invoke();
                break;
            case 2:
                onShoppingItemBuyYes?.Invoke();
                print("ITEM BUY MESSAGE");
                break;
            case 3:
                onShoppingItemBuyNo?.Invoke();
                print("ITEM SELL MESSAGE");
                break;
            case 4:
                print("GAME START");
                break;
            case 5:
                onGilroyStoryEnd?.Invoke();
                break;
            case 6:
                onGilroyStoryReset?.Invoke();
                break;
            case 7:
                onAxeTrade?.Invoke();
                break;
            case 8:
                onGharnamQuestComplete?.Invoke();
                break;
            case 9:
                PlaceFlowerAtGrave();
                break;
            case 10:
                GetFlowerFromGrave();
                break;
            case 11:
                onGameEnd?.Invoke();
                break;
            case 0:
                break;
        }
    }

    public void PlaceFlowerAtGrave() {
        GravestoneNPCController Grave = Player.Target.GetComponent<GravestoneNPCController>();
        Player.Inventory.RemoveItem("Mountain Tear");
        Grave.HasTear = true;

        if (Grave.Dialog.sentences[0].Content.Contains(GraveQuestName)) {
            print("flower done");
            onCorrectFlowerPlacement?.Invoke();
        }
    }

    public void GetFlowerFromGrave() {
        GravestoneNPCController Grave = Player.Target.GetComponent<GravestoneNPCController>();
        Player.Inventory.AddItem(TearPrefab);
        Grave.HasTear = false;
    }

    // Tree Chopping events
    public event Action onTreeChopped;

    public void TreeChopped() {
        onTreeChopped?.Invoke();
    }

    public bool CheckTreesChopped() {
        return GameEvents.current.CheckTreesChopped();
    }


}
