using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TMP_Text scoreField;

    [Space(10)]

    [Header("Timer")]
    [SerializeField] private TMP_Text timerField;
    [SerializeField] private Animator timerAnimator;

    [Space(5)]

    [Tooltip("Animates the timer when it is <= this value")]
    [SerializeField] private int pulseTimerAt = 5;

    [Space(10)]

    [Header("Game Over")]
    [SerializeField] private TMP_Text gameOverField;

    private const string ANIMATION_TRIGGER_PULSE = "Pulse";

    int lastTimeDisplayed = 0;

    private void Awake()
    {
        Events.OnScoreChanged += Events_OnScoreChanged;
        Events.OnTimerChanged += Events_OnTimerChanged;
        Events.OnTimerEnd += Events_OnTimerEnd;
    }

    private void OnDestroy()
    {
        Events.OnScoreChanged -= Events_OnScoreChanged;
        Events.OnTimerChanged -= Events_OnTimerChanged;
        Events.OnTimerEnd -= Events_OnTimerEnd;
    }

    private void Events_OnScoreChanged(int score)
    {
        SetScoreText(score);
    }

    private void SetScoreText(int score)
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
            timerAnimator.SetTrigger(ANIMATION_TRIGGER_PULSE);
        }
    }

    private void Events_OnTimerEnd(object sender, System.EventArgs e)
    {
        gameOverField.gameObject.SetActive(true);
    }
}
