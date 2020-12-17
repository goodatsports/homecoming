using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class InventoryController : MonoBehaviour
{
    public List<Item> Items;
    public ItemController ItemPrefab;
    public SpriteRenderer Pointer;
    public GameObject UI;
    public InputMaster Controls;
    public InputAction MoveInventoryCursor;

    private int activeItem = int.MaxValue;
    private Vector3 initialPointerPos;

    // Used to space items' positions from each other when displayed in inventory screen
    private float ITEM_UI_Y_OFFSET = -1.6f;

    private Vector3 ITEM_UI_OFFSET_VECTOR;

    // Used to account for camera's distance from game board (10 units) when calculating viewport->worldpoint values
    private float CAMERA_Z_OFFSET = 8f;

    private void Awake() {
        Controls = new InputMaster();
        MoveInventoryCursor = Controls.Inventory.MoveInventoryCursor;

        MoveInventoryCursor.started += ctx => { MovePointer(ctx.ReadValue<float>()); };
    }

    // Start is called before the first frame update
    void Start()
    {
        ITEM_UI_OFFSET_VECTOR = new Vector3(0, ITEM_UI_Y_OFFSET);
        UI = transform.Find("UI").gameObject;
        gameObject.SetActive(false);
        Pointer.enabled = false;
        Vector3 inventoryPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, CAMERA_Z_OFFSET));
        transform.position = inventoryPos;
        UpdateInventory();
       
        // If inventory has items, set the active item to the first in the array
        if (Items.Count > 0) {
            activeItem = 0;
        }

    }

    void LateUpdate() {
        // Oscillate pointer sprite y pos over time
        if (gameObject.activeSelf) { 
            float posY = Pointer.transform.position.y + Mathf.Sin(Time.time * 2f) * 0.0003f;
            Pointer.transform.position = new Vector3(Pointer.transform.position.x, posY);
        }
    }

    void MovePointer(float input) {
        if (input > 0 && activeItem > 0) {
            activeItem--;
            Pointer.transform.position -= ITEM_UI_OFFSET_VECTOR;
        }
        else if (input < 0 && activeItem < Items.Count - 1) {
            activeItem++;
            Pointer.transform.position += ITEM_UI_OFFSET_VECTOR;
        }
    }

    void SetPointer() {
        Pointer.transform.position = initialPointerPos + ITEM_UI_OFFSET_VECTOR * activeItem;
    }

    // Called from Shop controller
    // Used instead of regular Toggle so as to control which pointer/controls of both inventories is active
    public void ShopToggle() {
        if (gameObject.activeSelf) {
            Close();
        } else {
            ShopOpen();
        }
    }

    public void ShopOpen() {
        gameObject.SetActive(true);
        initialPointerPos = Pointer.transform.position;
        Controls.Enable();
    }

    // Called from Shop controller to enable it when the inventory in use by Player
    public void Active() {
        Pointer.enabled = true;
    }
    public void Inactive() {
        Pointer.enabled = false;

    }
    
    //  Returns true if inventory contains item whose name is partial match to search string
    public bool HasItem(string name) {
        foreach (Item item in Items) {
            if (item.name.Contains(name)) {
                return true;
            }
        }
        return false;
    }

    public void AddItem(Item newItem) {
        Items.Add(newItem);
        UpdateInventory();
    }

    public void RemoveItem(string itemName) {
        Item toBeRemoved = null;
        foreach (Item i in Items) {
            if (i.name == itemName) {
                toBeRemoved = i;
                break;
            }
        }
        if (toBeRemoved != null) {
            
            Items.Remove(toBeRemoved);
            Destroy(UI.transform.Find(toBeRemoved.Name).gameObject);
            UpdateInventory();
        }
    }

    // Empty inventory
    public void Clear() {
        foreach (Item i in Items) {
            Destroy(UI.transform.Find(i.Name).gameObject);
        }
        Items.Clear();
        UpdateInventory();
    }

    public void UpdateInventory() {
        Vector3 inventoryPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, CAMERA_Z_OFFSET));
        int index = 0;

        foreach (Item item in Items) {
            ItemController itemUI = ItemPrefab;
            itemUI.SetName(item.Name);
            itemUI.SetDescription(item.Description);
            itemUI.SetSprite(item.Sprite);
            Vector3 itemPos = new Vector3(inventoryPos.x, inventoryPos.y + ITEM_UI_Y_OFFSET * index, inventoryPos.z);
            ItemController newItem = Instantiate(itemUI, itemPos, Quaternion.identity,UI.transform);
            newItem.name = item.Name;
            index++;
        }
    }

    // Update to closest index'ed item in inventory given the index of another inventory's activeItem.
    // Called from ShopController when player moves between inventories and the pointer of inventory being moved into 
    // needs to be updated.
    public void SetActiveItem(int index) {
        if (index < Items.Count - 1) {
            activeItem = index;
        } else {
            activeItem = Items.Count - 1;
        }
        SetPointer();
    }

    public int GetActiveItem() {
        return activeItem;
    }

    public Item GetActiveItemInfo() {
        return Items[activeItem];
    }

    public int GetItemIndex(string name) {
        for(int i = 0; i < Items.Count; i++) {
            if (Items[i].name == name) {
                return i;
            }
        }
        return -1;
    }


    // Called from character controller
    public void Toggle() {
        if (gameObject.activeSelf) {
            Close();
        } else {
            Open();
        }
    }
    void Open() {
        GameEvents.current.InventoryOpen();
        Controls.Enable();
        gameObject.SetActive(true);
        Pointer.enabled = true;
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, CAMERA_Z_OFFSET));
        initialPointerPos = Pointer.transform.position;
    }

    void Close() {
        gameObject.SetActive(false);
        Controls.Disable();
        Pointer.enabled = false;
        GameEvents.current.InventoryClose();
    }
}
