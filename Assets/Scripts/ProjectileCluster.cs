using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCluster : MonoBehaviour
{
    Transform player;
    [SerializeField] bool facePlayer;
    [SerializeField] float spinSpeed;
    [SerializeField] float seperationDelay;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        if (facePlayer)
        {
            Vector3 facing = player.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, facing);
        }
    }

    private void Update()
    {
        if(facePlayer)
        {
            Vector3 facing = player.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, facing);
        }
    }

    private void FixedUpdate()
    {
        if (spinSpeed > 0)
        {
            transform.eulerAngles += Vector3.forward * spinSpeed * Time.fixedDeltaTime;
        }
    }

    public void Release()
    {
        StartCoroutine(ReleaseCoroutine());
    }

    IEnumerator ReleaseCoroutine()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<ProjectileBehavior>().Activate();
        }

        yield return new WaitForSeconds(seperationDelay);

        while (transform.childCount > 0)
        {
            transform.GetChild(0).parent = null;
        }

        Destroy(gameObject);
    }
}
