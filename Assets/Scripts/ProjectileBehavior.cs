using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public ColorType color;

    [SerializeField] List<Color> colorList = new List<Color>();
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Rigidbody2D rb;
    Transform player;
    Transform clusterCenter;
    public int rarity;
    [SerializeField] float speed;
    [SerializeField] bool targetPlayer;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        clusterCenter = transform.parent;
    }

    private void Start()
    {
        sprite.color = colorList[((int)color)];
    }

    private void Update()
    {
        //if the projectile cluster rotates, projectiles stay oriented upright
        transform.rotation = Quaternion.identity;
    }

    public void Activate()
    {
        Vector3 direction = transform.position - clusterCenter.position;

        if (targetPlayer)
        {
            direction = player.position - transform.position;
        }

        rb.velocity = direction.normalized * speed;

    }

    public void Reflect()
    {
        rb.velocity = -rb.velocity;
    }
}
