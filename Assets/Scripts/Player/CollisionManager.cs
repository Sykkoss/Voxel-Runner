using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
      if (col.gameObject.tag == "Coin") {
        Destroy(col.gameObject);
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
      }
    }
}
