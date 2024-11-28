using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // Pun : 포톤 유니티 네트워크의 약자

public class MouseManager : MonoBehaviour
{
    [SerializeField] TileInfo SelectedTile;
    //[SerializeField] List<Mesh> meshs; // 돌을 놓으면 바뀔 디자인. 인스펙터에서 할당
    //[SerializeField] Material mat;//타일이 놓였을때 바뀔 머티리얼. 투명에서 이 머티리얼로 바꿔야함. 인스펙터에서 할당

    //private PhotonView pv;
    //private void Awake()
    //{
    //    //TODO : 이거 포톤뷰 아이디 게임 매니저랑 겹치던데 괜찮나?? 
    //    pv = GetComponent<PhotonView>();
    //}

    private void Start()
    {
        SelectedTile = null;
    }

    private void Update()
    {
        //마우스 호버시 타일을 놓을 수 있는 곳에 아웃라인 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Tile")) //Tile 태그를 가지고 있는것과만 상호작용 하도록 
        {
            //이전에 셀렉된게 없고, 지금 셀렉될 타일이 아직 놓기 전이라면 셀렉(아웃라인 표시 )
            if (SelectedTile == null )
            {
                if (hit.transform.GetComponent<TileInfo>().State == 2) {
                    SelectTile(hit.transform.gameObject);
                }
            }
            //기존에 셀렉된게 있다면 
            else
            {
                //기존 셀렉이 새로 충돌한거랑 다른 오브젝트야? 
                if (hit.transform.gameObject != SelectedTile)
                {
                    UnSeletTile(); //기존꺼 언셀렉 
                    if (hit.transform.GetComponent<TileInfo>().State == 2) SelectTile(hit.transform.gameObject); //새로 셀렉 
                    
                }
            }
        }
        //레이캐스드가 아무랑도 충돌하지 않으면
        else
        {
            if(SelectedTile) UnSeletTile(); //셀렉된거 언셀렉 
        }

        //TODO : 잘 놓아지는지 확인 
        //마우스 클릭 이벤트 - 타일놓기 
        if (Input.GetMouseButtonDown(0))
        {
            SelectedTile?.SetStateTo1();
        }
    }

    void SelectTile(GameObject obj)
    {
        SelectedTile = obj.transform.GetComponent<TileInfo>();
        SelectedTile.GetComponent<Outline>().enabled = true;
        //Debug.Log(SelectedTile.name + "is Selected");

    }

    void UnSeletTile()
    {
        if (SelectedTile)
        {
            //Debug.Log(SelectedTile.name + "is Selected");
            SelectedTile.GetComponent<Outline>().enabled = false;
            SelectedTile = null;
        }
    }

    //TODO : 마우스에서 할게 아니라 마우슨는 타일의 정보만 바꾸고, 타일에서 정보에 따라 색 바뀌면 될것 같음 
    void PutTile()
    {
        //TODO : 이거 대신 새로운 함수 
        //SelectedTile.GetComponent<MeshRenderer>().material = mat;
        //SelectedTile.GetComponent<TileInfo>().State = 1;

        UnSeletTile();
    }

    //[PunRPC]
    //void PutTile_RPC()
    //{
    //    SelectedTile.GetComponent<MeshRenderer>().material = mat;
    //    SelectedTile.GetComponent<TileInfo>().State = 1;
    //    UnSeletTile();
    //}

    //void PutTile()
    //{
    //    pv.RPC("PutTile_RPC", RpcTarget.All);
    //}


    [SerializeField] int i = 0;
    //TODO : 뒤집을때 쓰면 될듯 
    //private void TileChange()
    //{
    //    Debug.Log("TileChange is executed");
    //    SelectedTile.GetComponent<MeshFilter>().mesh = meshs[i % 2];
    //    i++;
    //}
}
