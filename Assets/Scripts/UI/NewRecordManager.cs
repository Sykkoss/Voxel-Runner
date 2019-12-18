using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewRecordManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int lastScore = PlayerPrefs.GetInt("LastScore");
        GetComponent<Text>().text = "" + lastScore;
        if (!GetComponent<Leaderboard>().AddScore(lastScore, "na"))
            transform.GetChild(0).gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
