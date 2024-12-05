using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using VInspector;



public class TileManager : MonoBehaviour
{
    private static TileManager _instance;
    public static TileManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        #region 싱글턴 
        if (_instance == null)
        {
            _instance = this;
            Debug.Log("TileManager가 생성됐습니다");
            //TODO : 이후 씬 변동이 있다면 나중에 활성화 
            //DontDestroyOnLoad(gameObject); // 씬이 변경되어도 삭제되지 않도록
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미. 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 TileManager가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("Destroy TileManager");
        }
        #endregion
    }


    Vector3Int[] direction = new Vector3Int[6]{
        new Vector3Int(1, 0, -1 ),
        new Vector3Int(0, 1, -1),
        new Vector3Int(-1, 1, 0),
        new Vector3Int(-1, 0, 1),
        new Vector3Int(0, -1, 1),
        new Vector3Int(1, -1, 0)
    };

    [SerializeField] Tilemap tilemap; //인스펙터에서 할당

    // Cube 좌표를 키로, 타일 오브젝트를 값으로 저장하는 딕셔너리
    public SerializedDictionary<Cube, TileInfo> TileInfos = new SerializedDictionary<Cube, TileInfo>(); //잘 작동하는지 확인용
    //public Dictionary<Cube, TileInfo> TileInfos = new Dictionary<Cube, TileInfo>();

    public List<TileInfo> BoundaryTile;

    void Start()
    {
        AddTilesToDictionary(); //딕셔너리에 타일 등록
    } 

    public void HighlightSelectableTiles()
    {
        //기존 아웃라인 해제
        //UnhighlightSelectableTiles();

        FindBoundary(); //새로운 바운더리 탐색 

        //바운더리 중 놓을 수있는곳(놓으면 뒤집을 수 있는 곳 )만 활성화 
        for(int i = 0; i < BoundaryTile.Count; i++)
        {
            if (FindFlippableTiles(BoundaryTile[i].Cube_pos) > 0)
            {
                BoundaryTile[i].gameObject.GetComponent<Outline>().enabled = true;
                BoundaryTile[i].Selectable = true;
            }
        }
    }

    public void UnhighlightSelectableTiles()
    {
        //기존 아웃라인 해제
        for (int i = 0; i < BoundaryTile.Count; i++)
        {
            BoundaryTile[i].gameObject.GetComponent<Outline>().enabled = false;
        }
    }

    void AddTilesToDictionary()
    {
        TileInfo[] list = tilemap.gameObject.GetComponentsInChildren<TileInfo>();
        for (int i = 0; i < list.Length; i++)
        {
            TileInfos.Add(list[i].Cube_pos, list[i]);
        }


        //foreach (var key in TileInfos.Keys)
        //{
        //    Debug.Log($"Key: {key}, HashCode: {key.GetHashCode()}");
        //}
    }


    //TODO  : 아 이거 매번 다시 찾는게 아니라 새로운 부분만 update해서 뭔가 최적화 할 수있을것 같은데 우선 맵이 작아 큰 문제가 없으니 프로젝트 끝나고 만족하면 개선해보는걸로....
    //TODO : 이거 맵 넘어가는 경우도 처리해야하는것 같은데ㅋㅋㅋㅋㄴ
    /// <summary>
    /// 가장 바깥쪽에 놓인 타일들의 그 다음 빈 타일들, 바운더리 타일을 찾음
    /// </summary>
    void FindBoundary()
    {
        //기존 바운더리 삭제
        BoundaryTile.Clear();

        //1. 중심에서 4번 방향으로 빈 타일이 나올떄가지 전진.
        Cube cube = new Cube(0, 0, 0); //center

        //Debug.Log("Boundary 시작 찾는중");
        for (int i = 0; i < GameManager.Instance.MapSize; i++) //어차피 size 제한이 있기 때문에 위험성 높은 while 보다는 for 사용 
        {
            cube = cube.Add(direction[4]);
            //Debug.Log("cube Pos : " + cube);
            //Debug.Log($"cube: {cube}, HashCode: {cube.GetHashCode()}");

            

            //빈 타일인지 확인 -> 처음 빈타일을 발견하면 리스트에 넣고 break.
            if (TileInfos[cube].State == -1) //todo : 여기 왜 계속 0,-1,1 키가 없다고 그러지? 
            {
                BoundaryTile.Add(TileInfos[cube]);
                //Debug.Log("Boundary Start : " + cube);
                break;
            }
        }

        for(int t=0; t < GameManager.Instance.MapSize * 6; t++) //어차피 size 제한이 있기 때문에 위험성 높은 while 보다는 for 사용 
        {
            //2. 그 타일의 이웃들탐색 → 먼저 0번 방향 봐서
            Cube n_cube = cube.Add(direction[0]);
            //Debug.Log("0번째 이웃 : " + n_cube);
            //Tiles.ContainsKey(n_cube)
            if (!TileInfos.ContainsKey(n_cube) || TileInfos[n_cube].State == -1) //0번째 이웃이 판 사이즈를 넘었거나, 빈 타일이라면 → 반시계방향 탐색
            {
                //Debug.Log("0번째 이웃이 빈 타일이네");

                for (int i = 1; i < 6; i++)
                {
                    n_cube = cube.Add(direction[i]);
                    //Debug.Log(i+"번째 이웃 : " + n_cube);

                    //탐색하다가 이미 놓인 타일을 만난다면 그 전 방향으로 전진
                    if (TileInfos.ContainsKey(n_cube) && TileInfos[n_cube].State != -1)
                    {
                        //Debug.Log(i + "번째 이웃이 이미 놓인 타일임!" );
                        n_cube = cube.Add(direction[i - 1]);
                        
                        break;
                    }
                }
            }
            else //이미 놓인 타일이라면 → 시계방향 탐색
            {
                //Debug.Log("0번째 이웃엔 이미 놓인 타일이 있어요");

                for (int i = 5; i > 0; i--)
                {
                    n_cube = cube.Add(direction[i]);
                    //Debug.Log(i + "번째 이웃 : " + n_cube);

                    // 탐색하다가 빈 타일을 만나면 그 타일로 전진
                    if (TileInfos[n_cube].State == -1)
                    {
                        //Debug.Log(i + "번째 이웃이 빈타일임!");
                        break;
                    }
                }
            }

            //를 바운더리 시작지점과 만날떄까지 반복
            if (BoundaryTile[0] == TileInfos[n_cube])
            {
                break;
            }
            else
            {
                BoundaryTile.Add(TileInfos[n_cube]);
                //Debug.Log(n_cube + "을 바운더리 리스트에 넣음!");
                cube = n_cube;
            }   
        }
    }

    int FindFlippableTiles(Cube pos)
    {
        int ret = 0;
        TileInfo tileInfo = TileInfos[pos];

        //이웃한 6방향 확인
        for(int i = 0; i < 6; i++)
        {
            // 우선 기존 정보 초기화
            tileInfo.FlipTiles[i].Clear();

            //1.빈타일이 아니라면 쭉 담음
            Cube n_cube = pos.Add(direction[i]);
            if (TileInfos[n_cube].State != -1)
            {
                for (int j = 1; j < GameManager.Instance.MapSize; j++) //위험성 높은 while 보다는 for 사용
                {
                    n_cube = pos.Add(direction[i] * j);
                    if (TileInfos.ContainsKey(n_cube))
                    {
                        tileInfo.FlipTiles[i].Push(n_cube);
                    }
                    else
                    {
                        break;
                    }
                }

                //2. 내 타일이 나올떄까지 Pop -> 내 타일이 아닌데 center도 아닌경우 
                for (int s = tileInfo.FlipTiles[i].Count - 1; s >= 0; s--) //위험성 높은 while 보다는 for 사용
                {
                    Cube cube = tileInfo.FlipTiles[i].Peek();

                    //TODO : 임시 액터 넘버 나중에 플레이어 스크립트로 수정 
                    if (TileInfos[cube].State != Player.Instance.PunActorNumber && TileInfos[cube].State != 0)
                    {
                        tileInfo.FlipTiles[i].Pop();
                    }
                    else
                    {
                        //내 타일이 나오면 break;
                        break;
                    }
                }

                //3. 뒤집을 상대 타일만 남겨놓기 위해 이제는 상대 타일이 나올떄까지 pop
                for (int s = tileInfo.FlipTiles[i].Count - 1; s >= 0; s--) //위험성 높은 while 보다는 for 사용
                {
                    Cube cube = tileInfo.FlipTiles[i].Peek();

                    //TODO : 임시 액터 넘버 나중에 플레이어 스크립트로 수정 
                    if (TileInfos[cube].State == Player.Instance.PunActorNumber || TileInfos[cube].State == 0)
                    {
                        tileInfo.FlipTiles[i].Pop();
                    }
                    else
                    {
                        //상대 타일이 나오면 break;
                        break;
                    }
                }

                //4. 남은게 이제 뒤집을 타일들 -> 바닥 부분에는 내 타일이 있을 수도 있긴 한데 암튼 위쪽에는 뒤집을 수있는 타일이 있음. 
                ret += tileInfo.FlipTiles[i].Count;

            }//이웃한 돌이 있다면 
        }//방향 확인 
        return ret;

    }

}
