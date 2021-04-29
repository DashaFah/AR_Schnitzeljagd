using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bread : GameItem
{

    public Bread() : base(ItemType.Bread, "Brot")
    {
    }

    public override void StartUsingItem()
    {
        Debug.Log(string.Format("StartUsingItem {0} - {1}", ItemID, Name));
        Debug.Log(string.Format("# yummy brot"));
    }

    public override void StopUsingItem()
    {
        Debug.Log(string.Format("StopUsingItem {0} - {1}", ItemID, Name));
    }

    public override void TriggerItemUse()
    {
        Debug.Log(string.Format("TriggerItemUse {0} - {1}", ItemID, Name));
    }
}