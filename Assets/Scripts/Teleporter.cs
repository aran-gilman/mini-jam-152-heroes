using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Respawn";

    public void TeleportToTarget()
    {
        GameObject target = GameObject.FindWithTag(targetTag);
        if (target != null)
        {
            transform.position = target.transform.position;
        }
    }
}
