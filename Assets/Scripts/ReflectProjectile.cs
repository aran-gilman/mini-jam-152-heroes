using UnityEngine;
using UnityEngine.Events;

public class ReflectProjectile : MonoBehaviour
{
    [SerializeField]
    private ColorType color;

    [SerializeField]
    private UnityEvent onHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ProjectileBehavior projectile))
        {
            if (!projectile.IsReflected && projectile.color == color)
            {
                onHit.Invoke();
                projectile.Reflect();
            }
        }
    }
}
