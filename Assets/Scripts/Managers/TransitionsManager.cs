using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionsManager : MonoBehaviour
{
    [SerializeField] float delayAmount = 0.2f; // delay transition before activating

    //------------------------------------------------------------------

    private Animator animator;

    //------------------------------------------------------------------

    private void Awake()
    {
        animator = GetComponent<Animator>();
        AddEvents();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    //--------------------
    #region Set Events

    private void AddEvents()
    {
        Events.OnStartTransition += Events_OnStartTransition;
        Events.OnEndTransition += Events_OnEndTransition;
    }

    private void RemoveEvents()
    {
        Events.OnStartTransition -= Events_OnStartTransition;
        Events.OnEndTransition -= Events_OnEndTransition;
    }

    #endregion
    //--------------------

    //--------------------
    #region Events Activation

    private void Events_OnStartTransition(bool delayTransition)
    {
        StartCoroutine(ActivateTransition("Start", delayTransition));
    }

    private void Events_OnEndTransition(bool delayTransition)
    {
        StartCoroutine(ActivateTransition("End", delayTransition));
    }

    #endregion
    //--------------------

    //--------------------
    #region Transition Behaviour

    private IEnumerator ActivateTransition(string transitionType, bool delayTransition)
    {
        if (delayTransition) yield return new WaitForSeconds(delayAmount);
        animator.SetTrigger(transitionType);
    }

    private void TransitionFinished()
    {
        Events.TransitionFinished();
    }

    #endregion
    //--------------------

}
