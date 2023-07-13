using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private float gameTimer = 60f;

    private void Awake()
    {
        CreateSingleton();
    }

    private void Update()
    {
        if (gameTimer <= 0)
        {
            print("TIME END");
        }
        else
        {
            print(gameTimer.ToString());
            gameTimer -= Time.deltaTime;
        }
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
