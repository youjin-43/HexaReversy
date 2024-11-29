using UnityEngine;
using Photon.Pun; // Pun : 포톤 유니티 네트워크의 약자
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        #region 싱글턴 
        if (_instance == null)
        {
            _instance = this;
            Debug.Log("GameManager가 생성됐습니다");
            //TODO : 이후 씬 변동이 있다면 나중에 활성화 
            //DontDestroyOnLoad(gameObject); // 씬이 변경되어도 삭제되지 않도록
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미. 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 GameManager가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("Destroy GameManager");
        }
        #endregion
    }

    private int myActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
    public bool isPlaying = false;
    public int CurrentTurn = 1;
    
    void Start()
    {
        Debug.Log("내 ActorNumber: " + myActorNumber);
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO : 턴 반복

    }

    public void GameStart()
    {
        UIManager.Instance.PlayIntroUI();
        if (CurrentTurn == myActorNumber) isPlaying = true;
    }
}
