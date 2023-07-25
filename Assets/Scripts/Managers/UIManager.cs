using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreField;
    [SerializeField] private TMP_Text timerField;

    [Space(10)]

    [Tooltip("Animates the timer when it is <= this value")]
    [SerializeField] private int pulseTimerAt = 5;

    private Animator animator;
    private const string ANIMATION_PULSE = "Pulse";

    int lastTimeDisplayed = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Events.OnScoreChanged += Events_OnScoreChanged;
        Events.OnTimerChanged += Events_OnTimerChanged;
    }

    private void Events_OnScoreChanged(int score)
    {
        scoreField.text = score.ToString();
    }

    private void Events_OnTimerChanged(float time)
    {
        int timeAsInt = Mathf.CeilToInt(time);
        if (timeAsInt != lastTimeDisplayed )
        {
            lastTimeDisplayed = timeAsInt;
            SetTimerText(timeAsInt);
            AnimateTimer(timeAsInt);
        }
    }

    private void SetTimerText(int time)
    {
        string formattedTime = time.ToString().PadLeft(2, '0');
        timerField.text = formattedTime;
    }

    private void AnimateTimer(int time)
    {
        if (time <= pulseTimerAt)
        {
            animator.SetTrigger(ANIMATION_PULSE);
        }
    }
}
