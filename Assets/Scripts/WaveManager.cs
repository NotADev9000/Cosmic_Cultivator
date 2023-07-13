using System;
using System.Collections.Generic;
using UnityEngine;

// pool of cowCarriers
// on awake/start cowCarriers are randomly positioned and given direction
// manager has a set interval timer
// after each timer a cowCarrier moves
// when cowCarrier reaches end of travel
//  -> manager resets cowCarrier position, direction & carts
// interval timer decreases as global timer decreases

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [SerializeField] private float intervalTimer = 2f;
    private float currentTimer;
    public event EventHandler OnIntervalTimerDepleted;

    private void Awake()
    {
        CreateSingleton();
    }

    private void Start()
    {
        currentTimer = intervalTimer;
    }

    private void Update()
    {
        //print(currentTimer + "\n=========");
        if (currentTimer <= 0)
        {
            OnIntervalTimerDepleted?.Invoke(this, EventArgs.Empty);
            currentTimer = intervalTimer;
        }
        currentTimer -= Time.deltaTime;
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
