using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
  public GameObject coinsNbr;
    // Start is called before the first frame update
    void Start()
    {
      coinsNbr.GetComponent<Text>().text = PlayerPrefs.GetInt("Coins").ToString();
    }

    // Update is called once per frame
    void Update()
    {
      coinsNbr.GetComponent<Text>().text = PlayerPrefs.GetInt("Coins").ToString();
    }
}
