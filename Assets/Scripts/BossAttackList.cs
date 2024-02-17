using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackList : MonoBehaviour
{
    [SerializeField] int startingHealth;
    [SerializeField] bool justOnce;
    [SerializeField] List<BossListChoices> attackList = new List<BossListChoices>();
    [SerializeField] List<ColorType> typeA = new List<ColorType>();
    [SerializeField] List<ColorType> typeB = new List<ColorType>();
    [SerializeField] List<ColorType> typeC = new List<ColorType>();
    [SerializeField] List<ColorType> typeD = new List<ColorType>();
    

    public (List<GameObject>,List<float>) GetAttackEntry(int x)
    {
        return attackList[x].RandomChoice();
    }

    public int AttackListLength()
    {
        return attackList.Count;
    }
    public (List<ColorType>, List<ColorType>, List<ColorType>, List<ColorType>) GetPhaseColors()
    {
        return (typeA, typeB, typeC, typeD);
    }

    public bool AttackJustOnce()
    {
        return justOnce;
    }

    public int GetStartingHealth()
    {
        return startingHealth;
    }
}
