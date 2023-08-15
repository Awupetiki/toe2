using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkbenchHandler : RigidHandler
{
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject spriteGrid;
    [SerializeField] private GameObject spriteItemPrefab;
    [SerializeField] private int maxItemCount = 10;
    private List<GameObject> _items;
    private PrefabStorage _storage;
    private int storedItemCount;

    private new void Start()
    {
        base.Start();
        ui.GetComponent<Canvas>().worldCamera = Camera.main;

        _items = new List<GameObject>();
        _storage = new PrefabStorage();
        
        beingStored.AddListener(BeingStoredCallback);
    }
    
    private void Update()
    {
        ui.transform.position = transform.position + Vector3.up * 3f;
    }

    private void BeingStoredCallback()
    {
        ui.SetActive(false);
    }

    public override void Activate()
    {
        ui.SetActive(!ui.activeInHierarchy);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.rigidbody || Mathf.Abs(other.rigidbody.angularVelocity) < 16)
            return;
        
        Debug.Log(other.rigidbody.angularVelocity);

        if (storedItemCount >= maxItemCount)
            return;
        
        var sprite = other.gameObject.GetComponent<SpriteRenderer>();
        if (sprite)
        {
            var rigidHandler = other.gameObject.GetComponent<RigidHandler>();
            if (rigidHandler)
                rigidHandler.beingStored.Invoke();
            
            int index = _items.Count;
            _items.Add(other.gameObject);
            int containerId = _storage.Push(other.gameObject);

            var spriteItem = Instantiate(spriteItemPrefab, spriteGrid.transform, false);
            var itemComp = spriteItem.GetComponent<SpriteItem>();
            itemComp.SetSprite(sprite.sprite);
            itemComp.spriteId = index;
            itemComp.containerId = containerId;
            itemComp.pressed.AddListener(SpriteItemCallback);

            storedItemCount++;
        }
    }

    private void SpriteItemCallback(int mouseButton, SpriteItem item)
    {
        if (mouseButton == 1)
        {
            var go = _storage.Pop(item.containerId);
            _items[item.spriteId] = null;

            go.transform.position = transform.position + Vector3.up * 1.5f;
            Destroy(item.gameObject);

            storedItemCount--;
        }
    }
}
