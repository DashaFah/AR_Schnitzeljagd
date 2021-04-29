using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireExtinguishManager : MonoBehaviour
{

    public GameObject Tower;
    public GameObject FireSetting;
    public UnityEvent OnTowerFireFinished;
    public bool debbugBeginInteractionOnStart = false;

    public GameObject[] Fires;
    public GameObject[] Buckets;

    public void BeginInteraction()
    {
        FireSetting.SetActive(true);
        Tower.SetActive(true);
        SetAllFiresState(true);
        SetAllBucketsState(true);
    }

    public void HideFireInteraction()
    {
        FireSetting.SetActive(false);
        Tower.SetActive(false);
        StopInteraction();
    }

    private void StopInteraction()
    {
        SetAllFiresState(false);
        SetAllBucketsState(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (OnTowerFireFinished == null)
            OnTowerFireFinished = new UnityEvent();

        if (debbugBeginInteractionOnStart)
            BeginInteraction();

        // debug only
        OnTowerFireFinished.AddListener(OnDebugFireExtinguishFinishedListener);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMaterialDropped(IMaterialSiteType type, IMaterialSite materialOrigin)
    {
        Debug.Log("FireExtinguishManager.OnMaterialDropped: IMaterialSiteType = " + type + " IMaterialSite = " + materialOrigin);

        // TODO determine site and deactivate it.
        if (type != IMaterialSiteType.Water)
            return;
        
        if (WinConditionReached())
            OnTowerFireFinished.Invoke();

    }

    public bool WinConditionReached()
    {
        Debug.Log("FireExtinguishManager.WinConditionReached");
        Debug.Log("Fires.Length: " + Fires.Length);
        foreach (var fire in Fires)
        {
            if (fire.activeSelf)
                return false;
        }

        Debug.Log("All " + Fires.Length + " fires are extinguished.");
        StopInteraction();

        return true;
    }
    //public void SimulateDropWater(int siteIndex)
    //{
    //}

    public void OnDebugFireExtinguishFinishedListener()
    {
        Debug.Log("OnDebugFireExtinguishFinishedListener()");
    }

    private void SetAllFiresState(bool active)
    {
        Fires = GameObject.FindGameObjectsWithTag("Fire");

        foreach (var fire in Fires)
        {
            // TODO check if it is more performance friendly to instantiate fires instead of keeping them the whole time deactivated
            fire.SetActive(active);
        }
    }

    private void SetAllBucketsState(bool active)
    {
        Buckets = GameObject.FindGameObjectsWithTag("Bucket");

        foreach (var bucket in Buckets)
        {
            bucket.SetActive(active);
        }
    }

}
