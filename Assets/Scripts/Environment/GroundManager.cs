using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    #region Arguments-Public

    #region Arguments-TweakNumbers

    public int maxParts;
    public float partSize;
    public int numberStartParts;

    #endregion Arguments-TweakNumbers

    public GameObject[] mapParts; /* Number 0 must be the empty basic part ! */

    #endregion Arguments-Public

    private List<GameObject> instanciatedParts;
    private int lastRandom;

    private void Start()
    {
        instanciatedParts = new List<GameObject>(maxParts);
        lastRandom = 0;
        GenerateBeginning();
    }

    private void Update()
    {
        if (instanciatedParts.Count < maxParts)
            GenerateNewPart();
    }

    #region PartCreation

    // Create beginning parts. Create 'numberStartParts' parts only with the first 'mapParts' part (empty tile)
    private void GenerateBeginning()
    {
        GameObject newPart;
        Vector3 spawnPosition;

        while (instanciatedParts.Count < numberStartParts)
        {
            spawnPosition = GetSpawnPosition();
            newPart = Instantiate(mapParts[0], spawnPosition, Quaternion.identity, transform) as GameObject;
            instanciatedParts.Add(newPart);
        }
    }

    // Create a new part and add it to the 'instanciatedParts' list
    private void GenerateNewPart()
    {
        GameObject newPart;
        Vector3 spawnPosition = GetSpawnPosition();

        newPart = Instantiate(GetRandomPart(), spawnPosition, Quaternion.identity, transform) as GameObject;
        instanciatedParts.Add(newPart);
    }

    // Get a random part from the array 'mapParts'. Does not allow to have the same tile side to side
    private GameObject GetRandomPart()
    {
        int randomNumber = lastRandom;

        while (randomNumber == lastRandom)
            randomNumber = Random.Range(0, mapParts.Length);
        lastRandom = randomNumber;
        return mapParts[randomNumber];
    }

    // Get spawn position for new part taking 'partSize' and the number of instanciated parts
    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPos = Vector3.zero;

        if (instanciatedParts.Count > 0)
            spawnPos.x = partSize + instanciatedParts[instanciatedParts.Count - 1].transform.position.x;
        return spawnPos;
    }

    #endregion PartCreation

    public void DeleteFirstPart()
    {
        instanciatedParts.RemoveAt(0);
        Destroy(transform.GetChild(0).gameObject);
    }

    public float GetPartSize()
    {
        return partSize;
    }
}
