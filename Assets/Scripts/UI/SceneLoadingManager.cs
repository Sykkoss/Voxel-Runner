using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    private bool isPaused;

    private void Start()
    {
        isPaused = false;
        SceneManager.sceneUnloaded += ChangePauseState;
    }

    private void ChangePauseState(Scene arg0)
    {
        isPaused = !isPaused;
    }

    // Start is called before the first frame update
    void Update()
    {
        if (!isPaused && Input.GetKeyDown(KeyCode.P))
            LoadPause();
    }

    void LoadDeath()
    {
        GameObject.Find("LevelCanvas").SetActive(false);
        SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
    }

    void LoadPause()
    {
        isPaused = true;
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }
}
