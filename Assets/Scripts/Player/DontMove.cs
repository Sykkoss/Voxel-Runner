using UnityEngine;

public class DontMove : MonoBehaviour
{
    private Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    private void Update()
    {
        transform.position = initPos;
    }
}
