using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private SpriteRenderer cartVisual;
    [SerializeField] private float cartGap = 0.2f;
    [SerializeField] private GameObject cowVisual;

    [Space(10)]

    [Header("Audio")]
    [SerializeField] private Cart_Audio cartAudio;

    private Collider2D boxCollider;
    public float CartWidth { get { return cartVisual.bounds.size.x + cartGap; } }
    public float CartHeight { get { return cartVisual.bounds.size.y + cartGap; } }

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

    private void RemoveLaserHitListener()
    {
        Player.Instance.OnLaserShotAction -= Player_OnLaserShotCheck;
    }

    private void Player_OnLaserShotCheck(Collider2D laserCollider)
    {
        if (laserCollider == boxCollider)
        {
            Events.CartHit();
            cowVisual.SetActive(true);
            cartAudio.OnCowAppear();
            boxCollider.enabled = false;
            RemoveLaserHitListener();
        }
    }
}
