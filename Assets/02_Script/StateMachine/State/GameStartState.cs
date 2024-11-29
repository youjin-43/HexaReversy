using UnityEngine;

public class GameStartState : IState
{
    Player player;

    //생성자
    public GameStartState(Player player)
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
