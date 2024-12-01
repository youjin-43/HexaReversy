using UnityEngine;
using UnityEngine.UI;

public class TurnPlayer1 : IState
{
    Player player;
    MouseManager MouseControll;
    Slider slider;

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

        //1번 플레이어는 마우스 클릭 활성화 
        if (player.PunActorNumber==1)
        {
            MouseControll.enabled = true;
        }

        UIManager.Instance.ShowTimeSlider(); //시간제한 슬라이더 타이머 보이기
        slider.value = GameManager.Instance.actionTime; //타이머 초기화 
    }

    void IState.Excute()
    {
        //슬라이더 타이머 감소 
        slider.value -= Time.deltaTime;

        //돌을 놓거나 시간 제한이 끝나면 
        if (MouseControll.PutTile() || slider.value <= 0)
        {
            MouseControll.UnSeletTile();//아웃라인된게 있으면 끄기 
            MouseControll.enabled = false; //마우스 클릭 비활성화 
            UIManager.Instance.HideTimeSlider(); //시간제한 슬라이더 숨기기 
            player.TransitionTo(player.stateMachine.turnPlayer2); //플레이어 2의 턴으로 넘어감 
        }
    }

    void IState.Exit()
    {

    }

}
