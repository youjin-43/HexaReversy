using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] TileInfo SelectedTile;

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
                if (SelectedTile == null)
                {
                    if (hit.transform.GetComponent<TileInfo>().State == 2)
                    {
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
                if (SelectedTile) UnSeletTile(); //셀렉된거 언셀렉 
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


    public void PutTile()
    {
        //마우스 클릭 이벤트 - 타일놓기 
        if (Input.GetMouseButtonDown(0))
        {
            SelectedTile?.SetStateTo1();
        }
    }

    [SerializeField] int i = 0;
    //TODO : 뒤집을때 쓰면 될듯 -> 머티리얼이 아니라 메시를 바꿔야함... 이거 메커니즘을 바꿔야할것 같은데... 
    //private void TileChange()
    //{
    //    Debug.Log("TileChange is executed");
    //    SelectedTile.GetComponent<MeshFilter>().mesh = meshs[i % 2];
    //    i++;
    //}
}
