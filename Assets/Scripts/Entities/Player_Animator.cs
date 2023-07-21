using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    [Header("Player Main")]
    [SerializeField] private Player player;

    [Header("Movement")]
    [Tooltip("How much the Player should rotate when moving left/right")]
    [SerializeField] private float rotateBy = 20f;
    [SerializeField] private float rotateSpeed = 5f;

    private void Update()
    {
        float targetAngle = -rotateBy * Player.Instance.MoveDirection.x;

        // Calculate the target rotation as a quaternion
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        // Slerp between the current rotation and the target rotation based on rotationSpeed
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
