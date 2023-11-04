using System;
using UnityEngine;

public static class Events
{
    //--------------------
    #region Events

    // Transitions
    public static event Action<bool> OnStartTransition;
    public static event Action<bool> OnEndTransition;
    public static event EventHandler OnTransitionFinished;

    // GameManager
    public static event EventHandler OnGameStart;
    public static event EventHandler OnGameEnd;
    public static event EventHandler OnIntroStart;
    public static event EventHandler OnIncreaseScore;
    public static event Action<int> OnScoreChanged;
    public static event Action<float> OnTimerChanged;
    public static event EventHandler OnNewHighscore;

    // WaveManager
    public static event EventHandler OnWaveIntervalTimerDepleted;

    // AudioManager
    public static event EventHandler OnBgmFadeOut;

    // UIManager
    public static event EventHandler OnGameOverCutsceneStarted;

    // General UI
    public static event EventHandler OnMenuButtonPressed;

    // Player
    public static event EventHandler OnGamePaused;
    public static event EventHandler OnGameUnpaused;

    // Entities
    public static event Action<Transform> OnCartHit;
    public static event EventHandler OnCarrierFilled;
    public static event Action<GameObject> OnObjectReadyToFinish;

    #endregion
    //--------------------

    //--------------------
    #region Public Methods

    // Transitions
    public static void StartTransition(bool delayTransition)
    {
        OnStartTransition?.Invoke(delayTransition);
    }

    public static void EndTransition(bool delayTransition)
    {
        OnEndTransition?.Invoke(delayTransition);
    }

    public static void TransitionFinished()
    {
        OnTransitionFinished?.Invoke(null, EventArgs.Empty);
    }

    // GameManager
    public static void IntroStart()
    {
        OnIntroStart?.Invoke(null, EventArgs.Empty);
    }

    public static void GameStart()
    {
        OnGameStart?.Invoke(null, EventArgs.Empty);
    }

    public static void GameEnd()
    {
        OnGameEnd?.Invoke(null, EventArgs.Empty);
    }

    public static void ScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }

    public static void TimerChanged(float timer)
    {
        OnTimerChanged?.Invoke(timer);
    }

    public static void NewHighscore()
    {
        OnNewHighscore?.Invoke(null, EventArgs.Empty);
    }

    // WaveManager
    public static void WaveIntervalTimerDepleted()
    {
        OnWaveIntervalTimerDepleted?.Invoke(null, EventArgs.Empty);
    }

    // AudioManager

    public static void FadeOutBgm()
    {
        OnBgmFadeOut?.Invoke(null, EventArgs.Empty);
    }

    // UIManager

    public static void StartingGameOverCutscene()
    {
        OnGameOverCutsceneStarted?.Invoke(null, EventArgs.Empty);
    }

    // General UI

    public static void MenuButtonPressed()
    {
        OnMenuButtonPressed?.Invoke(null, EventArgs.Empty);
    }

    // Player
    public static void PausePressed()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.IsGameActive)
            {
                OnGamePaused?.Invoke(null, EventArgs.Empty);
            }
            else if (GameManager.Instance.IsGamePaused)
            {
                OnGameUnpaused?.Invoke(null, EventArgs.Empty);
            }
        }
    }

    // Entities
    public static void CartHit(Transform cartTransform)
    {
        OnCartHit?.Invoke(cartTransform);
        OnIncreaseScore?.Invoke(null, EventArgs.Empty);
    }

    public static void CarrierFilled()
    {
        OnCarrierFilled?.Invoke(null, EventArgs.Empty);
    }

    public static void CarrierEndOfPath(GameObject carrier)
    {
        OnObjectReadyToFinish?.Invoke(carrier);
    }

    #endregion
    //--------------------
}
