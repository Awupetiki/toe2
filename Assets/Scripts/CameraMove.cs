using UnityEngine;

public class CameraMove : MonoBehaviour {
    [SerializeField] private float speed;
    private Rigidbody2D rig;

    void Awake() {
        rig = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        rig.velocity += new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"))*speed;
    }
}
