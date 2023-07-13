using System;
using UnityEngine;

public class CowCarrier : MonoBehaviour
{
    public CardinalDirection MoveDirection
    {
        get { return moveDirection; }
        set
        {
            moveDirection = value;
            switch (value)
            {
                case CardinalDirection.Up:
                    moveDirectionVector = Vector3.up;
                    break;
                case CardinalDirection.Down:
                    moveDirectionVector = Vector3.down;
                    break;
                case CardinalDirection.Left:
                    moveDirectionVector = Vector3.left;
                    break;
                case CardinalDirection.Right:
                    moveDirectionVector = Vector3.right;
                    break;
                default:
                    moveDirectionVector = Vector3.zero;
                    break;
            }
            ResetChildCartPositions();
        }
    }
    [SerializeField] private CardinalDirection moveDirection;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private int moveSpeed = 5;

    [SerializeField] private float cartOffset = 1.2f;
    [SerializeField] private float cartOffscreenThreshold = 0.1f;
    [SerializeField] private Cart cartPrefab;

    private Cart[] childCarts = Array.Empty<Cart>();
    private Vector3 moveDirectionVector;

    private void OnEnable()
    {
        print(childCarts.Length);
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
            print(Camera.main.WorldToViewportPoint(GetLastChildCart().transform.position).x);
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
        MoveDirection = moveDirection;
    }

#endif
}
