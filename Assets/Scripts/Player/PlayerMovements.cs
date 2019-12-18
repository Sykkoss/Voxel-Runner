using UnityEngine;
using System.Collections;

public class PlayerMovements : MonoBehaviour {

    private BoxCollider coll;
    private bool isDoingAction;

    private Vector3 baseSize, basePosition;

    private Animator anim;

	private void Start ()
    {
        coll = GetComponent<BoxCollider>();
        anim = GetComponentInChildren<Animator>();
        isDoingAction = false;
        baseSize = coll.size;
        basePosition = coll.center;
    }

	private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isDoingAction)
        {
            anim.SetTrigger("Jump");
            StartCoroutine(DoAction(basePosition + new Vector3(0f, .9f, 0f), baseSize));
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !isDoingAction)
        {
            anim.SetTrigger("Slide");
            StartCoroutine(DoAction(basePosition - new Vector3(0f, .4f, 0f), new Vector3(baseSize.x, baseSize.y / 3f, baseSize.z)));
        }
    }

    private IEnumerator DoAction(Vector3 center, Vector3 size)
    {
        isDoingAction = true;
        coll.center = center;
        coll.size = size;
        yield return new WaitForSeconds(1f);
        coll.center = basePosition;
        coll.size = baseSize;
        isDoingAction = false;
    }
}
