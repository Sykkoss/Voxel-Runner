using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Biome
{
    public GameObject[] groundParts; /* Number 0 must be the empty basic part */
    public GameObject[] sideParts; /* Number 0 must be the empty basic part */
    public GameObject[] inTransitionGround; /* Must be in order */
    public GameObject[] inTransitionSide; /* Must be in order */
    public GameObject[] outTransitionGround; /* Must be in order */
    public GameObject[] outTransitionSide; /* Must be in order */
}

public class MapManager : MonoBehaviour
{
    #region Arguments-Public

    #region Arguments-TweakNumbers

    public int maxParts;
    public float partSize;
    public int numberStartParts;
    public int numberPartsToChangeBiome;

    #endregion Arguments-TweakNumbers

    public Biome[] biomes;

    #endregion Arguments-Public

    private List<GameObject> instanciatedGround;
    private List<GameObject> instanciatedSide;
    private int lastRandomGround;
    private int lastRandomSide;
    private int numberPartsTraveled;
    private int currentBiome;


    #region Monobehaviour

    private void Start()
    {
        instanciatedGround = new List<GameObject>(maxParts);
        instanciatedSide = new List<GameObject>(maxParts);
        lastRandomGround = 0;
        lastRandomSide = 0;
        numberPartsTraveled = 0;
        currentBiome = -1;
        GenerateNextBiome(false);
    }

    // Change to next biome if player traveled enough. Then generate a new part if needed
    private void Update()
    {
        if (numberPartsTraveled >= numberPartsToChangeBiome)
        {
            numberPartsTraveled = 0;
            GenerateNextBiome(true);
        }
        if (CheckBiomes() && instanciatedGround.Count < maxParts)
            GenerateNewPart();
    }

    #endregion Monobehaviour


    #region PartCreation

    // Check if public variables are correctly filled
    private bool CheckBiomes()
    {
        foreach (Biome biome in biomes)
        {
            if (biome.groundParts.Length <= 0 || biome.sideParts.Length <= 0)
            {
                Debug.LogError("Error: Ground parts or side parts are not given for a biome in MapManager.");
                return false;
            }
        }
        if (biomes.Length > 0)
            return true;
        else
        {
            Debug.LogError("Error: No biome given in MapManager.");
            return false;
        }
    }

    // Create beginning parts. Create 'numberStartParts' parts only with the first 'groundParts' part (empty tile)
    private void GenerateBeginning()
    {
        GameObject newGround;
        GameObject newSide;
        Vector3 spawnPosition;
        int generatedStartParts = 0;

        while (generatedStartParts < numberStartParts)
        {
            spawnPosition = GetSpawnPosition();
            newGround = Instantiate(biomes[currentBiome].groundParts[0], spawnPosition, Quaternion.identity, transform) as GameObject;
            newSide = Instantiate(biomes[currentBiome].sideParts[0], spawnPosition, Quaternion.identity, transform) as GameObject;
            instanciatedGround.Add(newGround);
            instanciatedSide.Add(newSide);
            generatedStartParts += 1;
        }
    }

    // Create a new part and add it to the 'instanciatedGround' list
    private void GenerateNewPart()
    {
        GameObject newGround;
        GameObject newSide;
        Vector3 spawnPosition = GetSpawnPosition();

        numberPartsTraveled += 1;
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
            randomNumber = Random.Range(0, biomes[currentBiome].groundParts.Length);
        lastRandomGround = randomNumber;
        return biomes[currentBiome].groundParts[randomNumber];
    }

    // Get a random part from the array 'sideParts'. Does not allow to have the same tile side to side
    private GameObject GetRandomSide()
    {
        int randomNumber = lastRandomSide;

        while (randomNumber == lastRandomSide)
            randomNumber = Random.Range(0, biomes[currentBiome].sideParts.Length);
        lastRandomSide = randomNumber;
        return biomes[currentBiome].sideParts[randomNumber];
    }

    // Get spawn position for new part taking 'partSize' and the number of instanciated parts
    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPos = Vector3.zero;

        if (instanciatedGround.Count > 0)
            spawnPos.z = partSize + instanciatedGround[instanciatedGround.Count - 1].transform.position.z;
        return spawnPos;
    }

    // Delete first ground and side parts to be generated
    public void DeleteFirstPart()
    {
        GameObject groundToDelete = instanciatedGround[0];

        Destroy(groundToDelete); // Destroy ground
        instanciatedGround.RemoveAt(0);

        Destroy(instanciatedSide[0]); // Destroy side if part is not transition
        instanciatedSide.RemoveAt(0);
    }

    #endregion PartCreation


    #region BiomeManagement

    // Change to next biome (or reset to first one if no next) and generate beginning of biome
    private void GenerateNextBiome(bool generateTransitions)
    {
        int totalBiomes = biomes.Length;

        if (generateTransitions)
            GenerateTransitionBiome(biomes[currentBiome].outTransitionGround, biomes[currentBiome].outTransitionSide);
        if (currentBiome + 1 >= totalBiomes)
            currentBiome = 0;
        else
            currentBiome += 1;

        if (generateTransitions)
            GenerateTransitionBiome(biomes[currentBiome].inTransitionGround, biomes[currentBiome].inTransitionSide);
        if (CheckBiomes())
            GenerateBeginning();
    }

    private void GenerateTransitionBiome(GameObject[] transitionGround, GameObject[] transitionSide)
    {
        if (transitionGround.Length <= 0 || transitionGround.Length <= 0)
        {
            Debug.LogError("Error: A transition part is not given.");
            return;
        }
        for (int index = 0; index < transitionGround.Length; index++)
        {
            GameObject newGround;
            GameObject newSide;
            Vector3 spawnPosition = GetSpawnPosition();

            newGround = Instantiate(transitionGround[index], spawnPosition, Quaternion.identity, transform) as GameObject;
            newSide = Instantiate(transitionSide[index], spawnPosition, Quaternion.identity, transform) as GameObject;
            instanciatedGround.Add(newGround);
            instanciatedSide.Add(newSide);
        }
    }

    #endregion BiomeManagement

    public float GetPartSize()
    {
        return partSize;
    }
}
