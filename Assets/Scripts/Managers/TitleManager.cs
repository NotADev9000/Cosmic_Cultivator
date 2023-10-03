using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    public void PlayPressed()
    {
        Events.OnTransitionEnded += Events_OnTransitionEnded;
        Events.StartTransition();
    }

    private void Events_OnTransitionEnded(object sender, System.EventArgs e)
    {
        Events.OnTransitionEnded -= Events_OnTransitionEnded;
        SceneManager.LoadScene(1);
    }
}
