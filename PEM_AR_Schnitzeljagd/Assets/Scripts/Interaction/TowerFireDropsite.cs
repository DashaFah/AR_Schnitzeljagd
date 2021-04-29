using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFireDropsite : MonoBehaviour, IMaterialDropSite
{

    public FireExtinguishManager controller;
    // TODO add this script to the flame 
    // handle deactivation either here or in... 

    public void OnMaterialDropped(IMaterialSiteType type, IMaterialSite materialOrigin)
    {
        Debug.Log("TowerFireDropsite.OnMaterialDropped: IMaterialSiteType = " + type + " IMaterialSite = " + materialOrigin);

        gameObject.SetActive(false);
        Debug.Log("Fire extinguished = " + gameObject.name);

        controller?.OnMaterialDropped(type, materialOrigin);
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

