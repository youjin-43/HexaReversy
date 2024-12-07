using UnityEngine;

public class EndState : IState
{
    Player player;

    //생성자
    public EndState(Player player)
    {
        this.player = player;
    }

    void IState.Enter()
    {
        Debug.Log("현재 State : GameEnd");

        //TODO : Show 승패 UI -> 리겜? -> 상대방이 리매치를 신청합니다 -> 상대방 응답 기다리는중 -> ... -> 씬 다시 로드 
    }

    void IState.Excute()
    {

    }

    void IState.Exit()
    {

    }
}
