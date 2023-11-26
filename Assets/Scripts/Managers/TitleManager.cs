using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private bool HasPressedPlay = false;

    //------------------------------------------------------------------

    //--------------------
    #region UI Interaction

    public void ButtonPressed()
    {
        if (!HasPressedPlay)
        {
            Events.MenuButtonPressed();
        }
    }

    public void PlayPressed()
    {
        if (!HasPressedPlay)
        {
            Events.OnTransitionFinished += Events_OnTransitionFinished;
            Events.StartTransition(true);
            Events.FadeOutBgm();
            HasPressedPlay = true;
        }
    }

    #endregion
    //--------------------

    //--------------------
    #region Events Activation

    private void Events_OnTransitionFinished(object sender, System.EventArgs e)
    {
        Events.OnTransitionFinished -= Events_OnTransitionFinished;
        SceneManager.LoadScene(1);
    }

    #endregion
    //--------------------

}
