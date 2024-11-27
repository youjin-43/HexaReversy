using UnityEngine;

public class TileInfo : MonoBehaviour
{
    /// <summary>
    /// 2: 아무도 놓지 않은 상태 
    /// 1: 선공 돌     
    /// 0: 후공 돌 
    /// </summary>
    public int State = 2;

    void Start()
    {
        State = 2; 
    }


}
