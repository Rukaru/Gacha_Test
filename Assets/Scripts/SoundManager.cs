using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("BGM")]
    [SerializeField] private AudioClip _bgm;

    [Header("SE")]
    [SerializeField] private AudioClip _buttonSE;
    [SerializeField] private AudioClip _gachaSE;

    private AudioSource _bgmSource;
    private AudioSource _seSource;

    private void Awake()
    {
        // シングルトン
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // BGM用
        _bgmSource = gameObject.AddComponent<AudioSource>();
        _bgmSource.loop = true;
        _bgmSource.playOnAwake = false;

        // SE用
        _seSource = gameObject.AddComponent<AudioSource>();
        _seSource.playOnAwake = false;
    }

    private void Start()
    {
        PlayBGM();
    }

    public void PlayBGM()
    {
        if (_bgm == null)
        {
            Debug.LogWarning("BGMが設定されていません");
            return;
        }

        _bgmSource.clip = _bgm;
        _bgmSource.Play();
    }

    public void PlayButtonSE()
    {
        if (_buttonSE == null)
        {
            Debug.LogWarning("ボタンSEが設定されていません");
            return;
        }

        _seSource.PlayOneShot(_buttonSE);
    }

    public void PlayGachaSE()
    {
        if (_gachaSE == null)
        {
            Debug.LogWarning("ガチャSEが設定されていません");
            return;
        }

        _seSource.PlayOneShot(_gachaSE);
    }
}