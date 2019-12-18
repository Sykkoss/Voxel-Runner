using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Mission
{
  public string title;
  public string reward;
  public int goalNbr;
  public string goal;

    public static Mission getRandomMission()
    {
      return null;
    }

    public static void getAllMissions()
    {
      string file= File.ReadAllText(Path.Combine("Assets/Scripts/UI/missions.json"));
      Debug.Log(file);
      Wrapper<Mission> wrapper = JsonUtility.FromJson<Wrapper<Mission>>(file);
      // Debug.Log(wrapper.Items);
      // Mission[] missions= wrapper.Items;
      Mission[] missions = JsonUtility.FromJson<Mission[]>(file);
      Debug.Log(missions[0]);
    }
    [Serializable]
    private class Wrapper<Mission> {
      public Mission[] Items;
    }
}
