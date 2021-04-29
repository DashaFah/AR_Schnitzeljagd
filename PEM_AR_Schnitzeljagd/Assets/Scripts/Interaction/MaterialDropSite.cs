using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialDropSite : MonoBehaviour, IMaterialDropSite
{
    public void OnMaterialDropped(IMaterialSiteType type, IMaterialSite materialOrigin)
    {
        Debug.Log("MaterialDropSite.OnMaterialDropped(): " + type);
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
