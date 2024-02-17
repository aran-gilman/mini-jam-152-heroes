using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform attackOrigin;
    [SerializeField] float phaseDelay;
    [SerializeField] float idleTime;
    [SerializeField] float windupTime;
    [SerializeField] float attackTime;
    [SerializeField] List<BossAttackList> bossPhases = new List<BossAttackList>();
    int currentPhase = 0;
    [SerializeField] Health health;
    GameObject heldPattern;



    private void Awake()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator MainLoop()
    {
        for (int i = 0; i < bossPhases[currentPhase].AttackListLength(); i++)
        {
            List<GameObject> currentAttack = bossPhases[currentPhase].GetAttackEntry(i);

            for(int j = 0; j < currentAttack.Count; j++)
            {
                animator.SetTrigger("Windup");
                heldPattern = Instantiate(currentAttack[j], attackOrigin.position, Quaternion.identity, transform);

                yield return new WaitForSeconds(windupTime);

                animator.SetTrigger("Attack");
                heldPattern.GetComponent<ProjectileCluster>().Release();
                heldPattern = null;

                yield return new WaitForSeconds(attackTime);
            }

            animator.SetTrigger("Idle");

            yield return new WaitForSeconds(idleTime);
        }

        if(!bossPhases[currentPhase].AttackJustOnce()) StartCoroutine(MainLoop());
    }

    public (List<ColorType>,List<ColorType>,List<ColorType>, List<ColorType>) GetPhaseColors()
    {
        return bossPhases[currentPhase].GetPhaseColors();
    }

    public void NextPhase()
    {
        StopAllCoroutines();
        if (heldPattern != null) Destroy(heldPattern);
        currentPhase++;
        if(currentPhase < bossPhases.Count)
        {
            health.NewPhaseHealth(bossPhases[currentPhase].GetStartingHealth());
            StartCoroutine(DelayedStart());
        }
    }

    IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(phaseDelay);
        StartCoroutine(MainLoop());
    }

}
