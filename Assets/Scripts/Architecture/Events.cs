using System;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public static class Events
{
    //--------------------
    #region Events

    // GameManager
    public static event EventHandler OnIncreaseScore;
    public static event Action<int> OnScoreChanged;
    public static event Action<float> OnTimerChanged;
    public static event EventHandler OnTimerEnd;

    // WaveManager
    public static event EventHandler OnWaveIntervalTimerDepleted;

    #endregion
    //--------------------

    //--------------------
    #region Public Methods

    // Entities
    public static void CartHit()
    {
        OnIncreaseScore?.Invoke(null, EventArgs.Empty);
    }

    // GameManager
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
