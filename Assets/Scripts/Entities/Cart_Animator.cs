using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart_Animator : MonoBehaviour
{
    [SerializeField] private GameObject cowChild;

    private Animator animator;

    private const string ANIMATION_TRIGGER_COW_APPEAR = "Cow_Appear";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnLaserHit()
    {
        cowChild.SetActive(true);
        animator.SetTrigger(ANIMATION_TRIGGER_COW_APPEAR);
    }
}
