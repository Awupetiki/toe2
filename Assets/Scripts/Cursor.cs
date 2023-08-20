using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cursor : MonoBehaviour
{
    private RigidHandler _currentRigidHandler;
    private Rigidbody2D _currentRigid;
    private TileHandler _currentTile;
    private bool _buildingMode;
    private Camera _mainCamera;
    private TilemapHandler _tilemap;
    private Tilemap _realTilemap;

    private void Start()
    {
        // присваиваем основную камеру в переменную для   f  p  s
        _mainCamera = Camera.main;
        _tilemap = FindObjectOfType<TilemapHandler>();
        _realTilemap = _tilemap.GetComponent<Tilemap>();
    }

    private void Update()
    {
        // телепортируем обджект на позицию мыши
        transform.position = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (!IsHoldingSomething())
        {
            // если курсор ничего не держит и нажато лкм то берём тайл
            if (Input.GetMouseButtonDown(0))
                DoAction();
        }
        else
        {
            // тянем тайл за мышью если лкм зажато
            if (Input.GetMouseButton(0))
            {
                var contextRigid = GetContextRigid();
                if (contextRigid.bodyType != RigidbodyType2D.Dynamic)
                    contextRigid.bodyType = RigidbodyType2D.Dynamic;
                
                contextRigid.velocity /= 2;

                var direction = (Vector2)transform.position - (Vector2)GetContextRigid().transform.position;
                direction *= 7.5f;

                contextRigid.velocity += direction;

                //вращаем по аксе ротейт
                contextRigid.angularVelocity += Input.GetAxisRaw("Rotate")*500f*Time.deltaTime;
            }

            // если лкм не зажато то бросаем тайл
            if (Input.GetMouseButtonUp(0))
            {
                _currentRigid = null;
                _currentRigidHandler = null;
                _currentTile = null;
            }

            // если нажато ктрл то переключаем режим строительства
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (_currentTile)
                    _currentTile.ToggleBuildingMode();
                else
                    _currentRigidHandler.ToggleBuildingMode();
            }
            
            if (_currentRigidHandler && Input.GetKeyDown(KeyCode.F))
                _currentRigidHandler.Activate();
        }
    }

    public void DoAction()
    {
        TryBreakTile();
        
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
            _currentRigidHandler = null;
            _currentRigid = obj.GetComponent<Rigidbody2D>();
            _currentTile = tile;
            return;
        }
        
        var rb = obj.GetComponent<RigidHandler>();
        if (rb)
        {
            _currentRigid = null;
            _currentRigidHandler = rb;
            _currentRigidHandler.tilemap = _realTilemap;
            _currentRigidHandler.Revive();
        }
    }

    private bool TryBreakTile()
    {
        var mousePosition = (Vector2)_mainCamera.ScreenToWorldPoint(Input.mousePosition);
        return _tilemap.TryCreateTileBody(mousePosition);
    }

    private bool IsHoldingSomething() => GetContextRigid() || _currentTile;

    private Rigidbody2D GetContextRigid()
    {
        if (_currentRigid)
            return _currentRigid;
        if (_currentRigidHandler)
            return _currentRigidHandler.GetRigid();
        return null;
    }
}