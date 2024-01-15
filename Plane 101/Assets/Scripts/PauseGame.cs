using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
    }
    public void GamePause()
    {
        Time.timeScale = 0.0f;
        AudioListener.pause = true;
    }

    public void GameResume()
    {
        Time.timeScale = 1.0f;
        AudioListener.pause = false;
    }
}
