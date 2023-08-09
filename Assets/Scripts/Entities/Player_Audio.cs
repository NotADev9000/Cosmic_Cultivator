using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Audio : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private List<AudioClip> laserClipsList = new List<AudioClip>();

    private AudioSource audioSource;
    private Queue<AudioClip> laserClipsQueue;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        laserClipsQueue = new Queue<AudioClip>(laserClipsList);
    }

    private void Start()
    {
        Player.Instance.OnLaserShotAction += Player_OnLaserShotAction;
    }

    private void Player_OnLaserShotAction(Collider2D obj)
    {
        if (laserClipsQueue.Count > 0)
        {
            audioSource.pitch = Random.Range(0.75f, 1.3f);

            AudioClip clipToPlay = laserClipsQueue.Dequeue();

            audioSource.PlayOneShot(clipToPlay);

            laserClipsQueue.Enqueue(clipToPlay);
        }
    }
}
