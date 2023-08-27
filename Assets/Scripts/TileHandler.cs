using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class TileHandler : MonoBehaviour
{
    public bool buildingMode;
    public bool additionalMode;

    private void OnCollisionStay2D(Collision2D other)
    {
        // ретурним если режим строительства выключен
        if (!buildingMode) {
            return;
        }
        
        // пробуем взять компонент тайлмапа
        var tilemapHandler = other.gameObject.GetComponent<TilemapHandler>();
        // если эта коллизия это тайлмап то ставим этот обджект
        if (tilemapHandler)
        {
            tilemapHandler.PlaceTile(this);
        }
    }
    
    public void ToggleBuildingMode() => buildingMode = !buildingMode;
    public void ToggleAdditionalMode() => additionalMode = !additionalMode;
}