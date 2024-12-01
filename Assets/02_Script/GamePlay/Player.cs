using UnityEngine;
using Photon.Pun; // Pun : 포톤 유니티 네트워크의 약자
public class Player : MonoBehaviour
{

    public StateMachine stateMachine;

    public PhotonView pv;
    public int PunActorNumber;
    //public bool isPlaying = false;

    private void Awake()
    {
        stateMachine = new StateMachine(this);
        pv = GetComponent<PhotonView>();
        //if (pv == null)
        //{
        //    Debug.LogError("PhotonView가 Player 객체에 없습니다!");
        //}
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


    //상태 변화 동기화

}