using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sources")]
    [SerializeField] private AudioSource audioSource_bgm;
    [SerializeField] private AudioSource audioSource_sfx;
    private float bgmTime; // how many seconds of BGM have played

    //------------------------------------------------------------------
    [Space(10)]

    [Header("BGM Clips")]
    [SerializeField] private AudioClip gameBgmClip;
    [SerializeField] private AudioClip gameOverClip;

    //------------------------------------------------------------------
    [Space(10)]

    [Header("SFX Clips")]
    [SerializeField] private AudioClip menuButtonClip;
    [SerializeField] private AudioClip scoreIncreaseClip;
    [SerializeField] private AudioClip pauseClip;
    [SerializeField] private AudioClip unpauseClip;
    [SerializeField] private AudioClip carrierFilledClip;

    //------------------------------------------------------------------

    private void Awake()
    {
        if (CreateSingleton()) 
            AddEvents();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    //--------------------
    #region Set Events

    private void AddEvents()
    {
        Events.OnMenuButtonPressed += Events_OnMenuButtonPressed;
        Events.OnIncreaseScore += Events_OnIncreaseScore;
        Events.OnGameStart += Events_OnGameStart;
        Events.OnGamePaused += Events_OnGamePaused;
        Events.OnGameUnpaused += Events_OnGameUnpaused;
        Events.OnGameOverCutsceneStarted += Events_OnGameOverCutsceneStarted;
        Events.OnBgmFadeOut += Events_OnBgmFadeOut;
        Events.OnCarrierFilled += Events_OnCarrierFilled;
    }

    private void RemoveEvents()
    {
        Events.OnMenuButtonPressed -= Events_OnMenuButtonPressed;
        Events.OnIncreaseScore -= Events_OnIncreaseScore;
        Events.OnGameStart -= Events_OnGameStart;
        Events.OnGamePaused -= Events_OnGamePaused;
        Events.OnGameUnpaused -= Events_OnGameUnpaused;
        Events.OnGameOverCutsceneStarted -= Events_OnGameOverCutsceneStarted;
        Events.OnBgmFadeOut -= Events_OnBgmFadeOut;
        Events.OnCarrierFilled -= Events_OnCarrierFilled;
    }

    #endregion
    //--------------------

    //--------------------
    #region Events Activation

    // Game Management
    private void Events_OnGameStart(object sender, System.EventArgs e)
    {
        audioSource_bgm.clip = gameBgmClip;
        ResetBgmClip();
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

    private void Events_OnGameOverCutsceneStarted(object sender, System.EventArgs e)
    {
        audioSource_bgm.clip = gameOverClip;
        ResetBgmClip();
        audioSource_bgm.Play();
    }

    // UI
    private void Events_OnMenuButtonPressed(object sender, System.EventArgs e)
    {
        audioSource_sfx.PlayOneShot(menuButtonClip);
    }


    // Gameplay
    private void Events_OnCarrierFilled(object sender, EventArgs e)
    {
        audioSource_sfx.PlayOneShot(carrierFilledClip);
    }

    private void Events_OnIncreaseScore(object sender, System.EventArgs e)
    {
        if (scoreIncreaseClip != null)
        {
            audioSource_sfx.PlayOneShot(scoreIncreaseClip);
        }
    }

    // Music & SFX
    private void Events_OnBgmFadeOut(object sender, System.EventArgs e)
    {
        FadeOutBgm(1);
    }


    #endregion
    //--------------------

    //--------------------
    #region Audio Manipulation

    private void ResetBgmClip()
    {
        audioSource_bgm.time = 0;
        audioSource_bgm.volume = 1;
    }

    private void FadeOutBgm(float time)
    {
        StartCoroutine(FadeAudioSource(audioSource_bgm, 0, time));
    }

    private IEnumerator FadeAudioSource(AudioSource source, float targetVolume, float fadeDuration)
    {
        float startVolume = source.volume;
        float secondsSinceFadeStarted = 0;

        while (secondsSinceFadeStarted < fadeDuration)
        {
            secondsSinceFadeStarted += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, targetVolume, secondsSinceFadeStarted / fadeDuration);
            yield return null;
        }
    }

    #endregion
    //--------------------

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
