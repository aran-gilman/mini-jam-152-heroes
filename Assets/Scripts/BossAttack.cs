using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform attackOrigin;
    [SerializeField] float idleTime;
    [SerializeField] float windupTime;
    [SerializeField] float attackTime;
    [SerializeField] BossAttackList phase1Attacks;
    

    private void Awake()
    {
        StartCoroutine(MainLoop());
    }

    IEnumerator MainLoop()
    {
        for (int i = 0; i < phase1Attacks.AttackListLength(); i++)
        {
            List<GameObject> currentAttack = phase1Attacks.GetAttackEntry(i);

            for(int j = 0; j < currentAttack.Count; j++)
            {
                animator.SetTrigger("Windup");
                GameObject heldPattern = Instantiate(currentAttack[j], attackOrigin.position, Quaternion.identity, transform);

                yield return new WaitForSeconds(windupTime);

                animator.SetTrigger("Attack");
                heldPattern.GetComponent<ProjectileCluster>().Release();

                yield return new WaitForSeconds(attackTime);
            }

            animator.SetTrigger("Idle");

            yield return new WaitForSeconds(idleTime);
        }

        StartCoroutine(MainLoop());
    }

    public (List<ColorType>,List<ColorType>,List<ColorType>) GetPhaseColors()
    {
        return phase1Attacks.GetPhaseColors();
    }

}
