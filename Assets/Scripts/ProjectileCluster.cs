using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCluster : MonoBehaviour
{
    Transform player;
    [SerializeField] bool facePlayer;

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

    public void Release()
    {
        while(transform.childCount > 0)
        {
            Transform projectile = transform.GetChild(0);
            projectile.parent = null;
            projectile.gameObject.GetComponent<ProjectileBehavior>().Activate();
        }

        Destroy(gameObject);
    }
}
