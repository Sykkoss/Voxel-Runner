using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missions : MonoBehaviour
{
  private Mission randomMission;
  public GameObject missionPrefab;

  public string[] goalMission;
    // Start is called before the first frame update
    void Start()
    {
      GenerateMission();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void GenerateMission()
    {
      // randomMission = Mission.getAllMissions();
      // Mission.getAllMissions();

    }
}
