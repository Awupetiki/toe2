using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

[RequireComponent(typeof(Tilemap))]
public class TilemapHandler : MonoBehaviour
{
    // сделал приватным что бы риксед завалил еба-
    [Tooltip("трансформ в котором будут храниться тайлы")] [SerializeField]
    private Transform tileContainer;

    [SerializeField] private Cursor cursor;
    private Tilemap _tilemap;

    private void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    /// <summary>
    /// устанавливает обджект тайла в тайлмапе
    /// </summary>
    /// <param name="tileObject">обджект тайла (управляемого игроком)</param>
    public void PlaceTile(TileHandler tileObject)
    {
        // переводим позицию тайла в тайлмаповскую
        var tilemapPosition = _tilemap.WorldToCell(tileObject.transform.position);

        // переносим данные из обджекта в тайл
        var tile = ScriptableObject.CreateInstance<Tile>();
        tile.name = tileObject.name;
        tile.sprite = tileObject.GetComponent<SpriteRenderer>().sprite;
        tile.color = Color.white;
        tile.colliderType = Tile.ColliderType.Sprite;

        // ставим тайл
        _tilemap.SetTile(tilemapPosition, tile);

        // удаляем обджект
        Destroy(tileObject.gameObject);
    }

    public bool TryCreateTileBody(Vector3 worldPosition)
    {
        var tilemapPosition = _tilemap.WorldToCell(worldPosition);

        // получаем тайл на позиции из параметра
        var tile = _tilemap.GetTile<Tile>(tilemapPosition);
        // переводим позицию в тайлмапе в мировую
        var worldGridPosition = _tilemap.CellToWorld(tilemapPosition);

        // игнорим если тут пустой тайл
        if (!tile) return false;

        TryBreakFlowerAbove(tilemapPosition);

        if (tile is PrefabTile prefabTile)
        {
            // var prefabTile = (PrefabTile)tile;

            Instantiate(
                prefabTile.prefab,
                worldGridPosition,
                Quaternion.identity,
                tileContainer
            );

            _tilemap.SetTile(tilemapPosition, null);

            return true;
        }

        // если у тайла нету коллизии (тоесть, цветы), просто ломаем его
        if (tile.colliderType == Tile.ColliderType.None)
        {
            _tilemap.SetTile(tilemapPosition, null);
            return true;
        }

        var tileObject = new GameObject(tile.name);

        // ставим родительский трансформ на tileContainer
        tileObject.transform.parent = tileContainer;
        // переносим на мировую позицию, которую мы получили ранее
        tileObject.transform.position = worldGridPosition + new Vector3(0.5f, 0.5f, 0);

        // создаём SpriteRenderer и ставим спрайт на спрайт из тайлмапа
        tileObject.AddComponent<SpriteRenderer>().sprite = tile.sprite;

        // создаём Rigidbody2D и BoxCollider2D для физики
        tileObject.AddComponent<BoxCollider2D>();
        tileObject.AddComponent<Rigidbody2D>();

        // создаём TileHandler для управления тайлом (перенос, установка, и.т.д)
        // и берём его в обдро- руки
        tileObject.AddComponent<TileHandler>();

        // "ломаем" тайл на тайлмапе
        _tilemap.SetTile(tilemapPosition, null);
        return true;
    }

    private void TryBreakFlowerAbove(Vector3Int centerPosition)
    {
        var tile = _tilemap.GetTile<Tile>(centerPosition + Vector3Int.up);
        if (tile && tile.colliderType == Tile.ColliderType.None)
            _tilemap.SetTile(centerPosition + Vector3Int.up, null);
    }
}