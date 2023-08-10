using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart_Animator : MonoBehaviour
{
    [SerializeField] private GameObject cowChild;

    private Animator animator;

    private const string ANIMATION_NAME_WAGON_IDLE = "Idle";
    private const int ANIMATION_LAYER_ID_WAGON = 1;
    private const string ANIMATION_TRIGGER_COW_APPEAR = "Cow_Appear";
    private const string ANIMATION_BOOL_COW_MIRROR = "Mirror_Idle";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetupCartIdleAnimation();
        SetupCowIdleAnimation();
    }

    private void SetupCartIdleAnimation()
    {
        animator.Play(ANIMATION_NAME_WAGON_IDLE, ANIMATION_LAYER_ID_WAGON, Random.value);
    }

    private void SetupCowIdleAnimation()
    {
        bool setBool = Random.value > 0.5f;
        animator.SetBool(ANIMATION_BOOL_COW_MIRROR, setBool);
    }

    public void OnLaserHit()
    {
        cowChild.SetActive(true);
        animator.SetTrigger(ANIMATION_TRIGGER_COW_APPEAR);
    }
}
