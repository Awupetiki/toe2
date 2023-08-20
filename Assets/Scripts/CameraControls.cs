using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraControls : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float smoothingLevel;
    private Vector3 _targetPosition;
    private float _targetZoom;
    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _targetZoom = _camera.orthographicSize;
        _targetPosition = _camera.transform.position;
    }

    private void Update()
    {
        _targetPosition +=
            new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * (speed * (Camera.main.orthographicSize/4f) * Time.deltaTime);
        _targetPosition.z = -10;

        var scrollDelta = Input.mouseScrollDelta.y;
        _targetZoom -= scrollDelta / 2;
        if (_targetZoom < 1) _targetZoom = 1;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, smoothingLevel);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _targetZoom, smoothingLevel);
    }
}