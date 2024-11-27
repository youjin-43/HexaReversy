using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float panSpeed = 24f;
    public float zoomSpeed = 10f;

    void Start()
    {
        Camera.main.fieldOfView = 11;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Camera.main.fieldOfView += (zoomSpeed * Input.GetAxis("Mouse ScrollWheel"));
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 5, 20);
        }
    }
}
