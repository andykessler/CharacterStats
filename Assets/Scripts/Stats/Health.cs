using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterStats))]
public class Health : MonoBehaviour
{
    private Stat healthStat;

    [SerializeField]
    private float currentHealth;

    private float percentHealth;

    public float Value
    {
        get {
            return currentHealth;
        }
    }

    private void Start()
    {
        healthStat = GetComponent<CharacterStats>().Stats.Get(StatType.MaxHealth);
        healthStat.RegisterOnValueUpdatedHandler(AdjustForMaxHealth);
        currentHealth = healthStat.Value;
        UpdateHealthPercent();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Minus))
        {
            Damage(10);
        }

        if (Input.GetKeyUp(KeyCode.Equals))
        {
            Heal(10);
        }
    }

    private void OnDestroy()
    {
        healthStat.UnregisterOnValueUpdatedHandler(AdjustForMaxHealth);
    }

    public void AdjustForMaxHealth()
    {
        Debug.Log("Adjusting Max Health.");
        // TODO Make sure this percentage calculation isn't abusable.
        currentHealth = healthStat.Value * percentHealth;
        UpdateHealthPercent();
    }

    public void Damage(float amount)
    {
        Debug.Log("Damage: " + amount);
        currentHealth -= amount;
        UpdateHealthPercent();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        Debug.Log("Heal: " + amount);
        currentHealth += amount;
        UpdateHealthPercent();
        if (currentHealth > healthStat.Value)
        {
            Debug.Log("Overheal: " + (currentHealth - healthStat.Value));
            currentHealth = healthStat.Value;
        }
    }

    private void UpdateHealthPercent()
    {
        percentHealth = currentHealth / healthStat.Value;
    }

    public void Die()
    {
        Debug.Log("Die.");
        Destroy(gameObject);
    }
 
}
