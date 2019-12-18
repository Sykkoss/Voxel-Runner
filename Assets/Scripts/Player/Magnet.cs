using UnityEngine;

public class Magnet : MonoBehaviour
{
    private CollisionManager cm;
    private const float speed = 10f;

    private void Start()
    {
        cm = transform.parent.GetComponentInChildren<CollisionManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Coin") || other.CompareTag("Powerup"))
            other.transform.Translate(Vector3.Normalize(other.transform.position - transform.position) * speed * Time.deltaTime);
    }
}
