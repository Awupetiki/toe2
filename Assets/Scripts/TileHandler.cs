using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class TileHandler : MonoBehaviour
{
    private bool _buildingModeEnabled;
    private bool _grabbed;

    private void OnCollisionStay2D(Collision2D other)
    {
        // скипаем если режим строительства выключен
        if (!_buildingModeEnabled) return;
        
        // пробуем взять компонент тайлмапа
        var tilemapHandler = other.gameObject.GetComponent<TilemapHandler>();
        // если эта коллизия это тайлмап то ставим этот обджект
        if (tilemapHandler)
        {
            tilemapHandler.PlaceTile(this);
        }
    }

    public void ToggleGrab() => _grabbed = !_grabbed;
    public void TogglePlacingMode() => _buildingModeEnabled = !_buildingModeEnabled;
}