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

    public void DisplayLeaderboard()
    {
        SceneManager.LoadScene("Leaderboard", LoadSceneMode.Single);
    }

    public void DisplaySettings()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public void OpenShop()
    {
        SceneManager.LoadScene("Shop", LoadSceneMode.Single);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void HideCanvas()
    {
        GameObject.Find("Canvas").SetActive(false);
    }

    public void ShopEquip()
    {
        var tmp = transform.parent.parent.GetChild(1).gameObject;
        var tmpName = tmp.name.Replace("(Clone)", "");
        PlayerPrefs.SetString("ActiveSkin", tmpName);
    }

    public void ShopBuy()
    {
        // TODO : Remove 1000 pieces
        var tmp = transform.parent.parent.GetChild(1).gameObject;
        var tmpName = tmp.name.Replace("(Clone)", "");
        PlayerPrefs.SetInt(tmpName, 1);
        transform.parent.parent.parent.GetComponent<ShopManager>().ResetFocus();
    }
}
