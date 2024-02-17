using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossListChoices : MonoBehaviour
{
    [SerializeField] List<GameObject> sequenceA = new List<GameObject>();
    [SerializeField] List<GameObject> sequenceB = new List<GameObject>();
    [SerializeField] List<GameObject> sequenceC = new List<GameObject>();

    [SerializeField] List<float> windupModifier = new List<float>();

    public (List<GameObject>,List<float>) RandomChoice()
    {
        List<List<GameObject>> choices = new List<List<GameObject>>();
        if (sequenceA.Count > 0) choices.Add(sequenceA);
        if (sequenceB.Count > 0) choices.Add(sequenceB);
        if (sequenceC.Count > 0) choices.Add(sequenceC);

        int x = Random.Range(0, choices.Count);
        return (choices[x],windupModifier);
    }
}
