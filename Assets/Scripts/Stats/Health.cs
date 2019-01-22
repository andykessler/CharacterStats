using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterStats))]
public class Health : MonoBehaviour
{
    private Stat healthStat;

    [SerializeField]
    private float currentHealth;

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

        Debug.Log("New Health: " + currentHealth);
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
        if (currentHealth > healthStat.Value)
        {
            currentHealth = healthStat.Value;
        }
        // When max health increases, should you gain that as current health too?
        // Would need to know delta healthStat.Value
    }

    public void Damage(float amount)
    {
        Debug.Log("Damage: " + amount);
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        Debug.Log("Heal: " + amount);
        currentHealth += amount;
        if(currentHealth > healthStat.Value)
        {
            Debug.Log("Overheal: " + (currentHealth - healthStat.Value));
            currentHealth = healthStat.Value;
        }
    }

    public void Die()
    {
        Debug.Log("Die.");
        Destroy(gameObject);
    }
 
}
