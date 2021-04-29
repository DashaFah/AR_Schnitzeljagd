using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binoculars : GameItem
{

    public Binoculars() : base(ItemType.Apple, "Fernglas")
    {
    }

    public override void StartUsingItem()
    {
        Debug.Log(string.Format("StartUsingItem {0} - {1}", ItemID, Name));
        Debug.Log(string.Format("# Fernglas Guck Guck"));
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