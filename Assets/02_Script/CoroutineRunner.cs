using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour
{
    private static CoroutineRunner _instance;
    public static CoroutineRunner Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("CoroutineRunner");
                _instance = obj.AddComponent<CoroutineRunner>();
                DontDestroyOnLoad(obj); // 씬이 바뀌어도 유지
            }
            return _instance;
        }
    }

    public void RunCoroutine(IEnumerator coroutine, System.Action onComplete = null)
    {
        StartCoroutine(RunAndCallback(coroutine, onComplete));
    }

    private IEnumerator RunAndCallback(IEnumerator coroutine, System.Action onComplete)
    {
        yield return StartCoroutine(coroutine);
        onComplete?.Invoke();
    }
}
