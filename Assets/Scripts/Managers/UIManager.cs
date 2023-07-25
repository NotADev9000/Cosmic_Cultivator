using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreField;

    private void Awake()
    {
        Events.OnScoreChanged += Events_OnScoreChanged;
    }

    private void Events_OnScoreChanged(int score)
    {
        scoreField.text = score.ToString();
    }
}
