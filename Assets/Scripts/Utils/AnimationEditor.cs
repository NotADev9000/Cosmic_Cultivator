using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEditor : MonoBehaviour
{
    [Tooltip("If set, will play the animation with a normalized time on Awake")]
    [SerializeField] private string animationToPlay;
    [Tooltip("If set, will set the animation parameter randomly (50/50)")]
    [SerializeField] private string boolParameterName;

    private Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();

        if (boolParameterName != string.Empty)
        {
            bool setBool = Random.Range(0, 2) == 1;
            animator.SetBool(boolParameterName, setBool);
        }

        if (animationToPlay != string.Empty)
        {
            animator.Play(animationToPlay, 0, Random.value);
        }
    }
}
