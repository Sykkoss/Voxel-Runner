using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Level", LoadSceneMode.Single);
    }

    public void BackToMain()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void ReturnBack()
    {
        SceneManager.UnloadSceneAsync(SceneManager.sceneCount);
    }

    public void DisplaySettings()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HideCanvas()
    {
        GameObject.Find("Canvas").SetActive(false);
    }
}
