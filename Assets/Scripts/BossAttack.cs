using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float idleTime;
    [SerializeField] float windupTime;
    [SerializeField] float attackTime;
    GameObject heldAttack;
    [SerializeField] GameObject myOneAttack;
    [SerializeField] GameObject myOtherAttack;
    [SerializeField] Transform attackOrigin;

    private void Awake()
    {
        StartCoroutine(AttackTest());
    }

    IEnumerator AttackTest()
    {
        yield return new WaitForSeconds(idleTime);

        animator.SetTrigger("Windup");
        heldAttack = Instantiate(myOtherAttack, attackOrigin.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(windupTime);

        animator.SetTrigger("Attack");
        heldAttack.GetComponent<ProjectileCluster>().Release();

        yield return new WaitForSeconds(attackTime);

        animator.SetTrigger("Idle");

        yield return new WaitForSeconds(idleTime);

        animator.SetTrigger("Windup");
        heldAttack = Instantiate(myOneAttack, attackOrigin.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(windupTime);

        animator.SetTrigger("Attack");
        heldAttack.GetComponent<ProjectileCluster>().Release();

        yield return new WaitForSeconds(attackTime);

        animator.SetTrigger("Windup");
        heldAttack = Instantiate(myOneAttack, attackOrigin.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(windupTime);

        animator.SetTrigger("Attack");
        heldAttack.GetComponent<ProjectileCluster>().Release();

        yield return new WaitForSeconds(attackTime);

        animator.SetTrigger("Idle");

        StartCoroutine(AttackTest());
    }




}
