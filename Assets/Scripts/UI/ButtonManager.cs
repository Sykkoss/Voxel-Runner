using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("Gameplay", LoadSceneMode.Single);
    }

    public void BackToMain()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }

    public void OutFromSettings()
    {
        SceneManager.UnloadSceneAsync("Settings");
    }

    public void OutFromPause()
    {
        SceneManager.UnloadSceneAsync("Pause");
    }

    public void DisplayLeaderboard()
    {
        SceneManager.LoadSceneAsync("Leaderboard", LoadSceneMode.Single);
    }

    public void DisplaySettings()
    {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }

    public void DisplayPause()
    {
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
    }

    public void OpenShop()
    {
        SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Single);
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
        int account = PlayerPrefs.GetInt("Coins");
        if (account < 100)
            return;
        var tmp = transform.parent.parent.GetChild(1).gameObject;
        var tmpName = tmp.name.Replace("(Clone)", "");
        PlayerPrefs.SetInt(tmpName, 1);
        transform.parent.parent.parent.GetComponent<ShopManager>().ResetFocus();
        PlayerPrefs.SetInt("Coins", account - 100);
    }
}
