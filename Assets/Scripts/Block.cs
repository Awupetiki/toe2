using UnityEngine;

public class Block : MonoBehaviour {
    [SerializeField] private string type;
    private bool grabbed = false;
    private bool rigged = false;

    void OnMouseDown() {
        if (rigged) {grabbed = true; return;}
        grabbed = true;
        rigged = true;
        gameObject.AddComponent<Rigidbody2D>();
    }

    void OnMouseUp() {
        if (rigged) {grabbed = false;}
    }

    void FixedUpdate() {
        if (grabbed) {
            GetComponent<Rigidbody2D>().velocity /= 2;
            GetComponent<Rigidbody2D>().velocity += ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position) * 7.5f;
        }
    }

    void OnCollisionStay2D() {
        if (Input.GetKey("f")) {
            Destroy(GetComponent<Rigidbody2D>());
            transform.eulerAngles = Vector3.zero;
            transform.position = new Vector3(Mathf.RoundToInt(transform.position.x),Mathf.RoundToInt(transform.position.y),0f);
            grabbed = false;
            rigged = false;
        }
    }
}
