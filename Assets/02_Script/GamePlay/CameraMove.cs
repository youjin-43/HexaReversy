using UnityEngine;

public class CameraMove : MonoBehaviour
{
    float panSpeed = 2f;
    float zoomSpeed = 10f;

    void Start()
    {
        Camera.main.fieldOfView = 11;
        Camera.main.transform.position = new Vector3(0, 90, 0.05f);
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Camera.main.fieldOfView += (zoomSpeed * Input.GetAxis("Mouse ScrollWheel"));
            Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, 5, 20);
        }

        //오른쪽을 누르고 있으면 마우스 이동 감지 
        if (Input.GetMouseButton(1))
        {
            //Debug.Log("Y : " + Input.GetAxis("Mouse X"));
            //Debug.Log("X : " + Input.GetAxis("Mouse Y"));

            float x = Camera.main.transform.position.x - Input.GetAxis("Mouse X") * panSpeed;
            x = Mathf.Clamp(x, -8, 8);

            float z = Camera.main.transform.position.z - Input.GetAxis("Mouse Y") * panSpeed;
            z = Mathf.Clamp(z, -4, 10);
            Camera.main.transform.position = new Vector3(x, Camera.main.transform.position.y, z);
        } 
    }
}
