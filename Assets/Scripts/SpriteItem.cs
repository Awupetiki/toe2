using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpriteItem : MonoBehaviour, IPointerClickHandler
{
    public int spriteId;
    public int containerId;
    public UnityEvent<int, SpriteItem> pressed;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite)
    {
        if (!_image) return;
        
        _image.sprite = sprite;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("a");
        if (eventData.button == PointerEventData.InputButton.Left)
            pressed.Invoke(0, this);
        else if (eventData.button == PointerEventData.InputButton.Right)
            pressed.Invoke(1, this);
        else if (eventData.button == PointerEventData.InputButton.Middle)
            pressed.Invoke(2, this);
    }
}
