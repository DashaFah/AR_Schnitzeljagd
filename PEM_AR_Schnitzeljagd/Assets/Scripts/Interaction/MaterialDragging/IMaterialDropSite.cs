using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaterialDropSite
{
    void OnMaterialDropped(IMaterialSiteType type, IMaterialSite materialOrigin);
}
