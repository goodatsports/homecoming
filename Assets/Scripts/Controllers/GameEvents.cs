using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
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

    // Shopping events

    public event Action onShoppingStart;
    public event Action onShoppingEnd;

    public event Action onShoppingItemBuyNo;
    public event Action onShoppingItemBuyYes;

    public event Action onGilroyStoryEnd;
    public event Action onGilroyStoryReset;

    public event Action onAxeTrade;
    public event Action onGharnamQuestComplete;


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
            case 0:
                break;
        }
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
