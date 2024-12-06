using UnityEngine;

public class GameStartState : IState
{
    Player player;
    MouseManager MouseControll;
    float TimeLimit = 1.5f;
    float timer = 0;

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
        timer = 0;
    }


    //1.5초뒤 플레이어 1의 턴으로 넘어감 
    void IState.Excute()
    {
        timer += Time.deltaTime;
       if(timer>=TimeLimit) player.TransitionTo(player.stateMachine.turnPlayer1); 
    }

    void IState.Exit()
    {
     
    }
}
