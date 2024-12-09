using UnityEngine;
using UnityEngine.UI;

public class TurnPlayer2 : IState
{
    Player player;
    MouseManager MouseControll;
    Slider slider;

    //생성자
    public TurnPlayer2(Player player)
    {
        this.player = player;
        MouseControll = player.GetComponent<MouseManager>();
        slider = UIManager.Instance.TimeSlider.GetComponent<Slider>();
    }

    void IState.Enter()
    {
        Debug.Log("현재 State : TurnPlayer2");

        if (player.PunActorNumber == 2)
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

                //todo : 타이머 재활용 할 수 있으려나?
                slider.value = 2; //타이머 초기화 -> 2초뒤 넘어가도록 
            }

        }


    }

    void IState.Excute()
    {
        //슬라이더 타이머 감소 
        slider.value -= Time.deltaTime;

        //2번 플레이어가 
        if (player.PunActorNumber == 2)
        {
            //돌을 놓거나 시간제한이 끝나면 
            //돌을 놓거나 시간 제한이 끝나면
            bool isPUT = MouseControll.PutTile();
            if (isPUT || slider.value <= 0)
            {
                //시간 안에 돌을 놓지 못한경우 랜덤으로 알아서 타일을 놔줌
                if (isPUT == false) TileManager.Instance.PutTile_InRandomPos();

                TileManager.Instance.UnhighlightSelectableTiles();//하이라이트 비활성화 
                MouseControll.enabled = false; //마우스 클릭 비활성화 
                UIManager.Instance.HideTimeSlider(); //시간제한 슬라이더 숨기기 

                if(TileManager.Instance.Check_IsGameEnd()){
                    player.TransitionTo(player.stateMachine.endState); //게임 끝 
                }else{
                    player.TransitionTo(player.stateMachine.turnPlayer1); //플레이어 1의 턴으로 넘어감 
                }
            }
        }

    }

    void IState.Exit()
    {
        UIManager.Instance.HidePassText();
    }
}
