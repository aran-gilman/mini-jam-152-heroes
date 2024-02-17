using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class TriggerArea : MonoBehaviour
{
    [SerializeField]
    private string requiredTag = "Player";

    [SerializeField]
    private UnityEvent<Collider2D> onEnterArea;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (requiredTag == string.Empty || collision.CompareTag(requiredTag))
        {
            onEnterArea.Invoke(collision);
        }
    }
}
