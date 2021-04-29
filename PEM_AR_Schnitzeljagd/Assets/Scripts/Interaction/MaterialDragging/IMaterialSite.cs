using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaterialSite
{
    IMaterialSiteType getSiteType();
    void OnMaterialTransferedSuccessfullyFinished();
    void OnMaterialTransferStarted();
    void OnMaterialTransferCanceled();
}
