using System.Collections;
using UnityEngine;
using Photon.Pun; // Pun : 포톤 유니티 네트워크의 약자

public class Rotate : MonoBehaviour
{
    public float rotationSpeed = 180f; // 1초에 180도 회전

    private PhotonView pv;
    private void Start()
    {
        pv = GetComponentInChildren<PhotonView>();
    }

    void Update()
    {
        //transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));

    }

    //public void RotateTile()
    //{
    //    pv.RPC("RotateTile_RPC", RpcTarget.All);
    //}

    public void RotateTile()
    {
        StartCoroutine(Rotate180());
    }

    IEnumerator Rotate180()
    {
        //Debug.Log("Rotate180 코루틴 실행 ");

        // 목표 각도를 계산
        float targetAngle = transform.eulerAngles.z + 180f;
        //float startAngle = transform.eulerAngles.z;

        // 회전
        while (Mathf.Abs(transform.eulerAngles.z - targetAngle) > 0.1f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
            yield return null;
        }

        // 정렬 (정확한 값 보정)
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, targetAngle);
    }
}
