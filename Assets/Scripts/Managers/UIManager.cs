using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
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
    [SerializeField] private PlayableDirector gameOverTimeline;
    [SerializeField] private float waitForBeforePlaying = 2f;

    [Space(5)]

    [SerializeField] private TMP_Text gameOverScoreField;
    [SerializeField] private string gameOverScoreText = "Your Score: ";
    [SerializeField] private TMP_Text gameOverHighscoreField;
    //[SerializeField] private string gameOverHighscoreText = "Highscore: ";

    private string displayScore = "0";

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
        SetGameoverFields();
        StartCoroutine(PlayGameOverCutscene());
    }

    private void Events_OnScoreChanged(int score)
    {
        displayScore = score.ToString();
        SetScoreText(displayScore);
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
    #region Score

    private void SetScoreText(string score)
    {
        scoreField.text = score;
    }

    #endregion
    //--------------------

    //--------------------
    #region GameOver

    private void SetGameoverFields()
    {
        gameOverScoreField.text = gameOverScoreText + displayScore;
    }

    private IEnumerator PlayGameOverCutscene()
    {
        yield return new WaitForSeconds(waitForBeforePlaying);
        Events.StartingGameOverCutscene();
        gameOverTimeline.Play();
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
