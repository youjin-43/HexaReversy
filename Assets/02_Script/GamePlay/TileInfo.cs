using UnityEngine;
using System.Collections.Generic;
using Photon.Pun; // Pun : 포톤 유니티 네트워크의 약자
public class TileInfo : MonoBehaviour
{
    /// <summary>
    /// 2: 아무도 놓지 않은 상태 
    /// 1: 선공 돌     
    /// 0: 후공 돌 
    /// </summary>
    public int State = 2;


    [SerializeField] Material mat;//타일이 놓였을때 바뀔 머티리얼. 투명에서 이 머티리얼로 바꿔야함. 인스펙터에서 할당
    [SerializeField] List<Mesh> meshs; // 돌을 놓으면 바뀔 디자인. 인스펙터에서 할당

    private PhotonView pv;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    void Start()
    {
        State = 2; 
    }

    [PunRPC]
    public void SetStateTo1_RPC()
    {
        State = 1;
        GetComponent<MeshRenderer>().material = mat;
        GetComponent<MeshFilter>().mesh = meshs[State];
    }

    public void SetStateTo1()
    {
        pv.RPC("SetStateTo1_RPC", RpcTarget.All);
    }

   [PunRPC]
    public void SetStateTo2_RPC()
    {
        State = 2;
        GetComponent<MeshRenderer>().material = mat;
        GetComponent<MeshFilter>().mesh = meshs[State];
    }

    public void SetStateTo2()
    {
        pv.RPC("SetStateTo2_RPC", RpcTarget.All);
    }

}
