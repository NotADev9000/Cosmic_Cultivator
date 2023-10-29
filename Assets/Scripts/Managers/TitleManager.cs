using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private bool HasPressedPlay = false;

    public void PlayPressed()
    {
        if (!HasPressedPlay)
        {
            Events.MenuButtonPressed();
            Events.OnTransitionFinished += Events_OnTransitionFinished;
            Events.StartTransition(true);
            Events.FadeOutBgm();
            HasPressedPlay = true;
        }
    }

    private void Events_OnTransitionFinished(object sender, System.EventArgs e)
    {
        Events.OnTransitionFinished -= Events_OnTransitionFinished;
        SceneManager.LoadScene(1);
    }
}
