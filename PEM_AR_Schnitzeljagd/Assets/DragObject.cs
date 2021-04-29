using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragObject : MonoBehaviour
{
    public Image imgCenter;
    public Image MaterialType;

    public void SetProperties(Sprite materialTypeSprite)
    {
        MaterialType.sprite = materialTypeSprite;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
