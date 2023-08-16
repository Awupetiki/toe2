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
    private bool _changedSprite;
    private Sprite _targetSprite;
    private bool _loadedImage;

    public void SetSprite(Sprite sprite)
    {
        _changedSprite = false;
        _targetSprite = sprite;
    }

    private void Update()
    {
        if (!_loadedImage)
        {
            _image = GetComponent<Image>();
            _loadedImage = true;
        }
        
        if (!_changedSprite)
        {
            _image.sprite = _targetSprite;
            _changedSprite = true;
        }
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
