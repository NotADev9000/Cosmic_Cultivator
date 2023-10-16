using System;
using UnityEngine;

public static class Events
{
    //--------------------
    #region Events

    // Transitions
    public static event EventHandler OnStartTransition;
    public static event EventHandler OnEndTransition;
    public static event EventHandler OnTransitionFinished;

    // GameManager
    public static event EventHandler OnGameStart;
    public static event EventHandler OnIncreaseScore;
    public static event Action<int> OnScoreChanged;
    public static event Action<float> OnTimerChanged;
    public static event EventHandler OnTimerEnd;

    // Player
    public static event EventHandler OnGamePaused;
    public static event EventHandler OnGameUnpaused;

    // WaveManager
    public static event EventHandler OnWaveIntervalTimerDepleted;

    #endregion
    //--------------------

    //--------------------
    #region Public Methods

    // Transitions
    public static void StartTransition()
    {
        OnStartTransition?.Invoke(null, EventArgs.Empty);
    }

    public static void EndTransition()
    {
        OnEndTransition?.Invoke(null, EventArgs.Empty);
    }

    public static void TransitionFinished()
    {
        OnTransitionFinished?.Invoke(null, EventArgs.Empty);
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
    public static void CartHit()
    {
        OnIncreaseScore?.Invoke(null, EventArgs.Empty);
    }

    // GameManager
    public static void GameStart()
    {
        OnGameStart?.Invoke(null, EventArgs.Empty);
    }

    public static void ScoreChanged(int score)
    {
        OnScoreChanged?.Invoke(score);
    }

    public static void TimerChanged(float timer)
    {
        OnTimerChanged?.Invoke(timer);
    }

    public static void TimerEnded()
    {
        OnTimerEnd?.Invoke(null, EventArgs.Empty);
    }

    // WaveManager
    public static void WaveIntervalTimerDepleted()
    {
        OnWaveIntervalTimerDepleted?.Invoke(null, EventArgs.Empty);
    }

    #endregion
    //--------------------
}
