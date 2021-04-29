using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//public struct RebuildStageRequirement { }

public class TowerRebuildController : MonoBehaviour
{
    public GameObject Tower;
    public GameObject RebuildSetting;
    public UnityEvent OnTowerRebuildFinished;

    public int BuildStage = 0;
    public RebuildStage[] Stages = new RebuildStage[0];

    public bool debbugBeginInteractionOnStart = true;

    // Start is called before the first frame update
    void Start()
    {
        if (OnTowerRebuildFinished == null)
            OnTowerRebuildFinished = new UnityEvent();
        if (debbugBeginInteractionOnStart)
            BeginRebuildInteraction();

        // debug only
        OnTowerRebuildFinished.AddListener(OnDebugRebuildFinishedListener);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginRebuildInteraction()
    {
        RebuildSetting.SetActive(true);
        Tower.SetActive(true);
        ResetRebuildInteraction();
    }

    public void StopRebuildInteraction()
    {
        RebuildSetting.SetActive(false);
        ResetRebuildInteraction();
    }

    public void ResetRebuildInteraction()
    {
        BuildStage = 0;
        foreach (var stage in Stages)
        {
            foreach (var stagePart in stage.go_RebuildPart)
            {
                stagePart.SetActive(false);
            }
        }
    }


    public void EnableStage(int stage)
    {
        if (Stages.Length > BuildStage)
        {
            foreach (var stagePart in Stages[BuildStage].go_RebuildPart)
            {
                if (!stagePart.activeSelf)
                {
                    stagePart.SetActive(true);
                }
            }
        }
    }


    public void OnMaterialDropped(IMaterialSiteType type, IMaterialSite materialOrigin)
    {
        Debug.Log(string.Format("#### Material dropped {0}", type));

        if (Stages.Length > BuildStage)
        {
            Debug.Log(string.Format("dropped material {0} - currentStage {1} - require {2}", type, BuildStage, Stages[BuildStage].Requirement));
            if (Stages[BuildStage].Requirement == type)
            {
                Debug.Log(string.Format("met requirement"));
                // ok
                EnableStage(BuildStage);
                BuildStage++;
            }
            if (Stages.Length <= BuildStage)
            {
                OnTowerRebuildFinished.Invoke();
            }
        }
    }

    public void SimulateDropMaterialPillar()
    {
        Debug.Log("SimulateDropMaterialPillar");
        OnMaterialDropped(IMaterialSiteType.Pillar, null);
    }
    public void SimulateDropMaterialRoof()
    {
        Debug.Log("SimulateDropMaterialRoof");
        OnMaterialDropped(IMaterialSiteType.Roof, null);
    }
    public void SimulateDropMaterialSpire()
    {
        Debug.Log("SimulateDropMaterialSpire");
        OnMaterialDropped(IMaterialSiteType.Tower_spire, null);
    }


    public void OnDebugRebuildFinishedListener()
    {
        Debug.Log("OnDebugRebuildFinishedListener()");
    }
}
