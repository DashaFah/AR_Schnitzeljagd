using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Debug Item used for turing debug menu on and off
/// </summary>
public class Apple : GameItem
{
    public UnityEvent OnStartUsingApple;
    public GameObject objectToToggle;

    public Apple() : base(ItemType.Apple, "Apfel")
    {
        if (OnStartUsingApple == null)
            OnStartUsingApple = new UnityEvent();
    }

    public override void StartUsingItem()
    {
        Debug.Log(string.Format("StartUsingItem {0} - {1}", ItemID, Name));
        Debug.Log(string.Format("# Apple Nom Nom Nom"));
        OnStartUsingApple?.Invoke();
        if (objectToToggle != null)
            objectToToggle.SetActive(!objectToToggle.activeSelf);
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
