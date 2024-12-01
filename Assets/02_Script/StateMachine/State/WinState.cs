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
}
