using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLane : MonoBehaviour
{
  private int lane = 0;
  public enum Direction{
  	Up,
  	Left,
  	Right,
  	}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
      //Left
      if(Input.GetKeyDown(KeyCode.LeftArrow)){
        Move(Direction.Left);
      }

      //Right
      if(Input.GetKeyDown(KeyCode.RightArrow)){
        Move(Direction.Right);
      }
    }

    private void Move(Direction direction)
    {
      Vector3 newPosition = transform.position;

      //Left
      if(direction == Direction.Left){
        if(lane != -1){
          lane--;
          newPosition.x -= 0.9f;
        }
      }

      //Right
      if(direction == Direction.Right){
        if(lane != 1){
          lane++;
          newPosition.x += 0.9f;
        }
      }
      transform.position =  newPosition;
    }
}
