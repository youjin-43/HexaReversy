using UnityEngine;

public class TurnPlayer2 : IState
{
    Player player;
    MouseManager MouseControll;
   
    

    //생성자
    public TurnPlayer2(Player player)
    {
        Debug.Log("현재 State : TurnPlayer2");
        this.player = player;
        MouseControll = player.GetComponent<MouseManager>();
    }

    void IState.Enter()
    {
        //2번 플레이어는 마우스 클릭 활성화 
        if (player.PunActorNumber == 2)
        {
            MouseControll.enabled = true;
        }
        UIManager.Instance.ShowTimeSlider(); //시간제한 슬라이더 보이기
        UIManager.Instance.StartDecTimer();//액션 타임 감소                                  
    }

    void IState.Excute()
    {
        //시간에 따라 슬라이더값 줄이기

    }

    void IState.Exit()
    {

    }
}
