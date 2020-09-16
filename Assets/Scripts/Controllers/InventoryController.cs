using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour
{
    public Item[] Items;
    public ItemController ItemPrefab;
    GameObject UI;
    // Used to space items' positions from each other when displayed in inventory screen
    private float ITEM_UI_Y_OFFSET = -1.6f;

    // I need this, I do not know why
    private float ITEM_UI_X_OFFSET = -0.95f;

    // Used to account for camera's distance from game board (10 units) when calculating viewport->worldpoint values
    private float CAMERA_Z_OFFSET = 8f;

    // Start is called before the first frame update
    void Start()
    {
       
        UI = transform.Find("UI").gameObject;
        UI.SetActive(false);
        Vector3 inventoryPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, CAMERA_Z_OFFSET));
        transform.position = inventoryPos;
        int index = 0;
        foreach (Item item in Items) {
            ItemController itemUI = ItemPrefab;
            itemUI.SetName(item.Name);
            itemUI.SetDescription(item.Description);
            itemUI.SetSprite(item.Sprite);
            Vector3 itemPos = new Vector3(inventoryPos.x + ITEM_UI_X_OFFSET, inventoryPos.y + ITEM_UI_Y_OFFSET * index, inventoryPos.z);
            ItemController activeItem = Instantiate(itemUI, itemPos, Quaternion.identity);
            activeItem.transform.SetParent(UI.transform);
            index++;
        }

    }

    // Called from character controller
    public void Toggle() {
        if (UI.activeSelf) {
            Close();
        } else {
            Open();
        }
    }
    void Open() {
        GameEvents.current.InventoryOpen();
        UI.SetActive(true);
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, CAMERA_Z_OFFSET));
        
    }

    void Close() {
        UI.SetActive(false);
        GameEvents.current.InventoryClose();
    }
}
