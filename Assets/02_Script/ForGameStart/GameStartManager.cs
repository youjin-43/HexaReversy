using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameStartManager : MonoBehaviour
{
    private static GameStartManager _instance;
    public static GameStartManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private PhotonView pv;
    private void Awake()
    {
        #region 싱글턴 
        if (_instance == null)
        {
            _instance = this;
            Debug.Log("GameStartManager가 생성됐습니다");
            //DontDestroyOnLoad(gameObject); // 씬이 변경되어도 삭제되지 않도록
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미. 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 GameStartManager가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("Destroy GameStartManager");
        }
        #endregion

        pv = GetComponent<PhotonView>();
    }

    

    #region Finded Text
    [SerializeField] TextMeshProUGUI LoadingText;

    [PunRPC]
    private void SetFindedTextRPC()
    {
        LoadingText.text = "Finded!";
        AudioManager.Instance.PlayGoSound();
    }

    public void SetFindedText()
    {
        pv.RPC("SetFindedTextRPC", RpcTarget.All);
    }
    #endregion

    #region FadeOut
    [SerializeField] GameObject FadeInOut;
    public void Fadeout()
    {
        //Debug.Log("Fadeout 함수 실행됨");
        FadeInOut.GetComponent<Animator>().SetTrigger("FadeOut");
    }
    #endregion


    #region GameStart 
    /// <summary> 마스터에게 게임 씬 로드하라고 명령하는 함수 -> 포톤 매니저에서 두번째 참여(클라이언트)가 오면 이걸 호출 </summary>
    [PunRPC]
    private void GameStart_RPC()
    {

        Fadeout(); //페이드 아웃되며 
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("GameScene"); //씬이동 
        }
    }

    public void GameStart()
    {
        pv.RPC("GameStart_RPC", RpcTarget.All); 
    }
    #endregion


}
