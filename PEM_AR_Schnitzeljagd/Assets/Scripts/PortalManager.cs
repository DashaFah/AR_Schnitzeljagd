using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalManager : MonoBehaviour
{
    public UnityEvent OnTransportEnded;
    public GameObject portalSetting;
    public GameObject tower;
    public GameObject portal;

    private ParticleSystem portalParticleSystem;
    private readonly float waitTime = 3.0f;
    private readonly float disperseTime = 10.0f;
    private readonly float rateOverTimeOnEnter = 200f;
    private readonly float rateOverTimeOnTransported = 0f;


    public void ActivatePortalAndTower()
    {
        portalSetting.SetActive(true);

        tower.SetActive(true);

        portal.SetActive(true);

        ResetPortal();
    }

    public void SetPortalSetting(bool active)
    {
        portalSetting.SetActive(active);
    }

    public void SetTowerState(bool active)
    {
        tower.SetActive(active);
    }

    public void SetPortalState(bool active)
    {
        portal.SetActive(active);
    }

    public void OnEnterPortal()
    {
        var main = portalParticleSystem.main;
        var eModule = portalParticleSystem.emission;
        var solModule = portalParticleSystem.sizeOverLifetime;
        if (eModule.rateOverTimeMultiplier != 0 && eModule.rateOverTimeMultiplier != rateOverTimeOnEnter)
        {
            Debug.Log("OnEnterPortal, rateOverTimeMultiplier="+ eModule.rateOverTimeMultiplier);
            eModule.rateOverTimeMultiplier = rateOverTimeOnEnter;
            solModule.enabled = false;

            main.simulationSpeed = 1f;
            
            StartCoroutine(WaitForPortalTransport());
        }
    }

    private void ResetPortal()
    {
        var main = portalParticleSystem.main;
        var solModule = portalParticleSystem.sizeOverLifetime;
        var eModule = portalParticleSystem.emission;

        main.simulationSpeed = 0.5f;
        solModule.enabled = true;
        eModule.rateOverTimeMultiplier = 20;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (OnTransportEnded == null)
            OnTransportEnded = new UnityEvent();

        portalParticleSystem = portalSetting.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    


    private IEnumerator WaitForPortalTransport()
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Portal Transport finished");

        OnTransportEnded.Invoke();
        var eModule = portalParticleSystem.emission;
        eModule.rateOverTimeMultiplier = rateOverTimeOnTransported;

        StartCoroutine(WaitForPortalDisperse());
    }

    private IEnumerator WaitForPortalDisperse()
    {
        yield return new WaitForSeconds(disperseTime);
        Debug.Log("Portal dispersed.");

        ResetPortal();
        portalSetting.SetActive(false);
    }

}
