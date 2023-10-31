using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

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
    [SerializeField] private int defaultMoveSpeed = 2;
    [Tooltip("Move speed increase when all carts are filled")]
    [SerializeField] private int fullSpeedAddition = 3;
    //[Tooltip("Move speed when all carts are filled")]
    //[SerializeField] private int fullSpeedSet = 5;
    private Vector3 moveDirectionVector;

    [Header("Carts")]
    [Tooltip("Size of the gap between Carrier and Cart")]
    [SerializeField] private float cartOffset = 1.2f;
    [Tooltip("How far last cart should be offscreen to be considered at 'end of path'")]
    [SerializeField] private float cartOffscreenThreshold = 0.1f;
    [SerializeField] private Cart cartPrefab;
    private Cart[] childCarts = Array.Empty<Cart>();

    [Header("Tractor")]
    [SerializeField] private Transform tractorVisual;

    private int moveSpeed = 0;
    private int noOfCarts = 0; // how many carts does this tractor have
    private int noOfCows = 0; // how many cows have been given to this tractor

    private void OnEnable()
    {
        AddEvents();
        ResetCarrier();
        for (int i = 0; i < 3; i++)
        {
            Instantiate(cartPrefab, transform);
            noOfCarts++;
        }
        childCarts = GetComponentsInChildren<Cart>();
        MoveDirection = Camera.main.WorldToViewportPoint(transform.position).x < 0 ? CardinalDirection.Right : CardinalDirection.Left;
        isMoving = true;
    }

    private void OnDisable()
    {
        RemoveEvents();
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
    #region Set Events

    private void AddEvents()
    {
        Events.OnCartHit += Events_OnCartHit;
    }

    private void RemoveEvents()
    {
        Events.OnCartHit -= Events_OnCartHit;
    }

    #endregion
    //--------------------

    //--------------------
    #region Carrier Behavior

    private void ResetCarrier()
    {
        moveSpeed = defaultMoveSpeed;
        noOfCows = 0;
        noOfCarts = 0;
    }

    private void SetMovementDirection(CardinalDirection value)
    {
        moveDirection = value;
        switch (value)
        {
            case CardinalDirection.Left:
                moveDirectionVector = Vector3.left;
                Quaternion targetRotation = Quaternion.Euler(0f, 180f, 0f);
                tractorVisual.rotation = targetRotation;
                break;
            case CardinalDirection.Right:
                moveDirectionVector = Vector3.right;
                tractorVisual.rotation = Quaternion.identity;
                break;
            default:
                moveDirectionVector = Vector3.zero;
                break;
        }

        ResetChildCartPositions();
    }

    private void UpdateOnFull()
    {
        if (IsFull())
        {
            Events.CarrierFilled();
            ChangeToFullMoveSpeed();
        }
    }

    private void ChangeToFullMoveSpeed()
    {
        moveSpeed += fullSpeedAddition;
        //moveSpeed = fullSpeedSet;
    }

    #endregion
    //--------------------

    //--------------------
    #region Cart Handling

    private void Events_OnCartHit(Transform cartTransform)
    {
        if (cartTransform.IsChildOf(transform))
        {
            IncreaseNoOfCows();
            UpdateOnFull();
        }
    }

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
        //return childCarts[childCarts.Length - 1];
        return childCarts.Last();
    }

    #endregion
    //--------------------

    //--------------------
    #region Cow Handling

    private void IncreaseNoOfCows()
    {
        noOfCows++;
    }

    private bool IsFull()
    {
        return noOfCows >= noOfCarts;
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
