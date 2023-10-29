using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Game Activity
    public bool IsGameActive
    {
        get { return !IsGameOver && !IsGamePaused && IsGameStarted; }
    }
    public bool IsIntroRunning { get; private set; } = false; // during intro countdown
    public bool IsGameStarted { get; private set; } = false;  // game has started after intro
    public bool IsGameOver { get; private set; } = false;     // game has ended
    public bool IsGamePaused { get; private set; } = false;   // game has been paused
    public bool CanResetGame { get; set; } = false;   // can reset game after: game over & ending menus

    // Game Management
    [SerializeField] private float introTimer = 3f;
    [SerializeField] private float gameTimer = 60f;
    private float countdownTimer = 0f;
    public int Score { get; private set; } = 0;

    private void Awake()
    {
        LimitFPS();
        CreateSingleton();
        AddEvents();
    }

    private void Start()
    {
        Events.EndTransition(false);
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
    #region Game Loop Management

    private void StartIntro()
    {
        IsIntroRunning = true;
        countdownTimer = introTimer;
    }

    private void StartGame()
    {
        IsIntroRunning = false;
        IsGameOver = false;
        IsGamePaused = false;
        IsGameStarted = true;
        countdownTimer = gameTimer;
        Events.GameStart();
    }

    private void EndGame()
    {
        IsGameOver = true;
        Events.FadeOutBgm();
        Events.GameEnd();
    }

    #endregion
    //--------------------

    //--------------------
    #region Set Events

    private void AddEvents()
    {
        Events.OnTransitionFinished += Events_OnTransitionFinished;
        Events.OnIncreaseScore += Events_OnIncreaseScore;
        Events.OnGamePaused += Events_OnGamePaused;
        Events.OnGameUnpaused += Events_OnGameUnpaused;
    }

    private void RemoveEvents()
    {
        Events.OnTransitionFinished -= Events_OnTransitionFinished;
        Events.OnIncreaseScore -= Events_OnIncreaseScore;
        Events.OnGamePaused -= Events_OnGamePaused;
        Events.OnGameUnpaused -= Events_OnGameUnpaused;
    }

    #endregion
    //--------------------

    //--------------------
    #region Events Activation

    private void Events_OnTransitionFinished(object sender, EventArgs e)
    {
        if (!IsGameStarted)
        {
            StartIntro();
        } 
        else if (IsGameOver)
        {
            Events.FadeOutBgm();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

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
        if (IsIntroRunning || IsGameActive)
        {
            if (countdownTimer <= 0)
            {
                OnTimerEnd();
            }
            else
            {
                DecreaseTimer();
            }

            Events.TimerChanged(countdownTimer);
        }
    }

    private void DecreaseTimer()
    {
        countdownTimer -= Time.deltaTime;
    }

    private void OnTimerEnd()
    {
        if (IsIntroRunning)
        {
            StartGame();
        }
        else
        {
            EndGame();
        }
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
