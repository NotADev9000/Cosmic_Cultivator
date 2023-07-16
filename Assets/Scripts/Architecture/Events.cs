using System;
using UnityEngine;

public static class Events
{
    //--------------------
    #region Events

    // GameManager
    public static event EventHandler OnIncreaseScore;

    // WaveManager
    public static event EventHandler OnWaveIntervalTimerDepleted;
    
    // Entities
    //public static event Action<Collider2D> OnLaserHitAction;

    #endregion
    //--------------------

    //--------------------
    #region Public Methods

    // Entities
    public static void CartHit()
    {
        OnIncreaseScore?.Invoke(null, EventArgs.Empty);
    }

    //public static void LaserHit(Collider2D colliderHit)
    //{
    //    OnLaserHitAction?.Invoke(colliderHit);
    //}

    // WaveManager
    public static void WaveIntervalTimerDepleted()
    {
        OnWaveIntervalTimerDepleted?.Invoke(null, EventArgs.Empty);
    }

    #endregion
    //--------------------
}
