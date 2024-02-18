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
    Transform boss;
    Transform clusterCenter;
    public int rarity;
    [SerializeField] float speed;
    [SerializeField] bool targetPlayer;
    [SerializeField] bool oppositeDirection;
    float reflectSpeed = 8;
    [SerializeField] bool bigShot;
    float bigShotSize = .9f;
    [SerializeField] float randomDegreeOffset;
    [SerializeField] bool startInvisible;
    Vector2 reflectedDamage = new Vector2 (1, 1);
    Vector2 unreflectedDamage = new Vector2(1, 1);
    float shotFlashTime = .09f;

    public bool IsReflected { get; private set; }

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        boss = GameObject.FindWithTag("Boss").transform;
        clusterCenter = transform.parent;
        if (bigShot)
        {
            transform.localScale = Vector3.one * bigShotSize;
            StartCoroutine(BigShotFlash());
        }
    }

    private void Start()
    {
        sprite.color = colorList[((int)color)];
        if (startInvisible) sprite.color = Color.clear;
        StartCoroutine(SelfDestructTimer());
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

        float offset = Random.Range(-randomDegreeOffset, randomDegreeOffset);
        direction = Quaternion.Euler(Vector3.forward * offset) * direction;

        rb.velocity = direction.normalized * speed;
        myCollider.enabled = true;
        if(startInvisible) sprite.color = colorList[((int)color)];

        animator.SetTrigger("Released");

    }

    public void Reflect()
    {
        if (!bigShot) Destroy(gameObject);

        Vector3 direction = boss.position - transform.position;
        rb.velocity = direction.normalized * reflectSpeed;
        rb.drag = 0;
        transform.parent = null;
        IsReflected = true;
        sprite.color = Color.white;
    }

    public int ReflectedDamage()
    {
        Destroy(gameObject);

        if (bigShot)
        {
            return (int)reflectedDamage.y;
        }
        else
        {
            return (int)reflectedDamage.x;
        }
    }

    public int UnreflectedDamage()
    {
        if (bigShot)
        {
            return (int)unreflectedDamage.y;
        }
        else
        {
            return (int)unreflectedDamage.x;
        }
    }

    IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(5f);

        if ((transform.position - boss.position).magnitude > 20)
        {
            Destroy(gameObject);
        }

        StartCoroutine(SelfDestructTimer());
    }

    IEnumerator BigShotFlash()
    {
        yield return new WaitForSeconds(shotFlashTime);

        if (!IsReflected)
        {
            sprite.color = (Color.white + colorList[((int)color)])/2;
        }

        yield return new WaitForSeconds(shotFlashTime);

        if (!IsReflected)
        {
            sprite.color = colorList[((int)color)];
            StartCoroutine(BigShotFlash());
        }
    }
}
