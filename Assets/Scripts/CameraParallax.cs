using UnityEngine;

public class CameraParallax : MonoBehaviour {
    [SerializeField] private float modifier;
    [SerializeField] private float sizeMod;
    private float temp;
    
    void Update() {
        transform.localPosition = new Vector3(0f, Camera.main.transform.position.y * -modifier, 0f);
        temp = Camera.main.orthographicSize * sizeMod;
        transform.localScale = new Vector3(temp,temp,temp);
    }
}
