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
        Debug.Log("현재 State : GameStartState");

        MouseControll.enabled = false;
        UIManager.Instance.PlayIntroUI();
        player.stateMachine.TransitionTo(player.stateMachine.turnPlayer1); //플레이어 1의 턴으로 시작 
    }

    void IState.Excute()
    {
       
    }

    void IState.Exit()
    {
     
    }
}
