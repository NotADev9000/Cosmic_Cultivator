using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart_Audio : MonoBehaviour
{
    [Header("Cow")]
    [SerializeField] private List<AudioClip> cowClipsList = new List<AudioClip>();

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnCowAppear()
    {
        audioSource.pitch = Random.Range(0.75f, 1.55f);

        int clipIndex = Random.Range(0, cowClipsList.Count);
        AudioClip clipToPlay = cowClipsList[clipIndex];

        audioSource.PlayOneShot(clipToPlay);

        audioSource.pitch = 1;
    }

    public void PlayAudioOneShot(AudioClip clipToPlay)
    {
        audioSource.PlayOneShot(clipToPlay);
    }
}
