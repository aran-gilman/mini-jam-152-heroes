using System.Collections;
using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float cameraSpeed = 10.0f;

    [SerializeField]
    private string cameraTargetTag = "CameraTarget";

    public void PanToTarget()
    {
        StartCoroutine(PanToTargetInternal());
    }

    private IEnumerator PanToTargetInternal()
    {
        yield return null;
        GameObject target = GameObject.FindWithTag(cameraTargetTag);
        if (target == null)
        {
            yield break;
        }

        Vector3 newPosition = target.transform.position;
        newPosition.z = transform.position.z;
        Vector3 direction = (newPosition - transform.position).normalized;
        bool hasReachedTarget = false;

        while (!hasReachedTarget)
        {
            float sqrRemainingDistance =
                Vector3.SqrMagnitude(newPosition - transform.position);
            float distanceThisFrame = cameraSpeed * Time.deltaTime;
            if (sqrRemainingDistance < distanceThisFrame * distanceThisFrame)
            {
                transform.position = newPosition;
                hasReachedTarget = true;
            }
            else
            {
                transform.position += distanceThisFrame * direction;
            }
        }
    }

    private void Start()
    {
        GameObject target = GameObject.FindWithTag(cameraTargetTag);
        if (target != null)
        {
            Vector3 newPosition = target.transform.position;
            newPosition.z = transform.position.z;
            transform.position = newPosition;
        }
    }
}
