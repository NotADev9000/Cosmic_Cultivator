using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool WaveActive { get; private set; } = false;
    [SerializeField] private float gameTimer = 60f;
    private int score = 0;

    private void Awake()
    {
        LimitFPS();
        CreateSingleton();
        Events.OnIncreaseScore += Events_OnIncreaseScore;
    }

    private void Start()
    {
        WaveActive = true;
    }

    private void Update()
    {
        UpdateTimer();
    }

    //--------------------
    #region Timer

    private void UpdateTimer()
    {
        if (gameTimer <= 0)
        {
            WaveActive = false;
        }
        else
        {
            int time = Mathf.CeilToInt(gameTimer);
            print("Time: " + time.ToString());

            gameTimer -= Time.deltaTime;
        }
    }

    #endregion
    //--------------------

    //--------------------
    #region Score

    private void IncreaseScore()
    {
        score++;
        print("Score: " + score.ToString());
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
