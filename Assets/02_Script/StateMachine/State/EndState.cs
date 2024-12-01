using UnityEngine;

public class EndState : IState
{
    Player player;
    //TODO : END 부분은 아직 생각을 안해봐서^^7 마우스 컨트롤 관련해서 우선 안되도록 함 
    MouseManager MouseControll;

    //생성자
    public EndState(Player player)
    {
        this.player = player;
        MouseControll = player.GetComponent<MouseManager>();
    }

    void IState.Enter()
    {
        MouseControll.enabled = false;
    }

    void IState.Excute()
    {

    }

    void IState.Exit()
    {

    }

    //bool IState.ShouldTransition(out IState nextState)
    //{
    //    nextState = player.stateMachine.gameStartState; // 미리 생성된 상태 객체 사용
    //    //TODO : 수정 필요 
    //    //return true;
    //    return false;
    //}
}
