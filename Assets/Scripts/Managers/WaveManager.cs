using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates a timer that countsdown to 0 and sends out an Event upon doing so.
/// </summary>
public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [Header("Timer")]
    [SerializeField] private float initialTimer = 0.5f;

    [Space(5)]

    [SerializeField] private AnimationCurve timerCurve;
    [SerializeField] private float startIntervalTimer = 2f;
    [SerializeField] private float endIntervalTimer = 0.5f;

    //------------------------------------------------------------------

    private float currentTimer;

    //------------------------------------------------------------------

    private void Awake()
    {
        CreateSingleton();
        currentTimer = initialTimer;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameActive)
        {
            CheckIntervalTimer();
            DepleteIntervalTimer();
        }
    }

    //--------------------
    #region Timer Management

    private void CheckIntervalTimer()
    {
        if (currentTimer <= 0)
        {
            OnEndOfTimer();
        }
    }

    private void DepleteIntervalTimer()
    {
        currentTimer -= Time.deltaTime;
    }

    private void OnEndOfTimer()
    {
        Events.WaveIntervalTimerDepleted();
        currentTimer = GetTimer();
    }

    /// <summary>
    /// As the main game timer countsdown the interval timer starts at lower and lower values.
    /// e.g. at game start the interval timer countsdown from 2 seconds.
    ///      with 10 seconds remaining the interval timer countsdown from 0.5 seconds.
    /// </summary>
    /// <returns>The value the interval timer counts down from</returns>
    private float GetTimer()
    {
        float progress = GameManager.Instance.EvaluateCurveFromTimerProgress(timerCurve);
        return Mathf.Lerp(startIntervalTimer, endIntervalTimer, progress);
    }

    #endregion
    //--------------------

    private void CreateSingleton()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of WaveManager");
        }
        Instance = this;
    }
}
