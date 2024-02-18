using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    public int MaxHealth
    {
        get => maxHealth;
        set
        {
            if (maxHealth == value)
            {
                return;
            }

            maxHealth = value;
            if (maxHealth < 0)
            {
                maxHealth = 0;
                onDeath.Invoke();
            }

            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            onMaxHealthChange.Invoke();
        }
    }

    private enum ProjectileState
    {
        Unreflected,
        Reflected,
    }
    [SerializeField]
    private ProjectileState damagingProjectiles;

    [SerializeField]
    private float invincibilityTime = 0.5f;

    [SerializeField]
    private UnityEvent onMaxHealthChange;
    public UnityEvent OnMaxHealthChange => onMaxHealthChange;

    [SerializeField]
    private UnityEvent onHealthChange;
    public UnityEvent OnHealthChange => onHealthChange;

    [SerializeField]
    private UnityEvent onDeath;

    public UnityEvent OnDeath => onDeath;

    private int currentHealth;
    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            if (currentHealth == value)
            {
                return;
            }

            currentHealth = value;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                onDeath.Invoke();
            }
            onHealthChange.Invoke();
        }
    }

    private bool isInvincible;

    public void NewPhaseHealth(int newHealth)
    {
        MaxHealth = newHealth;
        CurrentHealth = newHealth;
    }

    private void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        StartCoroutine(RunInvincibilityCooldown());
    }

    private IEnumerator RunInvincibilityCooldown()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isInvincible)
        {
            return;
        }

        if (collision.gameObject.TryGetComponent(out ProjectileBehavior projectile))
        {
            if (projectile.IsReflected)
            {
                if (damagingProjectiles == ProjectileState.Reflected)
                {
                    TakeDamage(projectile.ReflectedDamage());
                }
            }
            else if (damagingProjectiles == ProjectileState.Unreflected)
            {
                TakeDamage(projectile.UnreflectedDamage());
            }
        }
    }
}
