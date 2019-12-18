using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour {

    private BoxCollider coll;
    private bool isDoingAction;

    private Vector3 baseCenter, baseSize;
    private Vector3 smallSize;
    private Vector3 jumpPos, slidePos;

	private void Start ()
    {
        coll = GetComponent<BoxCollider>();
        isDoingAction = false;
        baseCenter = coll.center;
        baseSize = coll.size;
        smallSize = new Vector3(coll.size.x, .3f, coll.size.z);
        jumpPos = new Vector3(coll.center.x, .2f, coll.center.z);
        slidePos = new Vector3(coll.center.x, -.2f, coll.center.z);
    }

	private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDoingAction)
        {
            StartCoroutine(DoAction(jumpPos));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isDoingAction)
        {
            StartCoroutine(DoAction(slidePos));
        }
    }

    private IEnumerator DoAction(Vector3 center)
    {
        isDoingAction = true;
        coll.center = center;
        coll.size = smallSize;
        yield return new WaitForSeconds(.5f);
        coll.center = baseCenter;
        coll.size = baseSize;
        isDoingAction = false;
    }
}
