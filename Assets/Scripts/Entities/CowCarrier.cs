using System;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CowCarrier : MonoBehaviour
{
    public CardinalDirection MoveDirection
    {
        get { return moveDirection; }
        set { SetMovementDirection(value); }
    }
    [Header("Movement")]
    [SerializeField] private CardinalDirection moveDirection;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private int moveSpeed = 5;
    private Vector3 moveDirectionVector;

    [Header("Carts")]
    [Tooltip("Size of the gap between Carrier and Cart")]
    [SerializeField] private float cartOffset = 1.2f;
    [Tooltip("How far last cart should be offscreen to be considered at 'end of path'")]
    [SerializeField] private float cartOffscreenThreshold = 0.1f;
    [SerializeField] private Cart cartPrefab;
    private Cart[] childCarts = Array.Empty<Cart>();

    [Header("Tractor")]
    [SerializeField] private SpriteRenderer tractorSprite;
    [SerializeField] private Transform particlePosition_Right;
    [SerializeField] private Transform particlePosition_Left;
    [SerializeField] private GameObject particleSmoke;

    private void OnEnable()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(cartPrefab, transform);
        }
        childCarts = GetComponentsInChildren<Cart>();
        MoveDirection = Camera.main.WorldToViewportPoint(transform.position).x < 0 ? CardinalDirection.Right : CardinalDirection.Left;
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
        {
            transform.position += moveSpeed * Time.deltaTime * moveDirectionVector;
            CheckPathProgress();
        }
    }

    private void SetMovementDirection(CardinalDirection value)
    {
        moveDirection = value;
        switch (value)
        {
            case CardinalDirection.Left:
                moveDirectionVector = Vector3.left;
                tractorSprite.flipX = false;
                //if (EditorApplication.isPlaying)
                //{
                    particleSmoke.transform.parent = particlePosition_Left;
                    particleSmoke.transform.localPosition = Vector3.zero;

                    Quaternion targetRotation = Quaternion.Euler(0f, 180f, 0f);
                    particleSmoke.transform.rotation = targetRotation;
                //}
                break;
            case CardinalDirection.Right:
                moveDirectionVector = Vector3.right;
                tractorSprite.flipX = true;
                //if (EditorApplication.isPlaying)
                //{
                    particleSmoke.transform.parent = particlePosition_Right;
                    particleSmoke.transform.localPosition = Vector3.zero;
                    particleSmoke.transform.rotation = Quaternion.identity;
                //}
                break;
            default:
                moveDirectionVector = Vector3.zero;
                break;
        }

        ResetChildCartPositions();
    }

    //--------------------
    #region Cart Handling

    private void ResetChildCartPositions()
    {
        if (childCarts != null)
        {
            for (int i = 0; i < childCarts.Length; i++)
            {
                Cart cart = childCarts[i];
                float cartSize = moveDirection == CardinalDirection.Up || moveDirection == CardinalDirection.Down ? cart.CartHeight: cart.CartWidth;
                float offset = cartOffset + (cartSize * i);
                childCarts[i].transform.localPosition = moveDirectionVector * offset * -1;
            }
        }
    }

    private void RemoveChildCarts()
    {
        foreach (Cart cart in childCarts)
        {
            Destroy(cart.gameObject);
        }
        childCarts = Array.Empty<Cart>();
    }

    private Cart GetLastChildCart()
    {
        return childCarts[childCarts.Length - 1];
    }

    #endregion
    //--------------------

    //--------------------
    #region Path Handling

    private void CheckPathProgress()
    {
        if (HasReachedEndOfPath())
        {
            OnEndOfPath();
        }
    }

    private bool HasReachedEndOfPath()
    {
        Cart lastChildCart = GetLastChildCart();
        float xPosInViewport = Camera.main.WorldToViewportPoint(lastChildCart.transform.position).x;
        // entirety of object is offscreen if last trailing cart X position (in the viewport) is > 1 or < 0
        return moveDirection == CardinalDirection.Right ? xPosInViewport > (1 + cartOffscreenThreshold) : xPosInViewport < (0 - cartOffscreenThreshold);
    }

    private void OnEndOfPath()
    {
        RemoveChildCarts();
        SpawnManager.Instance.ReturnObjectToPool(gameObject);
        gameObject.SetActive(false);
    }

    #endregion
    //--------------------

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (!EditorApplication.isPlaying)
        {
            childCarts = GetComponentsInChildren<Cart>();
            MoveDirection = moveDirection;
        }
    }

#endif
}
