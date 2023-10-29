using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsManager : MonoBehaviour
{
    [SerializeField] float delayAmount = 0.2f;

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

    private void Events_OnStartTransition(bool delayTransition)
    {
        StartCoroutine(ActivateTransition("Start", delayTransition));
    }

    private void Events_OnEndTransition(bool delayTransition)
    {
        StartCoroutine(ActivateTransition("End", delayTransition));
    }

    private IEnumerator ActivateTransition(string transitionType, bool delayTransition)
    {
        if (delayTransition) yield return new WaitForSeconds(delayAmount);
        animator.SetTrigger(transitionType);
    }

    private void TransitionFinished()
    {
        Events.TransitionFinished();
    }
}
