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

    public void LoadDeath()
    {
        PlayerPrefs.SetInt("Account",
            PlayerPrefs.GetInt("Account") + PlayerPrefs.GetInt("Coins"));
        PlayerPrefs.SetInt("Coins", 0);
        GameObject.Find("LevelCanvas").SetActive(false);
        SceneManager.LoadScene("GameOverMenu", LoadSceneMode.Additive);
    }

    void LoadPause()
    {
        isPaused = true;
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }
}
