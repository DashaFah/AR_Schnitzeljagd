using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucketMaterialSite : MonoBehaviour, IMaterialSite
{
    private IMaterialSiteType SiteType = IMaterialSiteType.Water;
    public int Ammount = 1;

    public IMaterialSiteType getSiteType()
    {
        return SiteType;
    }

    public void OnMaterialTransferCanceled()
    {
        Debug.Log("IMaterialSiteType.onMaterialCanceled()");
        Ammount++;

        if (Ammount >= 0)
        {
            gameObject.SetActive(true);
        }
    }

    public void OnMaterialTransferedSuccessfullyFinished()
    {
        Debug.Log("IMaterialSiteType.OnMaterialSuccessfullyTransferedToDropsite()");
        // TODO deactivate

    }

    public void OnMaterialTransferStarted()
    {
        Debug.Log("IMaterialSiteType.OnMaterialTransferStarted()");
        Ammount--;

        if (Ammount == 0)
        {
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
