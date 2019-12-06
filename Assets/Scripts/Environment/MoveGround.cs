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
    private MapManager mapManager;

    private void Start()
    {
        actualSpeed = startSpeed;
        mapManager = GetComponent<MapManager>();
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
        Transform firstChild;

        if (transform.childCount > 0)
        {
            firstChild = transform.GetChild(0);
            if (firstChild != null && Mathf.Abs(firstChild.transform.position.z) >= mapManager.GetPartSize() * 2)
                mapManager.DeleteFirstPart();
        }
    }

    private void ResetPartPosition()
    {
        if (Mathf.Abs(transform.position.z) >= mapManager.GetPartSize() * numberTilesBeforeReset)
        {
            transform.position = Vector3.zero;
            foreach (Transform child in transform)
            {
                Vector3 childNewPos = child.position;

                childNewPos.z -= mapManager.GetPartSize() * numberTilesBeforeReset;
                child.position = childNewPos;
            }
        }
    }
}
