using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private Rigidbody2D _currentRigid;
    private TileHandler _currentTile;
    private Camera _mainCamera;

    private void Start()
    {
        // присваиваем основную камеру в переменную для   f  p  s
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        // телепортируем обджект на позицию мыши
        transform.position = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        if (!IsHoldingSomething())
        {
            // если курсор ничего не держит и нажато лкм то берём тайл
            if (Input.GetMouseButtonDown(0))
                GrabTile();
        }
        else
        {
            // тянем тайл за мышью если лкм зажато
            if (Input.GetMouseButton(0))
            {
                _currentRigid.velocity /= 2;

                var direction = (Vector2)transform.position - (Vector2) _currentRigid.transform.position;
                direction *= 7.5f;
            
                _currentRigid.velocity += direction;
            }
            
            // если лкм не зажато то бросаем тайл
            if (Input.GetMouseButtonUp(0))
            {
                _currentTile.ToggleGrab();
                _currentRigid = null;
                _currentTile = null;
            }
            
            // если нажато ктрл то переключаем режим строительства
            if (Input.GetKeyDown(KeyCode.LeftControl))
                _currentTile.TogglePlacingMode();
        }
        
    }

    public void GrabTile()
    {
        // пробуем сделать рейкаст что бы получить обджекты под мышью
        var hit = Physics2D.Raycast(transform.position, transform.forward);
        // если ничего нету то скипаем
        if (!hit) return;
        
        // берём обджект который попался на рейкаст
        var obj = hit.transform.gameObject;
        
        // пробуем взять компонент тайла, если получится то берём тайл в руку
        var tile = obj.GetComponent<TileHandler>();
        if (tile)
        {
            _currentRigid = obj.GetComponent<Rigidbody2D>();
            _currentTile = tile;
            _currentTile.ToggleGrab();
        }
    }

    private bool IsHoldingSomething() => _currentRigid || _currentTile;
}
