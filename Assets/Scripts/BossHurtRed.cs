using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHurtRed : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;

    public void TurnRed()
    {
        StopAllCoroutines();
        StartCoroutine(TurnRedCoroutine());
    }

    IEnumerator TurnRedCoroutine()
    {
        sprite.color = Color.red;

        yield return new WaitForSeconds(.25f);

        sprite.color = Color.white;
    }
}
