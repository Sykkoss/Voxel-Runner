using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreListManager : MonoBehaviour
{
    public GameObject defaultScore;

    // Start is called before the first frame update
    void Start()
    {
        var list = GetComponent<Leaderboard>().GetLeaderboard();
        DisplayList(list);
    }

    private void DisplayList(List<Tuple<int, string>> list)
    {
        int yShift = -70;
        var position = new Vector2(0, -50);
        for (int i = 0; i < 10; i++)
        {
            var tmp = Instantiate(defaultScore, transform);
            position.y += yShift;
            if (i < list.Count)
                tmp.GetComponentInChildren<Text>().text = "" + list[i].Item1;
            tmp.transform.localPosition = position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
