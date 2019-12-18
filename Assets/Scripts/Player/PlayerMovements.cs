using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour {

    private BoxCollider coll;
    private bool isDoingAction;

    private Vector3 baseSize;

	private void Start ()
    {
        coll = GetComponent<BoxCollider>();
        isDoingAction = false;
        baseSize = coll.size;
    }

	private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDoingAction)
        {
            StartCoroutine(DoAction(new Vector3(0f, .9f, 0f), baseSize));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isDoingAction)
        {
            StartCoroutine(DoAction(Vector3.zero, new Vector3(baseSize.x, baseSize.y / 3f, baseSize.z)));
        }
    }

    private IEnumerator DoAction(Vector3 center, Vector3 size)
    {
        isDoingAction = true;
        coll.center = center;
        coll.size = size;
        yield return new WaitForSeconds(.5f);
        coll.center = Vector3.zero;
        coll.size = baseSize;
        isDoingAction = false;
    }
}
