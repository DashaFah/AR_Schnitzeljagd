using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{

public Vector3 NorthDirection = new Vector3(0,0,0);
public Transform PlayerCamera;
Quaternion MissionDirection;

public RectTransform NorthArrow;
public RectTransform MissionArrow;

//public Transform missionplace;
Vector3 missionposition = new Vector3 (11.592024f, 1000, 48.152585f );


    // Update is called once per frame
    void Update()
    {
    ChangeNorthDirection();
    ChangeMissionDirection();
}

public void ChangeNorthDirection(){

    //NorthDirection.z = PlayerCamera.eulerAngles.y;
    if (Input.compass.enabled) Debug.Log("Compass enabled");
    else Debug.Log("Compass is not enabled");
    NorthDirection.z = Input.compass.trueHeading;
    Debug.Log(NorthDirection.z);

    NorthArrow.localEulerAngles = NorthDirection;
}

public void ChangeMissionDirection()
{
    GPS gps = GPS.Instance;
    //Vector3 PlayerLocation = new Vector3 (11.588878f, 1000, 48.105097f );
    Vector3 PlayerLocation = new Vector3(GPS.Instance.longitude, 1000,  GPS.Instance.latitude);
    //Vector3 dir = missionplace.position - transform.position; 
    //Debug.Log("Current location: " + PlayerLocation.ToString("F4"));

    Vector3 dir = missionposition - PlayerLocation; 

    Debug.Log(dir.ToString("F4") + "=" + PlayerLocation.ToString("F4") + "-" + missionposition.ToString("F4"));

    MissionDirection = Quaternion.LookRotation(-dir, Vector3.up);

    Debug.Log("MissionDirection: " + MissionDirection.ToString("F4"));


    MissionDirection.z = - MissionDirection.y;
    MissionDirection.x = 0;
    MissionDirection.y = 0;

    Debug.Log("MissionDirection2: " + MissionDirection.ToString("F4"));

    var t = MissionDirection.z;

    MissionArrow.localRotation = MissionDirection * Quaternion.Euler(NorthDirection);
    //MissionArrow.localRotation = new Quaternion(MissionDirection.x, MissionDirection.y, MissionDirection.z, MissionDirection.w);
     Debug.Log("MissionArrowLocalRotation: " + MissionArrow.localRotation.ToString("F4"));

        //MissionArrow.localRotation = Quaternion.Euler(0,0,-0.7f);

}
}