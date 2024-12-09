using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnPlayer1 : IState
{
    Player player;
    MouseManager MouseControll;
    Slider slider;
    bool isPut = false;

    //생성자
    public TurnPlayer1(Player player)
    {
        this.player = player;
        MouseControll = player.GetComponent<MouseManager>();
        slider = UIManager.Instance.TimeSlider;
    }

    void IState.Enter()
    {
        Debug.Log("현재 State : TurnPlayer1");
        isPut = false;
        if (player.PunActorNumber==1)
        {
            TileManager.Instance.HighlightSelectableTiles(); //돌 놓을 수 있는 곳 활성화

            if (TileManager.Instance.SelectableBoundaryTile.Count > 0)
            {
                //놓을 수 있는 곳이 있으면 1번 플레이어는 마우스 클릭 활성화 
                MouseControll.enabled = true;
                UIManager.Instance.ShowTimeSlider(); //시간제한 슬라이더 타이머 보이기
                slider.value = GameManager.Instance.actionTime; //타이머 초기화
            }
            else
            {
                //돌을 놓을 수 있는곳이 없으면 pass 보여주고 바로 다음턴으로 넘어감
                UIManager.Instance.ShowPassText();
                isPut = true; //놨다 침 

                //todo : 타이머 재활용 할 수 있으려나?
                slider.value = 2; //타이머 초기화 -> 2초뒤 넘어가도록 
            }
            
        }

        
    }


    void IState.Excute()
    {
        // 슬라이더 타이머 감소
        slider.value -= Time.deltaTime;

        // 1번 플레이어가 
        if (player.PunActorNumber == 1 && isPut == false)
        {
            // 돌을 놓거나 시간 제한이 끝나면
            Cube PutTile = MouseControll.PutTile();
            if (PutTile != null || slider.value <= 0)
            {
                isPut = true;

                // 시간 안에 돌을 놓지 못한 경우 랜덤으로 알아서 타일을 놔줌
                if (PutTile == null) PutTile = TileManager.Instance.PutTile_InRandomPos();

                MouseControll.enabled = false; // 마우스 클릭 비활성화
                TileManager.Instance.UnhighlightSelectableTiles(); // 하이라이트 비활성화 
                UIManager.Instance.HideTimeSlider(); // 시간제한 슬라이더 숨기기

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
                            player.TransitionTo(player.stateMachine.turnPlayer2); // 플레이어 2의 턴으로 넘어감
                        }
                    }
                );
            }
        }

        //놓을 수 있는곳이 없는 경우 시간이 지나면 
        if (TileManager.Instance.SelectableBoundaryTile.Count ==0 && slider.value <= 0)
        {
            UIManager.Instance.HidePassText();
            player.TransitionTo(player.stateMachine.turnPlayer2); // 플레이어 2의 턴으로 넘어감
        }
    }


    void IState.Exit()
    {
        
    }

}
