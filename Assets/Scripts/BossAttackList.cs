using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackList : MonoBehaviour
{
    [SerializeField] List<BossListChoices> attackList = new List<BossListChoices>();

    public List<GameObject> GetAttackEntry(int x)
    {
        return attackList[x].RandomChoice();
    }

    public int AttackListLength()
    {
        return attackList.Count;
    }
}
