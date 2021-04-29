using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassManager : MonoBehaviour
{

    bool CompassOpened = false;
    public GameObject compass;


    public void ToggleCompass()
    {
        if (CompassOpened) 
        {
            destroyCompass();            
        }
        else init();
        CompassOpened = !CompassOpened;
    }

    //public Sprite CompassBackground;
    public Sprite NorthArrowSprite;
    public Sprite MissionArrowSprite;   

    public Transform Camera;
    

    void init()
    {
       
       /*GameObject canvas = GameObject.Find("Canvas");
       GameObject CompassObj = new GameObject("CompassBackground");
       //Image NewImage = CompassObj.AddComponent<Image>(); 
       //NewImage.sprite = CompassBackground;
       CompassObj.AddComponent<RectTransform>(); 
       RectTransform RectTransformCompassObj = CompassObj.GetComponent<RectTransform>();
       Debug.Log("Created");
        RectTransformCompassObj.SetParent(canvas.transform);
        CompassObj.SetActive(true);
        RectTransformCompassObj.anchoredPosition = transform.parent.position;
        RectTransformCompassObj.anchorMin = new Vector2(0, 0);
        RectTransformCompassObj.anchorMax = new Vector2(0, 0);
        Vector3 coors = new Vector3(0,0,0);
        RectTransformCompassObj.position = coors;
        Vector2 pivot = new Vector2(0,0);
        RectTransformCompassObj.pivot = pivot;
        RectTransformCompassObj.localScale = new Vector3 (10,10,10); //Here change size of compass
        

        //Create NorthArrow

        GameObject NorthArrow = new GameObject("NorthArrow");
        Image NewImage2 = NorthArrow.AddComponent<Image>();
        NewImage2.sprite = NorthArrowSprite;
        RectTransform RectTransformNorthArrow = NorthArrow.GetComponent<RectTransform>();
        RectTransformNorthArrow.SetParent(CompassObj.transform);
        Vector2 pivot2 = new Vector2(0.5f,0.5f);
        RectTransformNorthArrow.pivot = pivot2;
        Vector3 coors2 = new Vector3(CompassObj.GetComponent<RectTransform>().rect.width / 2, CompassObj.GetComponent<RectTransform>().rect.height / 2, 0);
        RectTransformNorthArrow.localPosition = coors2;
        Vector3 scale2 = new Vector3(1,1,1);
        RectTransformNorthArrow.localScale = scale2;

        //Create MissionArrow

        GameObject MissionArrow = new GameObject("MissionArrow");
        Image NewImage3 = MissionArrow.AddComponent<Image>();
        NewImage3.sprite = MissionArrowSprite;
        RectTransform RectTransformMissionArrow = MissionArrow.GetComponent<RectTransform>();
        RectTransformMissionArrow.SetParent(CompassObj.transform);
        
        Vector3 coors3 = new Vector3(CompassObj.GetComponent<RectTransform>().rect.width / 2, CompassObj.GetComponent<RectTransform>().rect.height / 2, 0);
        RectTransformMissionArrow.localPosition = coors3;
        //RectTransformMissionArrow.localRotation = Quaternion.Euler(0,0,180);
        Vector3 scale = new Vector3(1,1,1);
        RectTransformMissionArrow.localScale = scale;
        Vector2 pivot3 = new Vector2(0.5f,0.5f);
        RectTransformMissionArrow.pivot = pivot3;


        var compassScript = CompassObj.AddComponent<Compass>(); 
        compassScript.NorthArrow = RectTransformNorthArrow;
        compassScript.MissionArrow = RectTransformMissionArrow;
        // = GameObject.Find("ARCamera").GetComponent<Transform>();
        compassScript.PlayerCamera = Camera;*/

        compass.SetActive(true);
    }

    void destroyCompass()
    {
        /*GameObject CompassObj = GameObject.Find("CompassBackground");
        Destroy(CompassObj);*/
        compass.SetActive(false);
    }
}
