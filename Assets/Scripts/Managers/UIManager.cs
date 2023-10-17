using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] private TMP_Text scoreField;

    //------------------------------------------------------------------
    [Space(10)]

    [Header("Timer")]
    [SerializeField] private TMP_Text[] timerFields;
    [SerializeField] private Animator[] timerAnimators;

    [Space(5)]

    [Tooltip("Before game start, animates the timer when it is <= this value")]
    [SerializeField] private int timerStartingPulse = 5;
    [Tooltip("During the game countdown, animates the timer when it is <= this value")]
    [SerializeField] private int timerGamePulse = 10;

    [Space(5)]

    [SerializeField] private string introTimerFinishedText = "Go!";

    //------------------------------------------------------------------
    [Space(10)]

    [Header("Pause")]
    [SerializeField] private Image pauseOverlay;
    [SerializeField] private TMP_Text pauseField;

    [Space(5)]

    [SerializeField] private float overlayAlphaOnPlay = 0f;
    [SerializeField] private float overlayAlphaOnPause = 0.5f;

    //------------------------------------------------------------------
    [Space(10)]

    [Header("Game Over")]
    [SerializeField] private TMP_Text gameOverField;

    //------------------------------------------------------------------
    private const string ANIMATION_TRIGGER_PULSE = "Pulse";
    private const string ANIMATION_TRIGGER_PULSEFADE = "PulseFade";
    private const int TIMER_INDEX_INTRO = 0;
    private const int TIMER_INDEX_MAIN = 1;

    private int timerIndex = TIMER_INDEX_INTRO;
    private int pulseTimerAt = 0;
    private int lastTimeDisplayed = 0;

    private void Awake()
    {
        AddEvents();
        pulseTimerAt = timerStartingPulse;
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    //--------------------
    #region Set Events

    private void AddEvents()
    {
        Events.OnGameStart += Events_OnGameStart;
        Events.OnScoreChanged += Events_OnScoreChanged;
        Events.OnTimerChanged += Events_OnTimerChanged;
        Events.OnGameEnd += Events_OnGameEnd;
        Events.OnGamePaused += Events_OnGamePaused;
        Events.OnGameUnpaused += Events_OnGameUnpaused;
    }

    private void RemoveEvents()
    {
        Events.OnGameStart -= Events_OnGameStart;
        Events.OnScoreChanged -= Events_OnScoreChanged;
        Events.OnTimerChanged -= Events_OnTimerChanged;
        Events.OnGameEnd -= Events_OnGameEnd;
        Events.OnGamePaused -= Events_OnGamePaused;
        Events.OnGameUnpaused -= Events_OnGameUnpaused;
    }

    #endregion
    //--------------------

    //--------------------
    #region Events Activation

    private void Events_OnGameStart(object sender, System.EventArgs e)
    {
        pulseTimerAt = timerGamePulse;
        timerIndex = TIMER_INDEX_MAIN;
    }

    private void Events_OnGameEnd(object sender, System.EventArgs e)
    {
        gameOverField.gameObject.SetActive(true);
        ChangePauseOverlayAlpha(overlayAlphaOnPause);
    }

    private void Events_OnScoreChanged(int score)
    {
        SetScoreText(score);
    }

    private void Events_OnTimerChanged(float time)
    {
        int timeAsInt = Mathf.CeilToInt(time);
        if (timeAsInt != lastTimeDisplayed)
        {
            lastTimeDisplayed = timeAsInt;
            UpdateActiveTimer(timeAsInt);
        }
    }

    private void Events_OnGamePaused(object sender, System.EventArgs e)
    {
        ChangePauseOverlayAlpha(overlayAlphaOnPause);
        pauseField.gameObject.SetActive(true);
    }

    private void Events_OnGameUnpaused(object sender, System.EventArgs e)
    {
        ChangePauseOverlayAlpha(overlayAlphaOnPlay);
        pauseField.gameObject.SetActive(false);
    }

    //--------------------
    #region Timers

    private void UpdateActiveTimer(int time)
    {
        bool mainTimerIsActive = timerIndex == TIMER_INDEX_MAIN;

        SetTimerText(time.ToString());
        if (mainTimerIsActive) PadTimerText();
        if (time <= pulseTimerAt)
        {
            string trigger;
            if (!mainTimerIsActive && time <= 0)
            {
                trigger = ANIMATION_TRIGGER_PULSEFADE;
                SetTimerText(introTimerFinishedText);
            }
            else
            {
                trigger = ANIMATION_TRIGGER_PULSE;
            }

            TriggerAnimation_Timer(trigger);
        }
    }

    #endregion
    //--------------------

    #endregion
    //--------------------

    //--------------------
    #region Text

    private void SetScoreText(int score)
    {
        scoreField.text = score.ToString();
    }

    private void SetTimerText(string text)
    {
        timerFields[timerIndex].text = text;
    }

    private void PadTimerText()
    {
        timerFields[timerIndex].text = timerFields[timerIndex].text.PadLeft(2, '0');
    }

    #endregion
    //--------------------

    //--------------------
    #region Animating

    private void TriggerAnimation_Timer(string triggerName)
    {
        timerAnimators[timerIndex].SetTrigger(triggerName);
    }

    #endregion
    //--------------------

    //--------------------
    #region Images

    private void ChangePauseOverlayAlpha(float alpha)
    {
        pauseOverlay.color = GetAlphaAsColor(alpha);
    }

    #endregion
    //--------------------

    //--------------------
    #region Misc.

    private Color GetAlphaAsColor(float value)
    {
        return new Color(pauseOverlay.color.r, pauseOverlay.color.g, pauseOverlay.color.b, value);
    }

    #endregion
    //--------------------
}
