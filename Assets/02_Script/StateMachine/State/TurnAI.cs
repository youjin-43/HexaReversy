using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnAI : IState
{
    Player player;

    //생성자
    public TurnAI(Player player)
    {
        this.player = player;
    }

    void IState.Enter()
    {
        Debug.Log("현재 State : TurnAI");

        TileManager.Instance.FindSelectableBoundatry();
        Cube PutTile = TileManager.Instance.PutTile_InRandomPos();

        // 여기서 뒤집는 코루틴이 끝날 때까지 기다렸다가 다음 함수 실행
        CoroutineRunner.Instance.RunCoroutine(
            TileManager.Instance.AllTiles[PutTile].FlipWithDelay(), // 코루틴 호출
            () =>
            {
                // 코루틴 완료 후 실행
                if (TileManager.Instance.Check_IsGameEnd())
                {
                    player.TransitionTo(player.stateMachine.endState); // 게임 끝
                }
                else
                {
                    player.TransitionTo(player.stateMachine.turnPlayer1); // 플레이어 1의 턴으로 넘어감
                }
            }
        );


    }


    void IState.Excute()
    {


    }


    void IState.Exit()
    {

    }

}
