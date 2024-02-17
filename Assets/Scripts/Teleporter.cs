using System.Collections;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    private string targetTag = "Respawn";

    public void TeleportToTarget()
    {
        StartCoroutine(DelayedTeleport());
    }

    private IEnumerator DelayedTeleport()
    {
        yield return null;
        GameObject target = GameObject.FindWithTag(targetTag);
        if (target != null)
        {
            transform.position = target.transform.position;
        }
    }
}
