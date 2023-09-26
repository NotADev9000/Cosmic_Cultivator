using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] private AudioSource audioSource_bgm;
    [SerializeField] private AudioSource audioSource_sfx;

    [Space(10)]

    [Header("Clips")]
    [SerializeField] private AudioClip scoreIncreaseClip;
    [SerializeField] private AudioClip pauseClip;
    [SerializeField] private AudioClip unpauseClip;

    private void Awake()
    {
        Events.OnIncreaseScore += Events_OnIncreaseScore;
        Events.OnTimerEnd += Events_OnTimerEnd;
        Events.OnGamePaused += Events_OnGamePaused;
        Events.OnGameUnpaused += Events_OnGameUnpaused;
    }

    private void OnDestroy()
    {
        Events.OnIncreaseScore -= Events_OnIncreaseScore;
        Events.OnTimerEnd -= Events_OnTimerEnd;
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

    private void Events_OnGamePaused(object sender, System.EventArgs e)
    {
        audioSource_bgm.Pause();
        audioSource_sfx.PlayOneShot(pauseClip);
    }

    private void Events_OnGameUnpaused(object sender, System.EventArgs e)
    {
        audioSource_sfx.PlayOneShot(unpauseClip);
        audioSource_bgm.Play();
    }
}
