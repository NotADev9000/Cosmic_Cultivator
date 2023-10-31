using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private Cart_Animator cartAnimator;
    [SerializeField] private SpriteRenderer wagonVisual;

    [Space(5)]

    [Tooltip("Distance between each cart behind a tractor")]
    [SerializeField] private float cartGap = 0.2f;

    [Space(10)]

    [Header("Audio")]
    [SerializeField] private Cart_Audio cartAudio;

    private Collider2D boxCollider;
    public float CartWidth { get { return wagonVisual.bounds.size.x + cartGap; } }
    public float CartHeight { get { return wagonVisual.bounds.size.y + cartGap; } }

    private void Awake()
    {
        boxCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Player.Instance.OnLaserShotAction += Player_OnLaserShotCheck;
    }

    private void OnDisable()
    {
        RemoveLaserHitListener();
    }

    private void Player_OnLaserShotCheck(Collider2D laserCollider)
    {
        if (laserCollider == boxCollider)
        {
            Events.CartHit(transform);
            DoAudioAndVisuals();
            RemoveTarget();
            RemoveLaserHitListener();
        }
    }

    private void DoAudioAndVisuals()
    {
        cartAnimator.OnLaserHit();
        cartAudio.OnCowAppear();
    }

    private void RemoveTarget()
    {
        boxCollider.enabled = false;
    }

    private void RemoveLaserHitListener()
    {
        Player.Instance.OnLaserShotAction -= Player_OnLaserShotCheck;
    }
}
