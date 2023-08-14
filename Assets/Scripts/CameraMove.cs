using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float speed;

    void Update()
    {
        transform.position +=
            new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * (speed * Time.deltaTime);
    }
}