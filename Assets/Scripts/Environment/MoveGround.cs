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
    private GroundManager groundManager;

    private void Start()
    {
        actualSpeed = startSpeed;
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
        transform.Translate(Vector3.back * actualSpeed * Time.deltaTime);
    }

    private void DeletePassedParts()
    {
        Transform firstChild = null;

        if (transform.childCount > 0)
        {
            firstChild = transform.GetChild(0);
            if (firstChild != null && Mathf.Abs(firstChild.transform.position.z) >= groundManager.GetPartSize() * 2)
                groundManager.DeleteFirstPart();
        }
    }

    private void ResetPartPosition()
    {
        if (Mathf.Abs(transform.position.z) >= groundManager.GetPartSize() * numberTilesBeforeReset)
        {
            transform.position = Vector3.zero;
            foreach (Transform child in transform)
            {
                Vector3 childNewPos = child.position;

                childNewPos.z -= groundManager.GetPartSize() * numberTilesBeforeReset;
                child.position = childNewPos;
            }
        }
    }
}
