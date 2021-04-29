using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class GameItem : MonoBehaviour
{
    static int idCount = 0;

    public int ItemID { get; private set; }
    public ItemType ItemType { get; private set; }

    public string Name = "";
    //public Image Image;
    public Sprite Sprite;

    public bool HasARRepresentation;
    public GameObject ARRepresentation;

    public GameItem() : this(ItemType.Undefined, "undefined")
    {
    }

    public GameItem(ItemType type, string name)  
    {
        ItemID = idCount++;

        ItemType = type;
        Name = name ?? "";
    }

    /// <summary>
    /// triggers start stop of Item use
    /// </summary>
    public abstract void TriggerItemUse();
    public abstract void StartUsingItem();
    public abstract void StopUsingItem();  
}

