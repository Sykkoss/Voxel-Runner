using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class CollisionManager : MonoBehaviour
{
    private float timerMagnet, timerInvincible, timerMultiplicator;
    private BoxCollider bc;

    private void Start()
    {
        timerMagnet = 0f;
        timerInvincible = 0f;
        timerMultiplicator = 0f;
        bc = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + (1 * (timerMultiplicator > 0 ? PowerupProperties.coinMultiplicatorPower : 1)));
        }
        else if (col.CompareTag("Powerup"))
        {
            switch (col.GetComponent<Powerup>().GetPowerupType())
            {
                case Powerup.PowerupType.Invincible:
                    timerInvincible += PowerupProperties.invincibleDuration;
                    break;

                case Powerup.PowerupType.Magnet:
                    timerMagnet += PowerupProperties.magnetDuration;
                    break;

                case Powerup.PowerupType.ScoreMultiplicator:
                    timerMultiplicator += PowerupProperties.coinMultiplicatorDuration;
                    break;

                default:
                    throw new System.ArgumentException("Invalid powerup type");
            }
            Destroy(col.gameObject);
        }
    }

    private void Update()
    {
        timerMagnet -= Time.deltaTime;
        timerInvincible -= Time.deltaTime;
        timerMultiplicator -= Time.deltaTime;
        if (timerMagnet <= 0f) timerMagnet = 0f;
        if (timerInvincible <= 0f)
        {
            bc.enabled = true;
            timerInvincible = 0f;
        }
        else
        {
            bc.enabled = false;
        }
        if (timerMultiplicator <= 0f) timerMultiplicator = 0f;
    }
}
