using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class CollisionManager : MonoBehaviour
{
    private float timerMagnet, timerInvincible, timerMultiplicator;
    private BoxCollider[] bc;
    private bool gameOver;

    private void Start()
    {
        timerMagnet = 0f;
        timerInvincible = 0f;
        timerMultiplicator = 0f;
        gameOver = false;
        bc = transform.parent.GetComponentsInChildren<BoxCollider>();
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
        else if ((col.CompareTag("Obstacle") || col.CompareTag("Spike")) && gameOver == false) {
          GameObject.Find("MapManager").GetComponent<MoveGround>().enabled = false;
          GameObject.FindWithTag("Player").GetComponent<ChangeLane>().enabled = false;
          SceneManager.LoadScene("GameOverMenu", LoadSceneMode.Additive);
          gameOver = true;
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
            foreach (BoxCollider c in bc)
                c.enabled = true;
            timerInvincible = 0f;
        }
        else
        {
            foreach (BoxCollider c in bc)
                c.enabled = false;
        }
        if (timerMultiplicator <= 0f) timerMultiplicator = 0f;
    }

    public bool IsMagnetActive()
        => timerMagnet > 0f;
}
