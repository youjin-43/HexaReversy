using UnityEngine;
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

    //TODO : 스테이트 머신이 있어서..게임 매니저의 쓸모가 없어졌긴 했는데 이게 맞나ㅣ

    public float actionTime = 10f; // 행동할 수 있는 시간


    //TODO : 나중에는 맵 사이즈도 설정하면 좋을듯 
    /// <summary>
    /// 중앙 타일로부터 몇개까지 뻗어있는지 
    /// </summary>
    public int MapSize = 5;

    public int tmpActorNum = 1;

}
