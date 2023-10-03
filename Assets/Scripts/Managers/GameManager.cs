using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Game Activity
    public bool IsGameActive
    {
        get { return !IsGameOver && !IsGamePaused; }
    }
    public bool IsGameOver { get; private set; } = false;
    public bool IsGamePaused { get; private set; } = false;

    // Game Management
    [SerializeField] private float gameTimer = 60f;
    public int Score { get; private set; } = 0;

    private void Awake()
    {
        LimitFPS();
        CreateSingleton();
        AddEvents();
    }

    private void Start()
    {
        IsGameOver = false;
        IsGamePaused = false;
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    //--------------------
    #region Events

    private void AddEvents()
    {
        Events.OnIncreaseScore += Events_OnIncreaseScore;
        Events.OnGamePaused += Events_OnGamePaused;
        Events.OnGameUnpaused += Events_OnGameUnpaused;
    }

    private void RemoveEvents()
    {
        Events.OnIncreaseScore -= Events_OnIncreaseScore;
        Events.OnGamePaused -= Events_OnGamePaused;
        Events.OnGameUnpaused -= Events_OnGameUnpaused;
    }

    #endregion
    //--------------------

    //--------------------
    #region Activity

    private void Events_OnGamePaused(object sender, EventArgs e)
    {
        IsGamePaused = true;
        Time.timeScale = 0;
    }

    private void Events_OnGameUnpaused(object sender, EventArgs e)
    {
        IsGamePaused = false;
        Time.timeScale = 1;
    }

    #endregion
    //--------------------

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
        IsGameOver = true;
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
            Destroy(gameObject);
        }
        Instance = this;
    }
}
