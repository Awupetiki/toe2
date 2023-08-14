using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public float speed = 20f;
    public float zoomSpeed = 5f;

    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) * speed * Time.deltaTime);
        // Clamp the zoom value between the min and max zoom levels
        // newZoom = Mathf.Clamp(newZoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize - Input.GetAxis("Mouse ScrollWheel") * zoomSpeed, Time.deltaTime * zoomSpeed);
    }
}
