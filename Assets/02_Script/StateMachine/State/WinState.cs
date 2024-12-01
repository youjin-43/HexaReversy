using UnityEngine;

public class WinState : IState
{
    Player player;
    MouseManager MouseControll;

    //생성자
    public WinState(Player player)
    {
        this.player = player;
        MouseControll = player.GetComponent<MouseManager>();
    }

    void IState.Enter()
    {
        Debug.Log("현재 State : Win");
        MouseControll.enabled = false;
    }


    void IState.Excute()
    {

    }

    void IState.Exit()
    {

    }

    public bool ShouldTransition()
    {
        return false;
    }

    //bool IState.ShouldTransition(out IState nextState)
    //{
    //    nextState = player.stateMachine.endState; // 미리 생성된 상태 객체 사용
    //    //TODO : 수정 필요 
    //    //return true;
    //    return false;
    //}
}
