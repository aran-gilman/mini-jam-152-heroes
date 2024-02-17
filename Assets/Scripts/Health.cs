using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

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
        onHit.Invoke();

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            onDeath.Invoke();
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
            TakeDamage(1);
        }
    }
}
