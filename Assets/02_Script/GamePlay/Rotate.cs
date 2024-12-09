using System.Collections;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 180f; // 1?? 180? ??

    void Update()
    {
        //transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }

    public void RotateTile()
    {
        StartCoroutine(Rotate180());
    }

    IEnumerator Rotate180()
    {
        //Debug.Log("Rotate180 ??? ?? ");

        // ?? ??? ??
        float targetAngle = transform.eulerAngles.z + 180f;

        // ??
        while (Mathf.Abs(transform.eulerAngles.z - targetAngle) > 0.1f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
            yield return null;
        }

        // ?? (??? ? ??)
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetAngle);

    }
}
