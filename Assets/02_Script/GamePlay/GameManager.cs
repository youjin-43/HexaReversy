using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }
    private PhotonView pv;

    private void Awake()
    {
        #region 싱글턴 
        if (_instance == null)
        {
            _instance = this;
            Debug.Log("GameManager가 생성됐습니다");
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 삭제되지 않도록
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미. 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            //Debug.LogWarning("씬에 두개 이상의 GameManager가 존재합니다!");
            //Destroy(gameObject);
            //Debug.Log("Destroy GameManager");
        }
        #endregion

        pv = GetComponent<PhotonView>();
    }

    [Header("UserInfo")]
    //public string UserName;
    //public int tmpActorNum = 1; //디버깅용
    public bool isAImode = false;



    [Header("GamePlay")]
    /// <summary> 행동할 수 있는 시간 </summary>
    public float actionTime = 10f;

    //TODO : 나중에는 맵 사이즈도 설정하면 좋을듯 
    /// <summary> 중앙 타일로부터 몇개까지 뻗어있는지 </summary>
    public int MapSize = 5;

    public void SetAImode_True()
    {
        isAImode = true;
    }
    public void ReLoad_GameScene()
    {
        pv.RPC("ReLoad_GameScene_RPC", RpcTarget.All);
    }

    [PunRPC]
    public void ReLoad_GameScene_RPC()
    {
        UIManager.Instance.FadeOut();
        PhotonNetwork.LoadLevel("GameScene"); //씬이동 
    }

    public void ReLoad_TitleScene()
    {
        UIManager.Instance.FadeOut();
        PhotonNetwork.LoadLevel("TitleScene"); //씬이동 
    }

    public void ReLoad_AIModeScene()
    {
        //AIModeScene
        GameManager.Instance.isAImode = true;
        SceneManager.LoadScene("AIModeScene");
    }

}
