using System.Collections;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 180f; // 1초에 180도 회전
    public float delayBetweenRotations = 0.5f; // 회전 사이 간격

    private bool isRotating = false;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
        //if (!isRotating)
        //{
        //    StartCoroutine(Rotate180());
        //}
    }

    public void RotateTile()
    {
        StartCoroutine(Rotate180());
    }

    IEnumerator Rotate180()
    {
        Debug.Log("Rotate180 코루틴 실행 ");
        isRotating = true;

        // 목표 각도를 계산
        float targetAngle = transform.eulerAngles.z + 180f;
        float startAngle = transform.eulerAngles.z;

        // 회전
        while (Mathf.Abs(transform.eulerAngles.z - targetAngle) > 0.1f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
            yield return null;
        }

        // 정렬 (정확한 값 보정)
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetAngle);

        // 딜레이
        yield return new WaitForSeconds(delayBetweenRotations);
        isRotating = false;
    }
}
