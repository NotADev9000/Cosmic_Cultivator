using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameActive { get; private set; } = false;
    [SerializeField] private float gameTimer = 60f;
    public int Score { get; private set; } = 0;

    private void Awake()
    {
        LimitFPS();
        CreateSingleton();
        Events.OnIncreaseScore += Events_OnIncreaseScore;
    }

    private void Start()
    {
        IsGameActive = true;
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void OnDestroy()
    {
        Events.OnIncreaseScore -= Events_OnIncreaseScore;
    }

    //--------------------
    #region Timer

    private void UpdateTimer()
    {
        if (IsGameActive)
        {
            if (gameTimer <= 0)
            {
                OnTimerEnd();
            }
            else
            {
                DecreaseTimer();
            }

            Events.TimerChanged(gameTimer);
        }
    }

    private void DecreaseTimer()
    {
        gameTimer -= Time.deltaTime;
    }

    private void OnTimerEnd()
    {
        IsGameActive = false;
        Time.timeScale = 0;
        Events.TimerEnded();
    }

    #endregion
    //--------------------

    //--------------------
    #region Score

    private void IncreaseScore()
    {
        Score++;
        Events.ScoreChanged(Score);
    }

    private void Events_OnIncreaseScore(object sender, EventArgs e)
    {
        IncreaseScore();
    }

    #endregion
    //--------------------

    private void LimitFPS()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    private void CreateSingleton()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of GameManager");
        }
        Instance = this;
    }
}
