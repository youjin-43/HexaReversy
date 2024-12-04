using UnityEngine;
using System.Collections.Generic;
using Photon.Pun; // Pun : 포톤 유니티 네트워크의 약자
using UnityEngine.Tilemaps;

public class TileInfo : MonoBehaviour
{
    /// <summary>
    /// -1: 아무도 놓지 않은 상태 
    /// 0: 선공 돌     
    /// 1: 후공 돌
    /// 2: Center 
    /// </summary>
    public int State = -1;
    public bool isCenter = false;

    Tilemap tilemap; // 타일맵 컴포넌트

    [Header("Position")]
    public Vector3Int Oddr_pos;
    public Cube Cube_pos;


    [SerializeField] Material[] mat;//타일이 놓였을때 바뀔 머티리얼. 투명에서 이 머티리얼로 바꿔야함. 인스펙터에서 할당

    private PhotonView pv;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    void Start()
    {
        //State 초기화 
        switch (transform.parent.gameObject.name)
        {
            case "Center":
                State = 2;
                break;
            case "Blue" :
                State = 0; //선공 
                break;
            case "Red":
                State = 1; //후공 
                break;
            default:
                State = -1; //아무것도 놓지 않은 상태 
                break;
        }

        //Position
        tilemap = transform.parent.parent.GetComponent<Tilemap>();
        Oddr_pos = tilemap.WorldToCell(transform.position);
        Cube_pos = new Cube().oddr_to_cube(Oddr_pos);
    }

    [PunRPC]
    public void SetStateTo1_RPC()
    {
        State = 0;
        GetComponent<MeshRenderer>().material = mat[State];
    }

    public void SetStateTo1()
    {
        pv.RPC("SetStateTo1_RPC", RpcTarget.All);
    }

   [PunRPC]
    public void SetStateTo2_RPC()
    {
        State = 1;
        GetComponent<MeshRenderer>().material = mat[State];
    }

    public void SetStateTo2()
    {
        pv.RPC("SetStateTo2_RPC", RpcTarget.All);
    }

}
