using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YearBehaviourScript : MonoBehaviour
{
    public Text year;
    // Start is called before the first frame update
    void Start()
    {
        if (year == null)
        {
            var gameObject = GameObject.Find("Year");
            year = gameObject.GetComponent<Text>();
        }

    }

    public void ChangeYear(string sceneYear)
    {
        year.text = sceneYear;
    }
}