using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    public int MaxHealth => maxHealth;

    private enum ProjectileState
    {
        Unreflected,
        Reflected,
    }
    [SerializeField]
    private ProjectileState damagingProjectiles;

    [SerializeField]
    private UnityEvent onHit;
    public UnityEvent OnHit => onHit;

    [SerializeField]
    private UnityEvent onDeath;
    public UnityEvent OnDeath => onDeath;

    public int CurrentHealth { get; private set; }

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            onDeath.Invoke();
        }
        onHit.Invoke();
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
                    TakeDamage(1);
                }
            }
            else if (damagingProjectiles == ProjectileState.Unreflected)
            {
                TakeDamage(1);
            }
        }
    }
}
