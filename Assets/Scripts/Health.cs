using System;
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
        private set
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

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ProjectileBehavior projectile))
        {
            if (projectile.IsReflected)
            {
                if (damagingProjectiles == ProjectileState.Reflected)
                {
                    CurrentHealth -= 1;
                }
            }
            else if (damagingProjectiles == ProjectileState.Unreflected)
            {
                CurrentHealth -= 1;
            }
        }
    }
}
