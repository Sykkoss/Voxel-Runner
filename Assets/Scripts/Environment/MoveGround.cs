using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGround : MonoBehaviour
{
    #region Argument-TweekNumbers

    public float startSpeed;
    public int numberTilesBeforeReset; // Number of tiles player will pass before reseting position to 0

    #endregion Argument-TweekNumbers

    private float actualSpeed;
    private float distanceTraveled;
    private GroundManager groundManager;

    private void Start()
    {
        actualSpeed = startSpeed;
        distanceTraveled = 0f;
        groundManager = GetComponent<GroundManager>();
    }

    private void FixedUpdate()
    {
        MoveParts();
        DeletePassedParts();
        ResetPartPosition();
    }

    private void MoveParts()
    {
        transform.Translate(Vector3.left * actualSpeed * Time.deltaTime);
        distanceTraveled += actualSpeed * Time.deltaTime;
    }

    private void DeletePassedParts()
    {
        if (distanceTraveled >= groundManager.GetPartSize() * 2)
        {
            groundManager.DeleteFirstPart();
            distanceTraveled -= groundManager.GetPartSize();
        }
    }

    private void ResetPartPosition()
    {
        if (Mathf.Abs(transform.position.x) >= groundManager.GetPartSize() * numberTilesBeforeReset)
        {
            transform.position = Vector3.zero;
            foreach (Transform child in transform)
            {
                Vector3 childNewPos = child.position;

                childNewPos.x -= groundManager.GetPartSize() * numberTilesBeforeReset;
                child.position = childNewPos;
            }
        }
    }
}
