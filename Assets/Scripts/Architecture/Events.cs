using System;
using UnityEngine;

public static class Events
{
    //--------------------
    #region Events

    // GameManager
    public static event EventHandler OnIncreaseScore;
    public static event Action<int> OnScoreChanged;

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

    // WaveManager
    public static void WaveIntervalTimerDepleted()
    {
        OnWaveIntervalTimerDepleted?.Invoke(null, EventArgs.Empty);
    }

    #endregion
    //--------------------
}
