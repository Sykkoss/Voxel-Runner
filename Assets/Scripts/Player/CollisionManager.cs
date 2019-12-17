using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
        }
        else if (col.CompareTag("Powerup"))
        {
            switch (col.GetComponent<Powerup>().GetPowerupType())
            {
                default:
                    throw new System.ArgumentException("Invalid powerup type");
            }
            Destroy(col.gameObject);
        }
    }
}
