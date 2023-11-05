using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [Header("Timer")]
    [SerializeField] private float initialTimer = 0.5f;

    [Space(5)]

    [SerializeField] private AnimationCurve timerCurve;
    [SerializeField] private float startIntervalTimer = 2f;
    [SerializeField] private float endIntervalTimer = 0.5f;

    private float currentTimer;

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
        //print("INTERVAL: " + currentTimer.ToString());
    }

    private float GetTimer()
    {
        float progress = GameManager.Instance.EvaluateCurveFromTimerProgress(timerCurve);
        return Mathf.Lerp(startIntervalTimer, endIntervalTimer, progress);
    }

    private void CreateSingleton()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of WaveManager");
        }
        Instance = this;
    }
}
