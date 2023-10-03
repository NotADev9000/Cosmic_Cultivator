using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsManager : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Events.OnStartTransition += Events_OnStartTransition;
    }

    private void Events_OnStartTransition(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Start");
    }

    private void TransitionEnded()
    {
        Events.TransitionEnded();
    }
}
