using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    Text thisText;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        thisText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score += 1;
        thisText.text = "" + (score + PlayerPrefs.GetInt("Coins") * 100);
        PlayerPrefs.SetInt("LastScore", score + PlayerPrefs.GetInt("Coins") * 100);
    }
}
