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
    [SerializeField] private Timer timerIntro;
    [SerializeField] private Timer timerMain;

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

    private void Awake()
    {
        AddEvents();
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
        Events.OnGameEnd += Events_OnGameEnd;
        Events.OnGamePaused += Events_OnGamePaused;
        Events.OnGameUnpaused += Events_OnGameUnpaused;
    }

    private void RemoveEvents()
    {
        Events.OnGameStart -= Events_OnGameStart;
        Events.OnScoreChanged -= Events_OnScoreChanged;
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
        timerIntro.isRunning = false;
        timerMain.isRunning = true;
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

    #endregion
    //--------------------

    //--------------------
    #region Text

    private void SetScoreText(int score)
    {
        scoreField.text = score.ToString();
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
