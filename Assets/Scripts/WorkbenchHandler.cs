using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchHandler : RigidHandler
{
    [SerializeField] private GameObject ui;

    private new void Start()
    {
        base.Start();
        ui.GetComponent<Canvas>().worldCamera = Camera.main;
    }
    
    private void Update()
    {
        ui.transform.position = transform.position + Vector3.up * 3f;
    }

    public override void Activate()
    {
        ui.SetActive(!ui.activeInHierarchy);
    }
}
