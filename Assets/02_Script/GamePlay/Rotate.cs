using UnityEngine;

public class Rotate : MonoBehaviour
{ 
    float rotSpeed = 100f;

    void Update()
    {
    transform.Rotate(new Vector3(0, 0 , rotSpeed * Time.deltaTime));
    }

}
