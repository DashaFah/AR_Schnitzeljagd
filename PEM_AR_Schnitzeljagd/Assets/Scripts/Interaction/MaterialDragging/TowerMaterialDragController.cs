using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerMaterialDragController : MonoBehaviour
{
    public LayerMask touchInputMask;
    private List<GameObject> touchList = new List<GameObject>();
    private RaycastHit hit;

    private bool isDraggingObject;
    private int dragFingerID;
    private IMaterialSiteType currentMaterialSiteType;
    private IMaterialSite currentMaterialSite;

    //public GameObject dbgObject;
    private GameObject currentDragObject;
    public GameObject pillarDragObjectPrefab;

    public Sprite waterSprite;
    public Sprite pillarSprite;
    public Sprite roofSprite;
    public Sprite towerSpireSprite;
    public GameObject DragCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    int cnt_down = 0;
    int cnt_up = 0;
    int cnt_stay = 0;
    int cnt_exit = 0;
    int cnt_old_exit = 0;

    private void ReplentishMaterialSite()
    {
        Debug.Log("## ReplentishMaterialSite");

        currentMaterialSite?.OnMaterialTransferCanceled();
    }
    private void ResetDragState()
    {
        isDraggingObject = false;
        dragFingerID = -1;
        currentMaterialSiteType = IMaterialSiteType.Undefined;
        currentMaterialSite = null;

        Destroy(currentDragObject);
        currentDragObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);    //attached the main camera

                        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && Physics.Raycast(ray, out hit, 100f, touchInputMask.value))
                        {
                            GameObject recipient = hit.transform.gameObject;

                            Collider c = hit.collider;
                            IMaterialSite materialSite = c.gameObject.GetComponent<IMaterialSite>();

                            if (materialSite != null)
                            {
                                Debug.Log("Material Site Found!");
                                if (!isDraggingObject)
                                {
                                    Debug.Log("Start dragging! fingerId: " + touch.fingerId);
                                    Debug.Log("## position touch: " + touch.position);

                                    dragFingerID = touch.fingerId;
                                    currentMaterialSite = materialSite;
                                    currentMaterialSiteType = materialSite.getSiteType();

                                    // TODO create follow object
                                    isDraggingObject = true;
                                    var foo = Instantiate(pillarDragObjectPrefab);

                                    DragObject dragObject = foo.GetComponent<DragObject>();
                                    SetDragObjectProperties(dragObject, currentMaterialSiteType);


                                    RectTransform rt = foo.GetComponent<RectTransform>();
                                    rt.transform.SetParent(DragCanvas.transform, false);
                                    //Debug.Log("## local position foo : " + rt.localPosition);
                                    //Debug.Log("## position foo : " + rt.position);
                                    //Debug.Log("## anchored position foo : " + rt.anchoredPosition);
                                    rt.position = new Vector2(touch.position.x, touch.position.y);

                                    currentMaterialSite.OnMaterialTransferStarted();
                                    currentDragObject = foo;
                                }
                            }
                        }
                        cnt_down++;
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        if (isDraggingObject && touch.fingerId == dragFingerID)
                        {
                            //Debug.Log("dragging");
                            //dbgObject.gameObject.GetComponent<Renderer>().material.color = Color.yellow;


                            RectTransform rt = currentDragObject.GetComponent<RectTransform>();
                            rt.position = new Vector2(touch.position.x, touch.position.y);
                        }
                        cnt_stay++;
                        break;
                    case TouchPhase.Ended:
                        // check if target was hit
                        if (touch.fingerId == dragFingerID && isDraggingObject)
                        {
                            // drag finger was released

                            Ray touch_end_ray = GetComponent<Camera>().ScreenPointToRay(touch.position);    //attached the main camera

                            Debug.Log("## finger was released: " + touch.fingerId);
                            // why not use EventSystem.current.IsPointerOverGameObject(touch.fingerId) ?
                            // there is a bug: IsPointerOverGameObject will always return false for IsPointerOverGameObject() in TouchPhase.Ended

                            Destroy(currentDragObject);
                            currentDragObject = null;

                            //if (IsPointerOverUIObject(touch.position))
                            //{
                            //    Debug.Log("## IsPointerOverUIObject: is over UI ## ##");
                            //}
                            //else
                            //{
                            //    Debug.Log("## IsPointerOverUIObject: over scene");
                            //}

                            // usage of IsPointerOverUIObject is a workaround for an unity bug
                            // FIXME use EventSystem.current.IsPointerOverGameObject(fingerId) as soon as its fixed
                            bool dropSuccess = false;
                            if (!IsPointerOverUIObject(touch.position) && Physics.Raycast(touch_end_ray, out hit, 100f, touchInputMask.value))
                            {
                                GameObject recipient = hit.transform.gameObject;

                                Collider c = hit.collider;
                                IMaterialDropSite dropSite = c.gameObject.GetComponent<IMaterialDropSite>();

                                if (dropSite != null)
                                {
                                    Debug.Log("## Drop Site Found!");
                                    dropSuccess = true;
                                    currentMaterialSite.OnMaterialTransferedSuccessfullyFinished();
                                    dropSite.OnMaterialDropped(currentMaterialSiteType, currentMaterialSite);
                                }
                            }
                            if(!dropSuccess)
                            {
                                ReplentishMaterialSite();
                            }

                            ResetDragState();
                        }

                        cnt_up++;
                        break;
                    case TouchPhase.Canceled:
                        if (touch.fingerId == dragFingerID && isDraggingObject)
                        {
                            // drag finger was released
                            ReplentishMaterialSite();
                        }

                        ResetDragState();
                        cnt_exit++;
                        break;
                }
            }
        }
    }

    /// <summary>
    /// why not use EventSystem.current.IsPointerOverGameObject(touch.fingerId) ?
    /// there is a bug: IsPointerOverGameObject will always return false for IsPointerOverGameObject() in TouchPhase.Ended
    /// 
    /// https://forum.unity.com/threads/ispointerovereventsystemobject-always-returns-false-on-mobile.265372/#post-1876138
    /// 
    /// 
    /// Cast a ray to test if screenPosition is over any UI object in canvas. This is a replacement
    /// for IsPointerOverGameObject() which does not work on Android in 4.6.0f3
    /// </summary>
    private bool IsPointerOverUIObject(Vector2 screenPosition)
    {
        // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
        // the ray cast appears to require only eventData.position.
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    private void SetDragObjectProperties(DragObject dragObject, IMaterialSiteType currentMaterialSiteType)
    {
        Sprite s = null;
        switch (currentMaterialSiteType)
        {
            case IMaterialSiteType.Undefined:
                return;
            case IMaterialSiteType.Pillar:
                s = pillarSprite;
                break;
            case IMaterialSiteType.Roof:
                s = roofSprite;
                break;
            case IMaterialSiteType.Tower_spire:
                s = towerSpireSprite;
                break;
            case IMaterialSiteType.Water:
                s = waterSprite;
                break;
        }
        dragObject.SetProperties(s);
    }
}
