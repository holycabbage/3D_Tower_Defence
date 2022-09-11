using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScene : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseSceneUI;

    public void Pause()
    {
        pauseSceneUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Resume()
    {
        pauseSceneUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}
