using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_Material_Dropsite : MonoBehaviour, IMaterialDropSite
{

    public TowerRebuildController controller;


    public void OnMaterialDropped(IMaterialSiteType type, IMaterialSite materialOrigin)
    {
        controller.OnMaterialDropped(type, materialOrigin);
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
