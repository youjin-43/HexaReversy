using UnityEngine;

public class WinState : IState
{
    Player player;

    //생성자
    public WinState(Player player)
    {
        this.player = player;
    }

    void IState.Enter()
    {

    }

    void IState.Excute()
    {

    }

    void IState.Exit()
    {

    }
}
