using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteMaterialSite : MonoBehaviour, IMaterialSite
{
    public IMaterialSiteType SiteType;

    public IMaterialSiteType getSiteType()
    {
        return SiteType;
    }

    public void OnMaterialTransferCanceled()
    {
        Debug.Log("IMaterialSiteType.onMaterialCanceled()");
    }

    public void OnMaterialTransferedSuccessfullyFinished()
    {
        Debug.Log("IMaterialSiteType.onMaterialTaken()");
    }

    public void OnMaterialTransferStarted()
    {
        // infinite site do nothing
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
