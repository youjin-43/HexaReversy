using UnityEngine;
using Photon.Pun; // Pun : 포톤 유니티 네트워크의 약자

public class Player : MonoBehaviour
{
    private static Player _instance;
    public static Player Instance
    {
        get { return _instance; }
    }

    public StateMachine stateMachine;

    public PhotonView pv;
    public int PunActorNumber;

    private void Awake()
    {
        #region 싱글턴 
        if (_instance == null)
        {
            _instance = this;
            Debug.Log("Player가 생성됐습니다");
            //TODO : 이후 씬 변동이 있다면 나중에 활성화 
            //DontDestroyOnLoad(gameObject); // 씬이 변경되어도 삭제되지 않도록
        }
        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있는 경우 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미. 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 Player가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("Destroy Player");
        }
        #endregion

        stateMachine = new StateMachine(this);
        pv = GetComponent<PhotonView>();
        if (pv == null) { Debug.LogError("PhotonView가 Player 객체에 없습니다!"); }
    }

    void Start()
    {
        PunActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log("내 ActorNumber: " + PunActorNumber);
        stateMachine.Initialize(stateMachine.gameStartState); //GameStartState로 시작

        //TODO : UI 효과중에는 클릭 안먹게 하고싶은데...
    }


    private void Update()
    {
        stateMachine.Excute(); //현재 state에서 실행할 동작을 실행 }
    }


    //StateMachine 클래스는 PhotonView를 직접 가질 수 없으므로 Player오브젝트를 거쳐서 트랜지션을 하도록 함 
    [PunRPC]
    private void SyncStateTransition(string stateName)
    {
        stateMachine.CurrentState.Exit(); //현재 상태는 나가고 

        // 상태 이름을 통해 새로운 상태로 전환
        switch (stateName)
        {
            case "TurnPlayer1":
                stateMachine.SetStateToTurnPlayer1();
                break;
            case "TurnPlayer2":
                stateMachine.SetStateToTurnPlayer2();
                break;

                // TODO :다른 상태들 추가...
        }

        stateMachine.CurrentState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        pv.RPC("SyncStateTransition", RpcTarget.All, nextState.GetType().Name); // 모든 클라이언트에 전송
    }

}