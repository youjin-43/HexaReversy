using UnityEngine;
using Photon.Pun; // Pun : 포톤 유니티 네트워크의 약자
public class Player : MonoBehaviour
{

    public StateMachine stateMachine;

    public int PunActorNumber;
    //public bool isPlaying = false;

    private void Awake()
    {
        stateMachine = new StateMachine(this);
    }

    void Start()
    {
        PunActorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log("내 ActorNumber: " + PunActorNumber);
        stateMachine.Initialize(stateMachine.gameStartState); //GameStartState로 시작

        //TODO : UI 효과중에는 클릭 안먹게 하고싶은데...

        OnTurnPlayer1(); //1번 플레이어 선으로 시작 
    }


    private void Update()
    {
        stateMachine.Excute(); //현재 state에서 실행할 동작을 실행 }
    }


    public void OnTurnPlayer1() { stateMachine.TransitionTo(stateMachine.turnPlayer1); }
    public void OnTurnPlayer2() { stateMachine.TransitionTo(stateMachine.turnPlayer2); }
    public void OnWin() { stateMachine.TransitionTo(stateMachine.winState); }
    public void OnLose() { stateMachine.TransitionTo(stateMachine.loseState); }
    public void OnEnd() { stateMachine.TransitionTo(stateMachine.endState); }


}