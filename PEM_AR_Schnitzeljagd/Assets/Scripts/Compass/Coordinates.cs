using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coordinates : MonoBehaviour
{
    public Text LocationCoordinates;

    // Update is called once per frame
    private void Update()
    {
        LocationCoordinates.text = "Lat:" + GPS.Instance.latitude.ToString() + "Lon:" + GPS.Instance.longitude.ToString();
    }
}
