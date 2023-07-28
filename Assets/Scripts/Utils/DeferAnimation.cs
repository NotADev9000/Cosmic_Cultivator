using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeferAnimation : MonoBehaviour
{
    [SerializeField] private string animationToPlay = "";
    private Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play(animationToPlay, 0, Random.value);
    }
}
