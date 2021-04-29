using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchColorChange : MonoBehaviour, ITouchable
{
    Renderer rend;
    Color originalColor;
    public Color Color_touch_down = Color.yellow;
    private Color Color_touch_stay = Color.cyan;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTouchDown()
    {
        rend.material.color = Color_touch_down;
    }

    public void OnTouchExit()
    {
        OnTouchUp();
    }

    public void OnTouchStay()
    {
        rend.material.color = Color_touch_stay;
    }

    public void OnTouchUp()
    {
        rend.material.color = originalColor;

    }
}
