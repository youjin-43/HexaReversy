using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get {return _instance;}
    }

    private void Awake()
    {
        #region 싱글턴 
        if (_instance == null)
        {
            _instance = this;
            Debug.Log("UIManager가 생성됐습니다");
            //TODO : 이후 씬 변동이 있다면 나중에 활성화 
            //DontDestroyOnLoad(gameObject); // 씬이 변경되어도 삭제되지 않도록
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미. 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 UIManager가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("Destroy UIManager");
        }
        #endregion
       
    }


    [SerializeField] Animator OpponentIntroUI; //인스펙터에서 할당 
    [SerializeField] Animator MainPanel; //인스펙터에서 할당 
    


    private void Start()
    {
        SetPlayerName();
        HideTimeSlider();
    }



    [SerializeField] TextMeshProUGUI IntroMyName; //인스펙터에서 할당 
    [SerializeField] TextMeshProUGUI IntroOppName; //인스펙터에서 할당

    [SerializeField] TextMeshProUGUI MainUIMyName; //인스펙터에서 할당 
    [SerializeField] TextMeshProUGUI MainUIOppName; //인스펙터에서 할당 

    void SetPlayerName()
    {
        foreach (var player in PhotonNetwork.CurrentRoom.Players)
        {
            if(player.Value.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                IntroMyName.text = PhotonNetwork.LocalPlayer.NickName;
                MainUIMyName.text = PhotonNetwork.LocalPlayer.NickName;
            }
            else
            {
                IntroOppName.text = player.Value.NickName;
                MainUIOppName.text = player.Value.NickName;
            }
        }
    }



    public void PlayIntroUI()
    {
        StartCoroutine("DisapperOponentIntroUI");
    }

    IEnumerator DisapperOponentIntroUI()
    {
        yield return new WaitForSeconds(2f);
        OpponentIntroUI.SetTrigger("Disappear");
        MainPanel.SetTrigger("Show");
    }


    #region Action Time
    public Slider TimeSlider;

    public void ShowTimeSlider()
    {
        TimeSlider.gameObject.SetActive(true);
    }

    public void HideTimeSlider()
    {
        TimeSlider.gameObject.SetActive(false);
    }
    #endregion

}
