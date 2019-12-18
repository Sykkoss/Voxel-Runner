using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private PowerupType type;

    public PowerupType GetPowerupType()
        => type;

    public enum PowerupType
    {
        Magnet,
        ScoreMultiplicator,
        Invincible
    }
}
