using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchController : MonoBehaviour
{

    public LayerMask touchInputMask;
    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    //public Text touchDown;
    //public Text touchUp;
    //public Text touchStay;
    //public Text touchExit;
    //public Text touchOldExit;

    int cnt_down = 0;
    int cnt_up = 0;
    int cnt_stay = 0;
    int cnt_exit = 0;
    int cnt_old_exit = 0;
    //void updateTexts()
    //{
    //    touchDown.text = "down: " + cnt_down;
    //    touchUp.text = "up: " + cnt_up;
    //    touchStay.text = "stay: " + cnt_stay;
    //    touchExit.text = "exit: " + cnt_exit;
    //    touchOldExit.text = "toldexit: " + cnt_old_exit;
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touchesOld = new GameObject[touchList.Count];
            touchList.CopyTo(touchesOld);
            touchList.Clear();

            foreach (Touch touch in Input.touches)
            {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);    //attached the main camera

                if (Physics.Raycast(ray, out hit, 100f, touchInputMask.value))
                {
                    GameObject recipient = hit.transform.gameObject;

                    Collider c = hit.collider;
                    ITouchable toInteract = c.gameObject.GetComponent<ITouchable>();


                    if (toInteract != null)
                    {
                        touchList.Add(recipient);
                    }

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            toInteract?.OnTouchDown();
                            cnt_down++;
                            break;
                        case TouchPhase.Moved:
                        case TouchPhase.Stationary:
                            toInteract?.OnTouchStay();
                            cnt_stay++;
                            break;
                        case TouchPhase.Ended:
                            toInteract?.OnTouchUp();
                            cnt_up++;
                            break;
                        case TouchPhase.Canceled:
                            toInteract?.OnTouchExit();
                            cnt_exit++;
                            break;
                    }
                }
            }

            foreach (GameObject g in touchesOld)
            {
                if (!touchList.Contains(g))
                {
                    g.gameObject.GetComponent<ITouchable>()?.OnTouchExit();
                    cnt_old_exit++;
                }
            }
            //updateTexts();
        }
    }
}
