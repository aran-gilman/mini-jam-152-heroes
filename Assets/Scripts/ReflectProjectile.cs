using UnityEngine;

public class ReflectProjectile : MonoBehaviour
{
    [SerializeField]
    private ColorType color;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out ProjectileBehavior projectile))
        {
            if (!projectile.IsReflected && projectile.color == color)
            {
                projectile.Reflect();
            }
        }
    }
}
