using UnityEngine;
using Photon.Pun; // Pun : 포톤 유니티 네트워크의 약자

public class Player : MonoBehaviour
{
    public StateMachine stateMachine;

    public PhotonView pv;
    public int PunActorNumber;

    private void Awake()
    {
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