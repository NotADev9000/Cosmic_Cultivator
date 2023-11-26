using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart_Audio : MonoBehaviour
{
    [Header("Cow")]
    [SerializeField] private List<AudioClip> cowClipsList = new List<AudioClip>();

    //------------------------------------------------------------------

    private AudioSource audioSource;

    //------------------------------------------------------------------

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    //--------------------
    #region Events Activation

    public void OnCowAppear()
    {
        int clipIndex = Random.Range(0, cowClipsList.Count);
        AudioClip clipToPlay = cowClipsList[clipIndex];
        PlayAudioOneShot(clipToPlay);
    }

    #endregion
    //--------------------

    //--------------------
    #region Audio

    public void PlayAudioOneShot(AudioClip clipToPlay)
    {
        audioSource.PlayOneShot(clipToPlay);
    }

    #endregion
    //--------------------

}
