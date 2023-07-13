using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    private PlayerActions playerActions;

    [Header("Movement")]
    [SerializeField] private int moveSpeed = 10;

    [Header("Laser")]
    [SerializeField] private float laserRadius = 3f;
    public event Action<Collider2D> OnLaserHitAction;

    private void Awake()
    {
        CreateSingleton();
        SetupInputActions();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void LateUpdate()
    {
        ClampToScreen();
    }

    //--------------------
    #region Movement

    private void MovePlayer()
    {
        Vector2 moveDirection = GetMovementVectorNormalized();
        float moveDistance = moveSpeed * Time.deltaTime;

        transform.position += (Vector3)moveDirection * moveDistance;
    }

    private void ClampToScreen()
    {
        // get boundaries of screen in world co-ordinates
        Vector2 leftAndBottomPoint = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 rightAndTopPoint = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        // clamp transform to the boundaries
        Vector2 clampedPosition;
        clampedPosition.x = Mathf.Clamp(transform.position.x, leftAndBottomPoint.x, rightAndTopPoint.x);
        clampedPosition.y = Mathf.Clamp(transform.position.y, leftAndBottomPoint.y, rightAndTopPoint.y);
        transform.position = clampedPosition;
    }

    #endregion
    //--------------------

    //--------------------
    #region Controls

    private void SetupInputActions()
    {
        playerActions = new PlayerActions();
        playerActions.Player_Map.Enable();
        playerActions.Player_Map.Laser.performed += Laser_performed;
    }

    private Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerActions.Player_Map.Movement.ReadValue<Vector2>();
        inputVector.Normalize();
        return inputVector;
    }

    private void Laser_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        print("FIRED LASER");
        Collider2D hitCollider = Physics2D.OverlapCircle(transform.position, laserRadius);
        OnLaserHitAction?.Invoke(hitCollider);
    }

    #endregion
    //--------------------

    private void CreateSingleton()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of Player");
        }
        Instance = this;
    }

    #if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, laserRadius);
    }

    #endif
}
