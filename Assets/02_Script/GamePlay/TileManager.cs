using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using VInspector;


//TODO : 싱글턴으로 만들고 스테에트 머신에서 함수들 호출하면 될것 같음 
public class TileManager : MonoBehaviour
{
    [SerializeField] Tilemap tilemap; //인스펙터에서 할당
    //[SerializeField] TileInfo center;

    // Cube 좌표를 키로, 타일 오브젝트를 값으로 저장하는 딕셔너리
    public SerializedDictionary<Cube, TileInfo> Tiles = new SerializedDictionary<Cube, TileInfo>(); //잘 작동하는지 확인용
    //public Dictionary<Cube, TileInfo> Tiles = new Dictionary<Cube, TileInfo>();
     
    public List<TileInfo> BoundaryTile;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AddTilesToDictionary(); //딕셔너리에 타일 등록
        //center = Tiles[new Cube(0, 0, 0)]; //센터 등록 
    }

    public void Test()
    {
        FindBoundary();
        ShowBoundary();
    }

    void AddTilesToDictionary()
    {
        TileInfo[] list = tilemap.gameObject.GetComponentsInChildren<TileInfo>();
        for (int i = 0; i < list.Length; i++)
        {
            Tiles.Add(list[i].Cube_pos, list[i]);
        }
    }


    //TODO  : 아 이거 매번 다시 찾는게 아니라 새로운 부분만 update해서 뭔가 최적화 할 수있을것 같은데 우선 맵이 작아 큰 문제가 없으니 프로젝트 끝나고 만족하면 개선해보는걸로....
    /// <summary>
    /// 가장 바깥쪽에 놓인 타일들의 그 다음 빈 타일들, 바운더리 타일을 찾음
    /// </summary>
    void FindBoundary()
    {
        //기존 바운더리 삭제
        BoundaryTile.Clear();

        //1. 중심에서 4번 방향으로 빈 타일이 나올떄가지 전진.
        Cube cube = new Cube(0, 0, 0); //center

        Debug.Log("Boundary 시작 찾는중");
        for (int i = 0; i < GameManager.Instance.MapSize; i++) //어차피 size 제한이 있기 때문에 위험성 높은 while 보다는 for 사용 
        {
            cube = cube.Add(cube.direction[4]);
            Debug.Log("cube Pos : " + cube.q +','+ cube.r + ',' + cube.s);

            //빈 타일인지 확인 -> 처음 빈타일을 발견하면 리스트에 넣고 break.
            if (Tiles[cube].State == -1)
            {
                BoundaryTile.Add(Tiles[cube]);
                break;
            }
        }

        for(int t=0; t < GameManager.Instance.MapSize * 6; t++) //어차피 size 제한이 있기 때문에 위험성 높은 while 보다는 for 사용 
        {
            //2. 그 타일의 이웃들탐색 → 먼저 0번 방향 봐서
            Cube n_cube = cube.Add(cube.direction[0]);
            Debug.Log("0번째 이웃 : " + n_cube);

            if (Tiles[n_cube].State == -1) //빈 타일이라면 → 반시계방향 탐색
            {
                Debug.Log("n_cube가 빈 타일이네");

                for (int i = 1; i < 6; i++)
                {
                    n_cube = cube.Add(cube.direction[i]);
                    Debug.Log(i+"번째 이웃 : " + n_cube);

                    //탐색하다가 이미 놓인 타일을 만난다면 그 전 방향으로 전진
                    if (Tiles[n_cube].State != -1)
                    {
                        Debug.Log(i + "번째 이웃이 이미 놓인 타일임!" );
                        n_cube = cube.Add(cube.direction[i - 1]);
                        
                        break;
                    }
                }
            }
            else //이미 놓인 타일이라면 → 시계방향 탐색
            {
                Debug.Log("n_cube엔 이미 놓인 타일이 있어요");

                for (int i = 5; i > 0; i--)
                {
                    n_cube = cube.Add(cube.direction[i]);
                    Debug.Log(i + "번째 이웃 : " + n_cube);

                    // 탐색하다가 빈 타일을 만나면 그 타일로 전진
                    if (Tiles[n_cube].State == -1)
                    {
                        Debug.Log(i + "번째 이웃이 빈타일임!");
                        //n_cube = cube.Add(cube.direction[i]);
                        break;
                    }
                }
            }

            //를 바운더리 시작지점과 만날떄까지 반복
            if (BoundaryTile[0] == Tiles[n_cube])
            {
                break;
            }
            else
            {
                BoundaryTile.Add(Tiles[n_cube]);
                Debug.Log(n_cube + "을 바운더리 리스트에 넣음!");
                cube = n_cube;
            }   
        }
      
    }

    void ShowBoundary()
    {
        Debug.Log("ShowBoundary 실행 ");
        foreach(TileInfo cube in BoundaryTile)
        {
            Debug.Log("cube Pos : " + cube.Cube_pos);
            cube.gameObject.GetComponent<Outline>().enabled = true;
        }
    }
}
