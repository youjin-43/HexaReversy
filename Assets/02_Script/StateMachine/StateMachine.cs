using UnityEngine;

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
        CurrentState.Exit();
        CurrentState = nextState;
        CurrentState.Enter();
    }

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
