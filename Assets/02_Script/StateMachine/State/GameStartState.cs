using UnityEngine;

public class GameStartState : IState
{
    Player player;
    MouseManager MouseControll;

    //생성자
    public GameStartState(Player player)
    {
        this.player = player;
        MouseControll = player.GetComponent<MouseManager>(); 
    }

    void IState.Enter()
    {
        MouseControll.enabled = false;
        UIManager.Instance.PlayIntroUI();        
    }

    void IState.Excute()
    {
       
    }

    void IState.Exit()
    {
     
    }

    //bool IState.ShouldTransition(out IState nextState)
    //{
    //    nextState = player.stateMachine.turnPlayer1; // 미리 생성된 상태 객체 사용
    //    //TODO : 수정 필요 
    //    //return true;
    //    return false;
    //}
}
