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

    public bool ShouldTransition()
    {
        return false;
    }
}
