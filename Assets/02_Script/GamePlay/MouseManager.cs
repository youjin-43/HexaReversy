using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    [SerializeField] TileInfo HoverdTile;
    [SerializeField] Material mat_hover; // 인스펙터에서 할당
    [SerializeField] Material basic; // 인스펙터에서 할당 

    private void Start()
    {
        HoverdTile = null;
    }

    private void Update()
    {
        // 마우스 호버 효과 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && hit.transform.CompareTag("Tile")) //Tile 태그를 가지고 있는것과만 상호작용 하도록 
        {
            TileInfo tileInfo = hit.transform.GetComponent<TileInfo>();

            if(tileInfo != HoverdTile)
            {
                if (tileInfo.Selectable && HoverdTile == null && tileInfo.State == -1) //Selectable이고 호버된 타일이 없다면
                {
                    HoverTile(tileInfo);
                }
                else
                {
                    if (HoverdTile) UnhoverTile(); //호버된게 있다면 언셀렉 
                }
            }
        }
        //레이캐스드가 아무랑도 충돌하지 않으면
        else
        {
            if (HoverdTile) UnhoverTile(); //호버된게 있다면 언셀렉 
        }
    }

    void HoverTile(TileInfo tile)
    {
        tile.GetComponent<MeshRenderer>().material= mat_hover;
        HoverdTile = tile;
        //Debug.Log(tile.Cube_pos + "is Hovered");
    }

    public void UnhoverTile()
    {

        //TODO : 타일 인포에서 가져오는건데 괜찮나,,? -> 에.. 작동 잘 되네...?

        //TODO : 이거 어차피 스테이트 머신 들어가면 바로 꺼질거라 상관없을것 같긴한데 우선 놔둬보자 
        if (HoverdTile.State == -1)
        {
            HoverdTile.GetComponent<MeshRenderer>().material = basic;
        }
        HoverdTile = null;
        
    }
    /// <summary>
    /// 타일을 놓으면 true 반환
    /// </summary>
    /// <returns></returns>
    public bool PutTile()
    {
        //마우스 클릭 이벤트 - 타일놓기 
        if (Input.GetMouseButtonDown(0) && HoverdTile != null)
        {
            if (Player.Instance.PunActorNumber == 1)
            {
                HoverdTile.SetStateTo1();
                //GameManager.Instance.tmpActorNum = 2;  //로컬 디버깅용
            }
            else
            {
                HoverdTile.SetStateTo2();
                //GameManager.Instance.tmpActorNum = 1; //로컬 디버깅용
            }
            HoverdTile.Flip(); //사이에 낀 상대편 돌 뒤집기 
            UnhoverTile(); //호버됐던거 언호버 
            return true;
        }
        return false;
    }

}
