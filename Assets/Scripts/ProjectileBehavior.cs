using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public ColorType color;

    [SerializeField] List<Color> colorList = new List<Color>();
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D myCollider;
    Transform player;
    Transform clusterCenter;
    public int rarity;
    [SerializeField] float speed;
    [SerializeField] bool targetPlayer;
    [SerializeField] bool oppositeDirection;
    float reflectSpeed = 8;

    public bool IsReflected { get; private set; }

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

        if(oppositeDirection)
        {
            direction = direction * -1;
        }

        rb.velocity = direction.normalized * speed;
        myCollider.enabled = true;

        animator.SetTrigger("Released");

    }

    public void Reflect()
    {
        Vector3 direction = GameObject.FindWithTag("Boss").transform.position - transform.position;
        rb.velocity = direction.normalized * reflectSpeed;
        rb.drag = 0;
        transform.parent = null;
        IsReflected = true;
        sprite.color = Color.white;
    }
}
