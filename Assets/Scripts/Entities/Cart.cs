using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cartVisual;
    [SerializeField] private float cartGap = 0.2f;

    private Collider2D boxCollider;
    public float CartWidth { get { return cartVisual.bounds.size.x + cartGap; } }
    public float CartHeight { get { return cartVisual.bounds.size.y + cartGap; } }

    private void Awake()
    {
        boxCollider = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        Player.Instance.OnLaserHitAction += Player_OnLaserHitCheck;
    }

    private void OnDisable()
    {
        RemoveLaserHitListener();
    }

    private void RemoveLaserHitListener()
    {
        Player.Instance.OnLaserHitAction -= Player_OnLaserHitCheck;
    }

    private void Player_OnLaserHitCheck(Collider2D laserCollider)
    {
        if (laserCollider == boxCollider)
        {
            cartVisual.color = Color.magenta;
            boxCollider.enabled = false;
            RemoveLaserHitListener();
            Events.CartHit();
        }
    }
}
