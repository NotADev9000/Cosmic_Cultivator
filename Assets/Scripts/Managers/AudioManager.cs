using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] private AudioSource audioSource_bgm;
    [SerializeField] private AudioSource audioSource_sfx;
    private float bgmTime; // how many seconds of BGM have played

    [Space(10)]

    [Header("BGM Clips")]
    [SerializeField] private AudioClip gameBgmClip;

    [Space(10)]

    [Header("SFX Clips")]
    [SerializeField] private AudioClip scoreIncreaseClip;
    [SerializeField] private AudioClip pauseClip;
    [SerializeField] private AudioClip unpauseClip;

    private void Awake()
    {
        if (CreateSingleton())
        {
            Events.OnIncreaseScore += Events_OnIncreaseScore;
            Events.OnTimerEnd += Events_OnTimerEnd;
            Events.OnGameStart += Events_OnGameStart;
            Events.OnGamePaused += Events_OnGamePaused;
            Events.OnGameUnpaused += Events_OnGameUnpaused;
        }
    }

    private void OnDestroy()
    {
        Events.OnIncreaseScore -= Events_OnIncreaseScore;
        Events.OnTimerEnd -= Events_OnTimerEnd;
        Events.OnGameStart -= Events_OnGameStart;
        Events.OnGamePaused -= Events_OnGamePaused;
        Events.OnGameUnpaused -= Events_OnGameUnpaused;
    }

    private void Events_OnIncreaseScore(object sender, System.EventArgs e)
    {
        if (scoreIncreaseClip != null)
        {
            audioSource_bgm.PlayOneShot(scoreIncreaseClip);
        }
    }

    private void Events_OnTimerEnd(object sender, System.EventArgs e)
    {
        audioSource_bgm.Stop();
    }

    private void Events_OnGameStart(object sender, System.EventArgs e)
    {
        audioSource_bgm.clip = gameBgmClip;
        audioSource_bgm.Play();
    }

    private void Events_OnGamePaused(object sender, System.EventArgs e)
    {
        bgmTime = audioSource_bgm.time;
        audioSource_bgm.Pause();
        audioSource_sfx.PlayOneShot(pauseClip);
    }

    private void Events_OnGameUnpaused(object sender, System.EventArgs e)
    {
        audioSource_bgm.time = bgmTime;
        audioSource_sfx.PlayOneShot(unpauseClip);
        audioSource_bgm.UnPause();
    }

    private bool CreateSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return false;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return true;
        }
    }
}
