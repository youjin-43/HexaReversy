using UnityEngine;
using System.Collections.Generic;
using Photon.Pun; // Pun : 포톤 유니티 네트워크의 약자
using UnityEngine.Tilemaps;

public class TileInfo : MonoBehaviour
{
    /// <summary>
    /// -1: 아무도 놓지 않은 상태, 0: Center, 1: 선공 돌, 2: 후공 돌 
    /// </summary>
    public int State = -1;
    public bool Selectable = false;

    Tilemap tilemap; // 타일맵 컴포넌트

    [Header("Position")]
    public Vector3Int Oddr_pos;
    public Cube Cube_pos;

    [Header("Flip")]
    Rotate rotate;
    public Stack<Cube>[] FlipTiles;
    Material[] mat;//타일이 놓였을때 바뀔 머티리얼.

    private PhotonView pv;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    void Start()
    {
        mat = Resources.LoadAll<Material>("PlayerColor"); //인스펙터에서 할당해주기 귀찮아서..

        //State 초기화 
        switch (transform.parent.gameObject.name)
        {
            case "Center":
                State = 0;
                break;
            case "Blue" :
                State = 1; //선공
                GetComponent<MeshRenderer>().material = mat[State - 1];
                break;
            case "Red":
                State = 2; //후공
                GetComponent<MeshRenderer>().material = mat[State - 1];
                break;
            default:
                State = -1; //아무것도 놓지 않은 상태 
                break;
        }

        Selectable = false;

        //Position
        tilemap = transform.parent.parent.GetComponent<Tilemap>();
        Oddr_pos = tilemap.WorldToCell(transform.position);
        Cube_pos = new Cube().oddr_to_cube(Oddr_pos);

        //Flip
        rotate = transform.parent.GetComponent<Rotate>();
        FlipTiles = new Stack<Cube>[6];
        for (int i = 0; i < FlipTiles.Length; i++)
        {
            FlipTiles[i] = new Stack<Cube>(); // 각 요소에 Stack 객체 생성 및 할당
        }
    }

    [PunRPC]
    public void SetStateTo1_RPC()
    {
        if (State == 2) TileManager.Instance.Cnt_state2--;
        State = 1;
        TileManager.Instance.Cnt_state1++;
        TileManager.Instance.EraseInEmptyTilesDic(Cube_pos);


        GetComponent<MeshRenderer>().material = mat[State-1];
        UIManager.Instance.UpdateTileCntUI();
    }

    public void SetStateTo1()
    {
        pv.RPC("SetStateTo1_RPC", RpcTarget.All);
        
    }

   [PunRPC]
    public void SetStateTo2_RPC()
    {
        if(State == 1) TileManager.Instance.Cnt_state1--;
        State = 2;
        TileManager.Instance.Cnt_state2++;
        TileManager.Instance.EraseInEmptyTilesDic(Cube_pos);

        GetComponent<MeshRenderer>().material = mat[State-1];
        UIManager.Instance.UpdateTileCntUI();
    }

    public void SetStateTo2()
    {
        pv.RPC("SetStateTo2_RPC", RpcTarget.All);
        
    }


    /// <summary> 스택에 있는 타일을 뒤집기! </summary>
    public void Flip()
    {
        for(int k=0;k<6;k++)
        {
            Stack<Cube> st = FlipTiles[k];
            int s = st.Count;
            Debug.Log("Stack size = " + s);

            for (int i=0; i<s; i++) //위험성 높은 while 보다는 for 사용 
            {
                TileInfo tile = TileManager.Instance.AllTiles[st.Peek()];

                //center도 아니고 내 타일도 아니라면  
                if (tile.State != 0 && tile.State != Player.Instance.PunActorNumber )
                {
                    tile.rotate.RotateTile();
                    if (Player.Instance.PunActorNumber == 1)
                    {
                        tile.SetStateTo1();
                    }
                    else
                    {
                        tile.SetStateTo2();
                    }
                }
                st.Pop();
            }
        }
    }
}
