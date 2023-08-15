using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RigidHandler : MonoBehaviour
{
    public bool buildingMode;
    public bool additionalMode;
    public Tilemap tilemap;
    private Rigidbody2D _rb;

    // Start is called before the first frame update
    protected void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected void OnCollisionStay2D(Collision2D other)
    {
        // скипаем если режим строительства выключен
        if (!buildingMode) return;
        var position = transform.position;
        var gridPosition = new Vector3(Mathf.Round(position.x), Mathf.Round(position.y), Mathf.Round(position.z)) -
                           new Vector3(0.5f, 0.5f);
        if (tilemap.GetTile<Tile>(tilemap.WorldToCell(gridPosition)))
            return;

        _rb.bodyType = RigidbodyType2D.Static;
        
        transform.position = gridPosition;
        transform.rotation = Quaternion.identity;

        buildingMode = false;
        tilemap.SetTile(tilemap.WorldToCell(transform.position), null);
    }

    public void ToggleBuildingMode()
    {
        buildingMode = !buildingMode;
    }

    public void ToggleAdditionalMode()
    {
        additionalMode = !additionalMode;
    }

    public virtual void Revive()
    {
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public virtual void Activate()
    {
        
    }

    public Rigidbody2D GetRigid()
    {
        return _rb;
    }

    public static implicit operator Rigidbody2D(RigidHandler handler)
    {
        return handler._rb;
    }
}