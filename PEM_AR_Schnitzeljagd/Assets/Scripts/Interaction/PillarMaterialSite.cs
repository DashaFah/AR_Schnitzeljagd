using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum MaterialSiteDepletionEvent { Nothing, Deactivate, Event}

public class PillarMaterialSite : MonoBehaviour, IMaterialSite
{
    //public UnityEvent MaterialTakenEvent;
    //public UnityEvent MaterialSiteDepletedEvent;
    public int ammount = 300;


    public IMaterialSiteType getSiteType()
    {
        return IMaterialSiteType.Pillar;
    }

    public void OnMaterialTransferCanceled()
    {
        Debug.Log("PillarMaterialSite.onMaterialCanceled()");
    }

    public void OnMaterialTransferedSuccessfullyFinished()
    {
        Debug.Log("PillarMaterialSite.onMaterialTaken()");
        ammount--;
        //MaterialTakenEvent.Invoke();

    }

    public void OnMaterialTransferStarted()
    {
        // do nothing
    }

    // Start is called before the first frame update
    void Start()
    {
        //if (MaterialTakenEvent == null)
        //    MaterialTakenEvent = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
