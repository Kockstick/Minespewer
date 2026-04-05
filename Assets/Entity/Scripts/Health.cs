using System.Threading.Tasks;
using UnityEditor.Rendering;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Entity entity;
    private Rigidbody entityRb;
    private int maxHealth = 1;
    public int health = 1;

    [SerializeField] private float impulseOnDamage = 20;

    private float timeLastGetDamage = 0;
    [SerializeField] private float delayToHeal = 10;
    [SerializeField] private float delayHeal = 1;
    private float timeLastHeal = 0;

    [SerializeField] private DeathExplosion deathExplosionPrefab;
    [SerializeField] private GameObject corpsePrefab;

    public delegate void OnChangeHealth_EventHalder(int health, int? lastHealth = null);
    public OnChangeHealth_EventHalder OnChangeHealth;
    public OnChangeHealth_EventHalder OnChangeMaxHealth;
    public delegate void OnDamage_EventHalder(Bullet bullet);
    public OnDamage_EventHalder OnDamage;

    void Start()
    {
        entity = GetComponent<Entity>();
        entityRb = entity.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Time.time - timeLastGetDamage < delayToHeal || health >= maxHealth)
            return;

        Heal();
    }

    private void Heal()
    {
        if (Time.time - timeLastHeal < delayHeal)
            return;

        timeLastHeal = Time.time;
        var newHealth = Mathf.Clamp(health + 1, 0, maxHealth);
        if (OnChangeHealth != null) OnChangeHealth(newHealth, health);
        health = newHealth;
        //Heal animation
    }

    public void SetDamage(Bullet bullet)
    {
        if (OnDamage != null) OnDamage(bullet);

        var impulseVelocity = (transform.position - bullet.transform.position).normalized * impulseOnDamage;
        impulseVelocity.y = 0;
        entityRb.AddForce(impulseVelocity, ForceMode.Impulse);

        timeLastGetDamage = Time.time;
        health -= bullet.damage;
        if (OnChangeHealth != null) OnChangeHealth(health);

        if (health <= 0)
            Die(bullet.sender);
    }

    public void SetMaxHealth(int value)
    {
        maxHealth = value;
        health = value;
        if (OnChangeHealth != null) OnChangeHealth(health);
        if (OnChangeMaxHealth != null) OnChangeMaxHealth(maxHealth);
    }

    private void Die(Entity killer)
    {
        killer.OnKill(entity);
        Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
        Instantiate(corpsePrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public int GetMaxHealth() => maxHealth;
    public int GetHealth() => health;
}
