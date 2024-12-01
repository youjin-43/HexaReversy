using UnityEngine;
using Photon.Pun;

public class StateMachine 
{

    Player player;
    public IState CurrentState { get; private set; } //현재 상태는 읽을수는 있지만 셋팅은 못하게

    //관리할 State들
    public GameStartState gameStartState;
    public TurnPlayer1 turnPlayer1;
    public TurnPlayer2 turnPlayer2;
    public LoseState loseState;
    public WinState winState;
    public EndState endState;

    //생성자 
    public StateMachine(Player player)
    {
        this.player = player;

        //각 스테이트 생성
        gameStartState = new GameStartState(player);
        turnPlayer1 = new TurnPlayer1(player);
        turnPlayer2 = new TurnPlayer2(player);
        loseState = new LoseState(player);
        winState = new WinState(player);
        endState = new EndState(player);
    }


    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        if (player.pv == null)
        {
            Debug.LogError("PhotonView가 Player 객체에 없습니다!");
        }
        player.pv.RPC("SyncStateTransition", RpcTarget.All, nextState.GetType().Name); // 모든 클라이언트에 전송
    }

    [PunRPC]
    private void SyncStateTransition(string stateName)
    {
        CurrentState.Exit();
        // 상태 이름을 통해 적절한 상태로 전환
        switch (stateName)
        {
            case "TurnPlayer1":
                CurrentState = player.stateMachine.turnPlayer1; 
                break;
            case "TurnPlayer2":
                CurrentState = player.stateMachine.turnPlayer2;
                break;

                // TODO :다른 상태들 추가...
        }

        CurrentState.Enter();
    }

    //public void TransitionTo(IState nextState)
    //{
    //    CurrentState.Exit();
    //    CurrentState = nextState;
    //    CurrentState.Enter();
    //}

    public void Excute()
    {
        CurrentState.Excute();

        //// 상태 전환 조건 확인
        //if (CurrentState.ShouldTransition(out IState nextState))
        //{
        //    TransitionTo(nextState);
        //}
    }

}
