using UnityEngine;

public class TurnPlayer1 : IState
{
    Player player;
    MouseManager MouseControll;

    //생성자
    public TurnPlayer1(Player player)
    {
        this.player = player;
        MouseControll = player.GetComponent<MouseManager>();
    }

    void IState.Enter()
    {
        Debug.Log("현재 State : TurnPlayer1");
        //1번 플레이어는 마우스 클릭 활성화 
        if (player.PunActorNumber==1)
        {
            MouseControll.enabled = true;
        }
        UIManager.Instance.ShowTimeSlider(); //시간제한 슬라이더 보이기
        UIManager.Instance.StartDecTimer();//액션 타임 감소
    }

    void IState.Excute()
    {
        MouseControll.PutTile(); //마우스 클릭하면 돌 놓음 
    }

    void IState.Exit()
    {

    }


}
