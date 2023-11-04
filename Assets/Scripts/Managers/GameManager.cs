using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Game Data
    [SerializeField] private float introTime = 3f;
    [SerializeField] private float gameTime = 60f;

    [Space(10)]

    [SerializeField] private float increaseTimerOnCarrierFilled = 1f;

    [Space(10)]

    [SerializeField] private AnimationCurve noOfCartsCurve;
    [SerializeField] private int minNoOfCarts = 1;
    [SerializeField] private int maxNoOfCarts = 5;

    // Game State
    public bool IsGameActive
    {
        get { return !IsGameOver && !IsGamePaused && IsGameStarted; }
    }
    public bool IsIntroRunning { get; private set; } = false; // during intro countdown
    public bool IsGameStarted { get; private set; } = false;  // game has started after intro
    public bool IsGameOver { get; private set; } = false;     // game has ended
    public bool IsGamePaused { get; private set; } = false;   // game has been paused
    public bool CanResetGame { get; set; } = false;           // can reset game after: game over & ending menus
    
    // Game Management
    private float countdownTimer = 0f;
    private float TimerProgressNormalized { get { return 1 - (countdownTimer/gameTime); } }
    private int score = 0;

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
    #region Game State Management

    private void StartIntro()
    {
        IsIntroRunning = true;
        countdownTimer = introTime;
    }

    private void StartGame()
    {
        IsIntroRunning = false;
        IsGameOver = false;
        IsGamePaused = false;
        IsGameStarted = true;
        countdownTimer = gameTime;
        Events.GameStart();
    }

    private void EndGame()
    {
        IsGameOver = true;
        TrySaveHighscore();
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
        Events.OnCarrierFilled += Events_OnCarrierFilled;
    }

    private void RemoveEvents()
    {
        Events.OnTransitionFinished -= Events_OnTransitionFinished;
        Events.OnIncreaseScore -= Events_OnIncreaseScore;
        Events.OnGamePaused -= Events_OnGamePaused;
        Events.OnGameUnpaused -= Events_OnGameUnpaused;
        Events.OnCarrierFilled -= Events_OnCarrierFilled;
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

    private void Events_OnCarrierFilled(object sender, EventArgs e)
    {
        AddToTimer(increaseTimerOnCarrierFilled);
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
                CountdownTimer();
            }
        }
    }

    private void CountdownTimer()
    {
        AddToTimer(-Time.deltaTime);
    }

    private void AddToTimer(float amount)
    {
        countdownTimer += amount;
        Events.TimerChanged(countdownTimer);
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
        score++;
        Events.ScoreChanged(score);
    }

    private void Events_OnIncreaseScore(object sender, EventArgs e)
    {
        IncreaseScore();
    }

    private bool IsHighscore()
    {
        return score > PlayerPrefs.GetInt("Highscore");
    }

    private void TrySaveHighscore()
    {
        if (IsHighscore())
        {
            Events.NewHighscore();
            SaveHighscore();
        }
    }

    private void SaveHighscore()
    {
        PlayerPrefs.SetInt("Highscore", score);
    }

    #endregion
    //--------------------

    //--------------------
    #region Game Progression

    public int GetNumberOfCarts()
    {
        float progress = noOfCartsCurve.Evaluate(TimerProgressNormalized);
        return Mathf.CeilToInt(Mathf.Lerp(minNoOfCarts - 1, maxNoOfCarts, progress));
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
