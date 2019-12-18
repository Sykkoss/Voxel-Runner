using UnityEngine;

public class Magnet : MonoBehaviour
{
    private CollisionManager cm;
    private SphereCollider magnetCollider;
    private const float speed = 20f;

    private void Start()
    {
        cm = transform.parent.GetComponentInChildren<CollisionManager>();
        magnetCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        magnetCollider.enabled = cm.IsMagnetActive();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Coin") || other.CompareTag("Powerup"))
            other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, speed * Time.deltaTime);
    }
}
