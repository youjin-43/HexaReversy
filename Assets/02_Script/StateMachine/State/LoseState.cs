using UnityEngine;

public class LoseState : IState
{
    Player player;

    //생성자
    public LoseState(Player player)
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
