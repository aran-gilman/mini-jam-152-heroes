using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Transform attackOrigin;
    [SerializeField] float phaseDelay;
    [SerializeField] float idleTime;
    [SerializeField] float windupTime;
    [SerializeField] float attackTime;
    [SerializeField] List<BossAttackList> bossPhases = new List<BossAttackList>();
    [SerializeField] int currentPhase = 0;
    [SerializeField] Health health;
    GameObject heldPattern;

    [SerializeField] UnityEvent onAllPhasesDone;

    private void Awake()
    {
        StartCoroutine(DelayedStart());
    }

    IEnumerator MainLoop()
    {
        for (int i = 0; i < bossPhases[currentPhase].AttackListLength(); i++)
        {
            List<GameObject> currentAttack;
            List<float> windupModifier;
            (currentAttack, windupModifier) = bossPhases[currentPhase].GetAttackEntry(i);

            for(int j = 0; j < currentAttack.Count; j++)
            {
                animator.SetTrigger("Windup");
                heldPattern = Instantiate(currentAttack[j], attackOrigin.position, Quaternion.identity, transform);
                if(j >= windupModifier.Count)
                {
                    windupModifier.Add(1);
                }

                yield return new WaitForSeconds(windupTime * windupModifier[j]);

                animator.SetTrigger("Attack");
                heldPattern.GetComponent<ProjectileCluster>().Release();

                yield return new WaitForSeconds(attackTime);
            }

            if (currentAttack.Count >= windupModifier.Count)
            {
                windupModifier.Add(1); //adds one to the end for idle modifying
            }

            animator.SetTrigger("Idle");

            yield return new WaitForSeconds(idleTime * windupModifier[currentAttack.Count]);
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
            StartCoroutine(DelayedStart());
        }
        else
        {
            onAllPhasesDone.Invoke();
        }
    }

    IEnumerator DelayedStart()
    {
        health.NewPhaseHealth(bossPhases[currentPhase].GetStartingHealth());
        yield return new WaitForSeconds(phaseDelay);
        StartCoroutine(MainLoop());
    }

}
