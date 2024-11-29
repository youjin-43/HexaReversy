using UnityEngine;

public interface IState
{
    public void Enter(); // 상태에 처음 진입할 때 실행되는 코드
    public void Excute(); // 프레임당 로직. 새로운 상태로 전환하는 조건 포함. Tick이라고도 함
    public void Exit(); // 상태에서 벗어날 때 실행되는 코드
}