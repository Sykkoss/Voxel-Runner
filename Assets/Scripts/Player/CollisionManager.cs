using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
      int coins;
      if (col.gameObject.name == "Coin"){
        col.gameObject.SetActive(false);
        coins = PlayerPrefs.GetInt("Coins");
        PlayerPrefs.SetInt("Coins", coins + 1);
      }
    }
}
