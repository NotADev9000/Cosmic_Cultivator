using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip scoreIncreaseClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Events.OnIncreaseScore += Events_OnIncreaseScore;
        Events.OnTimerEnd += Events_OnTimerEnd;
    }

    private void OnDestroy()
    {
        Events.OnIncreaseScore -= Events_OnIncreaseScore;
        Events.OnTimerEnd -= Events_OnTimerEnd;
    }

    private void Events_OnIncreaseScore(object sender, System.EventArgs e)
    {
        if (scoreIncreaseClip != null)
        {
            audioSource.PlayOneShot(scoreIncreaseClip);
        }
    }

    private void Events_OnTimerEnd(object sender, System.EventArgs e)
    {
        audioSource.Stop();
    }
}
