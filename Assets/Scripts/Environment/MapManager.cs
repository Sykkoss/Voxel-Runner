using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Biome
{
    public GameObject[] groundParts; /* Number 0 must be the empty basic part ! */
    public GameObject[] sideParts; /* Number 0 must be the empty basic part ! */
}

public class MapManager : MonoBehaviour
{


    #region Arguments-Public

    #region Arguments-TweakNumbers

    public int maxParts;
    public float partSize;
    public int numberStartParts;

    #endregion Arguments-TweakNumbers

    public Biome[] biomes;

    #endregion Arguments-Public

    private List<GameObject> instanciatedGround;
    private List<GameObject> instanciatedSide;
    private int lastRandomGround;
    private int lastRandomSide;

    #region Monobehaviour
    private void Start()
    {
        instanciatedGround = new List<GameObject>(maxParts);
        instanciatedSide = new List<GameObject>(maxParts);
        lastRandomGround = 0;
        lastRandomSide = 0;
        if (groundParts.Length > 0 && sideParts.Length > 0)
            GenerateBeginning();
    }

    private void Update()
    {
        if (groundParts.Length > 0 && sideParts.Length > 0 &&
            instanciatedGround.Count < maxParts)
            GenerateNewPart();
    }
    #endregion Monobehaviour

    #region PartCreation

    // Create beginning parts. Create 'numberStartParts' parts only with the first 'groundParts' part (empty tile)
    private void GenerateBeginning()
    {
        GameObject newGround;
        GameObject newSide;
        Vector3 spawnPosition;

        while (instanciatedGround.Count < numberStartParts)
        {
            spawnPosition = GetSpawnPosition();
            newGround = Instantiate(biomes[0].groundParts[0], spawnPosition, Quaternion.identity, transform) as GameObject;
            newSide = Instantiate(biomes[0].sideParts[0], spawnPosition, Quaternion.identity, transform) as GameObject;
            instanciatedGround.Add(newGround);
            instanciatedSide.Add(newSide);
        }
    }

    // Create a new part and add it to the 'instanciatedGround' list
    private void GenerateNewPart()
    {
        GameObject newGround;
        GameObject newSide;
        Vector3 spawnPosition = GetSpawnPosition();

        newGround = Instantiate(GetRandomGround(), spawnPosition, Quaternion.identity, transform) as GameObject;
        newSide = Instantiate(GetRandomSide(), spawnPosition, Quaternion.identity, transform) as GameObject;
        instanciatedGround.Add(newGround);
        instanciatedSide.Add(newSide);
    }

    // Get a random part from the array 'groundParts'. Does not allow to have the same tile side to side
    private GameObject GetRandomGround()
    {
        int randomNumber = lastRandomGround;

        while (randomNumber == lastRandomGround)
            randomNumber = Random.Range(0, biomes[0].groundParts.Length);
        lastRandomGround = randomNumber;
        return biomes[0].groundParts[randomNumber];
    }

    // Get a random part from the array 'sideParts'. Does not allow to have the same tile side to side
    private GameObject GetRandomSide()
    {
        int randomNumber = lastRandomSide;

        while (randomNumber == lastRandomSide)
            randomNumber = Random.Range(0, biomes[0].sideParts.Length);
        lastRandomSide = randomNumber;
        return biomes[0].sideParts[randomNumber];
    }

    // Get spawn position for new part taking 'partSize' and the number of instanciated parts
    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPos = Vector3.zero;

        if (instanciatedGround.Count > 0)
            spawnPos.z = partSize + instanciatedGround[instanciatedGround.Count - 1].transform.position.z;
        return spawnPos;
    }

    #endregion PartCreation

    public void DeleteFirstPart()
    {
        Destroy(instanciatedGround[0]); // Destroy ground
        instanciatedGround.RemoveAt(0);
        Destroy(instanciatedSide[0]); // Destroy side
        instanciatedSide.RemoveAt(0);
    }

    public float GetPartSize()
    {
        return partSize;
    }
}
