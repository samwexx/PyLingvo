using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;



    [Header("Combat stats")]
    public int damage;
    public float weaponRange;
    public float knockbackForce;
    public float knockbackTime;
    public float stunTime;

    [Header("Movement stats")]
    public int speed;

    [Header("Health stats")]
    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
