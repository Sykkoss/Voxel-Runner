using UnityEngine;
using System.Collections;

public enum Direction{
	Up,
	Left,
	Right,
	}

public class Knight : MonoBehaviour {
	public float moveDistance;
	private int currentLane;
	private int currentColumn;

    private BoxCollider coll;
    private bool isDoingAction;

    private Vector3 baseCenter, baseSize;
    private Vector3 smallSize;
    private Vector3 jumpPos, slidePos;

	// Use this for initialization
	void Start () {
		currentColumn = 1;
		currentLane = 2;
        coll = GetComponent<BoxCollider>();
        isDoingAction = false;
        baseCenter = coll.center;
        baseSize = coll.size;
        smallSize = new Vector3(coll.size.x, .3f, coll.size.z);
        jumpPos = new Vector3(coll.center.x, .2f, coll.center.z);
        slidePos = new Vector3(coll.center.x, -.2f, coll.center.z);
    }
	
	// Update is called once per frame
	void Update () {
		//Up
		if(Input.GetKeyDown(KeyCode.UpArrow)){
			Move(Direction.Up);
		}

		//Left
		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			Move(Direction.Left);
		}

		//Right
		if(Input.GetKeyDown(KeyCode.RightArrow)){
			Move(Direction.Right);
		}

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

	void Move(Direction direction){
		//Temp
		Vector3 newPosition = transform.position;

		//Up
		if(direction == Direction.Up){
			currentColumn++;
			newPosition.x -= moveDistance;
		}


		//Left
		if(direction == Direction.Left){
			if((currentLane - 1) > 0){
				currentLane--;
				newPosition.z -= moveDistance;
			}
		}

		//Right
		if(direction == Direction.Right){
			if((currentLane + 1) < 4){
				currentLane++;
				newPosition.z += moveDistance;
			}
		}

		//Move
		transform.position =  newPosition;
	}

	void OnCollisionEnter(Collision collision) {
		Debug.Log(transform.name + " collision with " + collision.transform.name);     
    }

    void OnTriggerEnter(Collider other){
		Debug.Log(transform.name + " triggered " + other.transform.name);     
    }
}
