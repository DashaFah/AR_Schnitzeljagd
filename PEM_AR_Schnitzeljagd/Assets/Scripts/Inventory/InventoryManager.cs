using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    private GameItem[] inventory;

    public RectTransform ItemPanel;
    public GameObject UI_Inventory;

    public GameObject prefabSlot;
    private int rows = 2;
    private int columns = 4;
    public Vector2 dimensions_landscape = new Vector2(4, 2); // rows, columns
    public Vector2 dimensions_portrait = new Vector2(2, 4);

    private GameObject[] UI_slots;
    public bool renderInventoryUI = false;
    public bool dbgPrintItemID = false;

    public void Init()
    {
        inventory = new GameItem[rows * columns];
        UI_slots = new GameObject[rows * columns];
        for (int i = 0; i < rows * columns; i++)
        {
            GameObject prefabSlotObj = Instantiate(prefabSlot);

            int btnIndex = i; // if not used this will result in closure problem -> same button id returned for all buttons
            var btn = prefabSlotObj.GetComponentInChildren<UnityEngine.UI.Button>();
            btn.onClick.AddListener(() => { ItemSlotWasPressed(btnIndex); });

            UI_slots[i] = prefabSlotObj;
        }


        //AddItem(new Apple());
        //AddItem(new Apple());

        //Debug.Log(string.Format("inv[0]: {0} - {1}", inventory[0].ItemID, inventory[0].ItemType));
        //Debug.Log(string.Format("inv[1]: {0} - {1}", inventory[1].ItemID, inventory[1].ItemType));

        //AddItem(new GameItem(1, "item_1"));
        //AddItem(new GameItem(2, "item_2"));
        //AddItem(new GameItem(3, "item_3"));
        //AddItem(new GameItem(4, "item_4"));
        //AddItem(new GameItem(5, "item_5"));
    }

    private void Reset()
    {
        Init();
    }

    public bool AddItem(GameItem item)
    {
        if (item == null)
            return false;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                inventory[i] = item;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(int id)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && inventory[i].ItemID == id)
            {
                inventory[i] = null;
                return true;
            }
        }
        return false;
    }

    public bool HasItem(int id)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && inventory[i].ItemID == id)
            {
                return true;
            }
        }
        return false;
    }

    public bool HasItemOfType(ItemType type)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && inventory[i].ItemType == type)
            {
                return true;
            }
        }
        return false;
    }


    private void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("rows: " + rows + " columns: " + columns);
        UpdateSlotDimensions();
    }


    void UpdateSlotDimensions()
    {
        float width = ItemPanel.rect.width;
        float height = ItemPanel.rect.height;

        float eleWidth = width / columns;
        float eleHeight = height / rows;
        //Debug.Log("w: " + width + " h:" + height);
        //Debug.Log("wh: " + eleWidth + " eh:" + eleHeight);
        var tl = new Vector2(-width / 2, height / 2);
        var tlAnchor = new Vector2(tl.x + eleWidth / 2, tl.y - eleHeight / 2);

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                //Debug.Log("r: " + r + " c: " + c);
                GameObject prefabSlotObj = UI_slots[(r * columns) + c];
                prefabSlotObj.transform.SetParent(ItemPanel, false);
                RectTransform rt = prefabSlotObj.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(eleWidth, eleHeight);
                rt.localPosition = new Vector3(0, 0, 0);
                rt.Translate(tlAnchor);
                rt.Translate(new Vector2((c) * eleWidth, -r * eleHeight));

                //Debug.Log(rt.localPosition);
            }
        }
    }

    void UpdateSlotItems()
    {
        for (int si = 0; si < inventory.Length; si++)
        {
            GameItem item = inventory[si];
            if (item != null)
            {
                var slot = UI_slots[si].gameObject.GetComponentInChildren<ItemSlot>();
                //var txt = UI_slots[si].gameObject.GetComponentInChildren<Text>();
                //var image = UI_slots[si].gameObject.GetComponentInChildren<UnityEngine.UI.Image>();
                //image.sprite = item.Sprite;
                ////Debug.Log(txt.text);
                //txt.text = string.Format("{0} - {1}", item.ItemID, item.Name);
                if (dbgPrintItemID)
                    slot.UpdateName(string.Format("{0} - {1}", item.ItemID, item.Name));
                else
                    slot.UpdateName(item.Name);

                slot.UpdateSprite(item.Sprite);

                //TouchButton tButton = UI_slots[si].gameObject.GetComponentInChildren<TouchButton>();
                //if(tButton.Pressed && !tButton.IsHeld)
                //{
                //    Debug.Log(string.Format("Button for item {0} - {1} has been pressed", item.ItemID, item.Name));
                //}

            }
        }
    }

    public void ToggleShowInventoryUI()
    {
        renderInventoryUI = !renderInventoryUI;
    }

    public void ShowInventoryUI(bool show)
    {
        renderInventoryUI = show;
    }

    private void ItemSlotWasPressed(int slotIndex)
    {
        Debug.Log(string.Format("Button for ItemSlot {0} was Pressed", slotIndex));
        if (inventory[slotIndex] != null)
        {
            GameItem item = inventory[slotIndex];
            item.StartUsingItem();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.deviceOrientation);
        switch (Input.deviceOrientation)
        {
            case DeviceOrientation.Unknown:
            case DeviceOrientation.Portrait:
            case DeviceOrientation.PortraitUpsideDown:
                rows = (int)dimensions_portrait.x;
                columns = (int)dimensions_portrait.y;
                break;
            case DeviceOrientation.LandscapeLeft:
            case DeviceOrientation.LandscapeRight:
                rows = (int)dimensions_landscape.x;
                columns = (int)dimensions_landscape.y;
                break;
            case DeviceOrientation.FaceUp:
            case DeviceOrientation.FaceDown:
                // no changes, keep current orientation
                break;
        }

        if (renderInventoryUI)
        {
            if (!UI_Inventory.activeSelf)
            {
                UI_Inventory.SetActive(true);
            }

            UpdateSlotDimensions();
            UpdateSlotItems();
        }
        else
        {
            if (UI_Inventory.activeSelf)
            {
                UI_Inventory.SetActive(false);
            }
        }
    }
}
