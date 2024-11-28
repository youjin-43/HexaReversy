using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<GameManager>();
                if (!_instance)
                {
                    GameObject obj = new GameObject();
                    obj.name = "GameManager";
                    _instance = obj.AddComponent(typeof(GameManager)) as GameManager;
                }
            }
            return _instance;
        }
    }

    //TODO : 포톤매니저가 먼저 실행된 후 이게 실행되는게 맞겠지? 우선 해보고 실험ㄱㄱ
    private PhotonView pv;
    private void Awake()
    {
        //TODO : 이거 포톤뷰 아이디 맞는지 확인 
        pv = GetComponent<PhotonView>();
    }


    //TODO : 이거 처음 UI에서 입력한대로 연동되도록 
    public string UserID = "Player";

    //TODO : 에 이거 해야하는데 아직은 필요없음
    public bool isPlaying = false;

    //TODO : 게임 매니저에 포톤뷰를 달고 플레이어 인포를 만들어서 그걸로 플레이어 관리? -> 우선 해보자..

    
    //포톤 매니저에서 두번째 참여(클라이언트)가 오면 이걸 호출
    /// <summary>
    /// 마스터에게 게임 씬 로드하라고 명령하는 함수 
    /// </summary>
    [PunRPC]
    private void GameStart_RPC()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //TODO : 이거 잘 실행되는지 확인 -> 상대를 찾으면 알아서 씬이 넘어가야함 
            PhotonNetwork.LoadLevel("GameScene");
        }
    }

    public void GameStart()
    {
        pv.RPC("GameStart", RpcTarget.All); 
    }
    
}
