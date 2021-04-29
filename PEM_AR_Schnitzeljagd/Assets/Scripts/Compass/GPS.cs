using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;


public class GPS : MonoBehaviour
{
    public float latitude;
    public float longitude;

    public bool successful;

    public static GPS Instance {set; get;}


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        latitude = 200;
        longitude = 200;


        //Debug.Log("Getting the location");
        

        /* if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
            dialog = new GameObject();
        }
*/

        StartCoroutine(StartLocationService());
    }

    private IEnumerator StartLocationService()
    {
        if (!Input.location.isEnabledByUser)
        {
            Debug.Log("User has not enabled GPS");
            yield break;
        }

        Input.compass.enabled = true;

        Input.location.Start(10, 0.1f);
        if (Input.location.status == LocationServiceStatus.Running) 
        { 
            Debug.Log("Location Service Running");
        }
        int maxWait = 20;

        while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        if (maxWait <= 0)
        {
            Debug.Log("Time out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("Unable to determune device location");
            yield break;
        }

        successful = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (successful)
        {
            Debug.Log("Updating the location");

            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            Debug.Log(latitude);

            Debug.Log(longitude);


        }

    }
}
