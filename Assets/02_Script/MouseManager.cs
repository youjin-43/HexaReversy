using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] GameObject SelectedTile;
    [SerializeField] List<Mesh> meshs; //인스펙터에서 할당
    [SerializeField] Material mat;//타일이 놓였을때 바뀔 머티리얼. 인스펙터에서 할당 
    private void Start()
    {
        SelectedTile = null;
    }

    private void Update()
    {
        //마우스 호버시 타일을 놓을 수 있는 곳에 아웃라인 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            //이전에 셀렉된게 없고, 지금 셀렉될 타일이 아직 놓기 전이라면 셀렉(아웃라인 표시 )
            if (SelectedTile == null )
            {
                if (hit.transform.GetComponent<TileInfo>().State == 2) {
                    SelectTile(hit.transform.gameObject);
                }
                else
                {
                    UnSeletTile();
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
        else
        {
            //레이캐스드가 아무랑도 충돌하지 않으면
            if(SelectedTile) UnSeletTile();
        }

        //마우스 클릭 이벤트 - 타일놓기 
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectedTile) PutTile();
        }
    }

    void SelectTile(GameObject obj)
    {
        SelectedTile = obj.transform.gameObject;
        SelectedTile.GetComponent<Outline>().enabled = true;
        Debug.Log(SelectedTile.name + "is Selected");

    }

    //TODO : 어디서 null을 언셀렉하는데 나중에 고쳐~

    void UnSeletTile()
    {
        Debug.Log(SelectedTile.name + "is Selected");
        SelectedTile.GetComponent<Outline>().enabled = false;
        SelectedTile = null;
    }

    void PutTile()
    {
        SelectedTile.GetComponent<MeshRenderer>().material = mat;
        SelectedTile.GetComponent<TileInfo>().State = 1;
        UnSeletTile();
    }


    [SerializeField] int i = 0;
    //TODO : 뒤집을때 쓰면 될듯 
    private void TileChange()
    {
        Debug.Log("TileChange is executed");
        SelectedTile.GetComponent<MeshFilter>().mesh = meshs[i % 2];
        i++;
    }
}
