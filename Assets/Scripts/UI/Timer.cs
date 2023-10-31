using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer: MonoBehaviour
{
    [Tooltip("Is the timer active")]
    public bool isRunning = false;

    //----------------------------------------------------------
    [Header("Animation")]
    [Tooltip("Should the timer animate when <= a particular value")]
    [SerializeField] private bool shouldAnimate = true;
    [Tooltip("Animates the timer when it is <= this value")]
    [SerializeField] private int pulseAt = 5;

    [Space(10)]

    //----------------------------------------------------------
    [Header("Sounds")]
    [SerializeField] private bool playSoundWhenLow = true;
    [SerializeField] private bool playSoundOnZero = true;
    [Tooltip("Plays sound when timer is <= this value")]
    [SerializeField] private int soundAt = 5;

    [Space(5)]

    [SerializeField] private AudioClip lowCountClip;
    [SerializeField] private AudioClip zeroCountClip;

    [Space(10)]

    //----------------------------------------------------------
    [Header("Text")]
    [SerializeField] private TMP_Text textField;

    [Space(5)]

    [Tooltip("Should string be displayed instead of number when timer reaches zero")]
    [SerializeField] private bool displayZeroText = false;
    [Tooltip("String to display when timer reaches zero")]
    [SerializeField] private string zeroText;

    [Space(5)]

    [SerializeField] private bool colorChangeOnLow = false;
    [SerializeField] private TMP_ColorGradient colorChangeTo;
    [Tooltip("change color when timer is <= this value")]
    [SerializeField] private int changeColorAt = 5;

    [Space(5)]

    [Tooltip("minimum number of digits displayed during countdown")]
    [SerializeField] private int numberOfDigits = 1;

    //----------------------------------------------------------
    private const string ANIMATION_TRIGGER_PULSE = "Pulse";
    private const string ANIMATION_TRIGGER_PULSEFADE = "PulseFade";

    private int lastNumberDisplayed;

    private Animator animator;
    private AudioSource audioSource;

    //----------------------------------------------------------
    private void Awake()
    {
        AddEvents();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    //--------------------
    #region Set Events

    private void AddEvents()
    {
        Events.OnTimerChanged += Events_OnTimerChanged;
    }

    private void RemoveEvents()
    {
        Events.OnTimerChanged -= Events_OnTimerChanged;
    }

    #endregion
    //--------------------

    //--------------------
    #region Timer Management

    private void Events_OnTimerChanged(float time)
    {
        if (isRunning)
        {
            int displayTime = Mathf.CeilToInt(time);
            if (displayTime != lastNumberDisplayed)
            {
                lastNumberDisplayed = displayTime;
                UpdateTimer(displayTime);
            }
        }
    }

    private void UpdateTimer(int time)
    {
        SetText(time);
        PadText();
        if (time <= pulseAt && shouldAnimate) AnimateTimer(time);
        if (time <= changeColorAt && colorChangeOnLow) ChangeColor(colorChangeTo);
        UpdateAndPlaySounds(time);
    }

    #endregion
    //--------------------

    //--------------------
    #region Changing Values

    private void SetText(int time)
    {
        string text = time <= 0 && displayZeroText ? zeroText : time.ToString();
        textField.text = text;
    }

    private void PadText()
    {
        textField.text = textField.text.PadLeft(numberOfDigits, '0');
    }

    private void AnimateTimer(int time)
    {
        string animationTrigger = time <= 0 ? ANIMATION_TRIGGER_PULSEFADE : ANIMATION_TRIGGER_PULSE;
        animator.SetTrigger(animationTrigger);
    }

    private void ChangeColor(TMP_ColorGradient color)
    {
        textField.colorGradientPreset = color;
    }

    private void UpdateAndPlaySounds(int time)
    {
        if (time <= 0)
        {
            if (playSoundOnZero)
            {
                audioSource.clip = zeroCountClip;
            }
            else
            {
                return;
            }
        }
        else if (playSoundWhenLow && time <= soundAt)
        {
            audioSource.clip = lowCountClip;
        }
        else
        {
            return;
        }

        audioSource.Play();
    }

    #endregion
    //--------------------

}
