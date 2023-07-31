using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animator : MonoBehaviour
{
    [Header("Alien")]
    [SerializeField] private Transform visualAlien;
    [Tooltip("How much the Player should rotate when moving left/right")]
    [SerializeField] private float rotateBy = 20f;
    [SerializeField] private float rotateSpeed = 5f;

    [Space(10)]

    [Header("Laser")]
    [SerializeField] private Transform laserSpawnPosition;
    [SerializeField] private GameObject laserPrefab;

    private void Start()
    {
        Player.Instance.OnLaserShotAction += Player_OnLaserShotAction;
    }

    private void Update()
    {
        float targetAngle = -rotateBy * Player.Instance.MoveDirection.x;

        // Calculate the target rotation as a quaternion
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle);

        // Slerp between the current rotation and the target rotation based on rotationSpeed
        visualAlien.transform.rotation = Quaternion.Slerp(visualAlien.transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    private void Player_OnLaserShotAction(Collider2D obj)
    {
        Instantiate(laserPrefab, laserSpawnPosition);
    }
}
