using System.Collections;
using UnityEngine;

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

    private void Start()
    {
        HideTimeSlider();
    }

    [SerializeField] Animator OpponentIntroUI;
    [SerializeField] Animator MainPanel;

    public GameObject TimeSlider;

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

    public void ShowTimeSlider()
    {
        TimeSlider.SetActive(true);
    }

    public void StartDecTimer()
    {
        TimeSlider.GetComponent<TimerSlider>().StartdecreaseActionTime();
    }

    public void StopDecTimer()
    {
        TimeSlider.GetComponent<TimerSlider>().StopdecreaseActionTime();
    }

    public void HideTimeSlider()
    {
        TimeSlider.SetActive(false);
    }

}
