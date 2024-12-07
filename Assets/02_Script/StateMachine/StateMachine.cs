using UnityEngine;

public class StateMachine 
{

    public IState CurrentState { get; private set; } //현재 상태는 읽을수는 있지만 셋팅은 못하게

    //관리할 State들
    public GameStartState gameStartState;
    public TurnPlayer1 turnPlayer1;
    public TurnPlayer2 turnPlayer2;
    public LoseState loseState;
    public WinState winState;
    public EndState endState;

    //생성자
    Player player;
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

    public void Excute()
    {
        CurrentState.Excute();
    }

    //각 스테이트로 셋팅 
    public void SetStateToTurnPlayer1(){ CurrentState = turnPlayer1; }
    public void SetStateToTurnPlayer2(){ CurrentState = turnPlayer2; }
    public void SetStateToEnd() { CurrentState = endState; }

    //이 뒤는 아직 덜 구현돼서 미사용하는 상태들 
    public void SetStateToWin() { CurrentState = winState; }
    public void SetStateToLose() { CurrentState = loseState; }
    
}
