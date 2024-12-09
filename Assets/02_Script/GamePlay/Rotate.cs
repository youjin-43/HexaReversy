using System.Collections;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 180f; // 1초에 180도 회전
    public float delayBetweenRotations = 0.5f; // 회전 사이 간격

    private bool isRotating = false;

    //void Update()
    //{
    //    if (!isRotating)
    //    {
    //        StartCoroutine(Rotate180());
    //    }
    //}

    public void RotateTile()
    {
        StartCoroutine(Rotate180());
    }

    IEnumerator Rotate180()
    {
        isRotating = true;

        // 목표 각도를 계산
        float targetAngle = transform.eulerAngles.y + 180f;
        float startAngle = transform.eulerAngles.y;

        // 회전
        while (Mathf.Abs(transform.eulerAngles.y - targetAngle) > 0.1f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            yield return null;
        }

        // 정렬 (정확한 값 보정)
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, targetAngle, transform.eulerAngles.z);

        // 딜레이
        yield return new WaitForSeconds(delayBetweenRotations);
        isRotating = false;
    }
}
