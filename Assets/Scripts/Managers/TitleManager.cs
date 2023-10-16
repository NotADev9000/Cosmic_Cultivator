using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void PlayPressed()
    {
        Events.OnTransitionFinished += Events_OnTransitionFinished;
        Events.StartTransition();
    }

    private void Events_OnTransitionFinished(object sender, System.EventArgs e)
    {
        Events.OnTransitionFinished -= Events_OnTransitionFinished;
        SceneManager.LoadScene(1);
    }
}
