using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            //TODO : 싱글턴을 어떻게 해야할지 고민이다..
            //if (!_instance)
            //{
            //    _instance = FindObjectOfType<GameManager>();
            //    if (!_instance)
            //    {
            //        GameObject obj = new GameObject();
            //        obj.name = "GameManager";
            //        _instance = obj.AddComponent(typeof(GameManager)) as GameManager;

            //        //TODO : 이런식으로 짜면 이거 필요없지 않나? 어차피 새로 생기니까 
            //        //DontDestroyOnLoad(gameObject); // 씬이 변경되어도 삭제되지 않도록
            //    }
            //}
            return _instance;
        }
    }

    //TODO : 포톤매니저가 먼저 실행된 후 이게 실행되는게 맞겠지? 우선 해보고 실험ㄱㄱ
    private PhotonView pv;
    private void Awake()
    {

        #region 싱글턴 
        if (_instance == null)
        {
            _instance = this;
            Debug.Log("게임매니저가 생성됐습니다");
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 삭제되지 않도록
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미. 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("Destroy 게임매니저");
        }
        #endregion

        //TODO : 이거 포톤뷰 아이디 맞는지 확인 
        pv = GetComponent<PhotonView>();
    }

    [SerializeField] TextMeshProUGUI LoadingText;


    //TODO : 이거 처음 UI에서 입력한대로 연동되도록 
    public string UserID = "Player";

    //TODO : 에 이거 해야하는데 아직은 필요없음
    public bool isPlaying = false;

    //TODO : 게임 매니저에 포톤뷰를 달고 플레이어 인포를 만들어서 그걸로 플레이어 관리? -> 우선 해보자..



    [PunRPC]
    private void SetFindedTextRPC()
    {
        LoadingText.text = "Finded!";
    }

    public void SetFindedText()
    {
        pv.RPC("SetFindedTextRPC", RpcTarget.All);
    }

    //포톤 매니저에서 두번째 참여(클라이언트)가 오면 이걸 호출
    /// <summary>
    /// 마스터에게 게임 씬 로드하라고 명령하는 함수 
    /// </summary>
    [PunRPC]
    private void GameStart_RPC()
    {

        UIManager.Instance.Fadeout();
        if (PhotonNetwork.IsMasterClient)
        {
            //TODO : 이거 잘 실행되는지 확인 -> 상대를 찾으면 알아서 씬이 넘어가야함 
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    public void GameStart()
    {
        pv.RPC("GameStart_RPC", RpcTarget.All); 
    }
    
}
