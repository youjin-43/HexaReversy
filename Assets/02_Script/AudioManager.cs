using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        #region 싱글턴 
        if (_instance == null)
        {
            _instance = this;
            Debug.Log("AudioManager가 생성됐습니다");
            DontDestroyOnLoad(gameObject); // 씬이 변경되어도 삭제되지 않도록
        }
        else
        {
           
            Debug.LogWarning("씬에 두개 이상의 AudioManager가 존재합니다!");
            Destroy(gameObject);
            Debug.Log("Destroy AudioManager");
        }
        #endregion
    }
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [SerializeField] AudioClip Hover;
    [SerializeField] AudioClip Click;
    [SerializeField] AudioClip Go;
    [SerializeField] AudioClip Flip;

    public void PlayHoverSound()
    {
        audioSource.PlayOneShot(Hover);
    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(Click);
    }

    public void PlayGoSound()
    {
        audioSource.PlayOneShot(Go);
    }

    public void PlayFlipSound()
    {
        audioSource.PlayOneShot(Flip);
    }

}
