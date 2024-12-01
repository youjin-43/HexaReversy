using UnityEngine;

public class LoseState : IState
{
    Player player;
    MouseManager MouseControll;

    //생성자
    public LoseState(Player player)
    {
        this.player = player;
        MouseControll = player.GetComponent<MouseManager>();
    }

    void IState.Enter()
    {
        Debug.Log("현재 State : Lose");
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
}
