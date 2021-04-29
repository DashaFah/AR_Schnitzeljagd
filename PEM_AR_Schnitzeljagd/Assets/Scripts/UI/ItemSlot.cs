using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public Image itemImageElement;
    public Text itemNameElement;
    public string defaultText = "";
    public Color hasSpriteColor = new Color(255, 255, 255, 255);
    public Color noSpriteColor = new Color(255, 255, 255, 0);

    public void UpdateName(string newName)
    {
        itemNameElement.text = newName;
    }

    public void UpdateSprite(Sprite newSprite)
    {
        itemImageElement.sprite = newSprite;
        itemImageElement.color = hasSpriteColor;
    }
    public void ClearSlot()
    {
        itemNameElement.text = defaultText;
        itemImageElement.sprite = null;
        itemImageElement.color = noSpriteColor;
    }

    private void Awake()
    {
        ClearSlot();
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
