using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLane : MonoBehaviour
{
  private bool isMoving;
  private int lane = 1;
  private Rigidbody controller;
    // Start is called before the first frame update
    void Start()
    {
      isMoving = false;
      controller = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      //Left
      if(Input.GetKeyDown(KeyCode.LeftArrow)){
        MoveLane(false);
      }
      //Right
      if(Input.GetKeyDown(KeyCode.RightArrow)){
        MoveLane(true);
      }
      if (isMoving == false) {
        Vector3 targetPosition = transform.position.x * Vector3.forward;
        targetPosition.y = transform.position.y;
        targetPosition.z = transform.position.z;
        if (lane == 0) {
          targetPosition += Vector3.left * 0.9f;
        } else if (lane == 2) {
          targetPosition += Vector3.right * 0.9f;
        }

        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x;
        moveVector.y = 0f;
        moveVector.z = 0f;
        StartCoroutine(MovePlayer(targetPosition, moveVector * Time.deltaTime));
      }
    }

    private IEnumerator MovePlayer(Vector3 targetPosition, Vector3 velocity) {
      float duration = 2f;

      isMoving = true;
      while(Vector3.Distance(transform.position, targetPosition) > 0.01f) {
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, duration * Time.deltaTime);
        yield return null;
      }
      transform.position = targetPosition;
      isMoving = false;
    }
    private void MoveLane(bool goingRight)
    {
      lane += (goingRight) ? 1 : -1;
      lane = Mathf.Clamp(lane, 0, 2);
    }
}
