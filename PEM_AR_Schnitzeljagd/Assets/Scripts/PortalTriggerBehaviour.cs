using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalTriggerBehaviour : MonoBehaviour, ITouchable
{
    public UnityEvent OnEnterPortal;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter Portal");
        OnEnterPortal.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay Portal");
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit Portal");
    }

    public void OnTouchDown()
    {
        Debug.Log("OnTouchDown Portal");
        OnEnterPortal.Invoke();
    }

    public void OnTouchUp()
    {
        Debug.Log("OnTouchUp Portal");
    }

    public void OnTouchStay()
    {
        Debug.Log("OnTouchStay Portal");
    }

    public void OnTouchExit()
    {
        Debug.Log("OnTouchExit Portal");
    }
}
