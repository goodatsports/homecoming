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

    public event Action onNPCDialogStart;
    public event Action onNPCDialogEnd;
    public void NPCDialogStart() {
        onNPCDialogStart?.Invoke();
    }

    public void NPCDialogEnd() {
        onNPCDialogEnd?.Invoke();
    }

    public event Action onInventoryOpen;
    public event Action onInventoryClose;

    public void InventoryOpen() {
        onInventoryOpen?.Invoke();
    }
    public void InventoryClose() {
        onInventoryClose?.Invoke();
    }

    public event Action onWaitForDialogChoice;
    public event Action<Response> onDialogChoiceMade;

    public void WaitForDialogChoice() {
        onWaitForDialogChoice?.Invoke();
    }
    public void DialogChoiceMade(Response choice) {
        onDialogChoiceMade?.Invoke(choice);
    }

    public event Action onShoppingStart;
    public event Action onShoppingEnd;

    public void DialogChoiceEvent(int eventId) {
        switch (eventId) {
            // Init shopping 
            case 1:
                onShoppingStart?.Invoke();
                break;
            case 0:
                break;
        }

    }

    public void ShoppingEnd() {
        onShoppingEnd?.Invoke();
    }


}
