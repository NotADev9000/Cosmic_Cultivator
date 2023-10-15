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
        Events.OnEndTransition += Events_OnEndTransition;
    }

    private void OnDestroy()
    {
        Events.OnStartTransition -= Events_OnStartTransition;
        Events.OnEndTransition -= Events_OnEndTransition;
    }

    private void Events_OnStartTransition(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Start");
    }

    private void Events_OnEndTransition(object sender, System.EventArgs e)
    {
        print("end transition");
        animator.SetTrigger("End");
    }

    private void TransitionEnded()
    {
        Events.TransitionEnded();
    }
}
