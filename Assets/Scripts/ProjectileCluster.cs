using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCluster : MonoBehaviour
{
    Transform player;
    [SerializeField] bool facePlayer;
    [SerializeField] float spinSpeed;
    [SerializeField] float seperationDelay;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        if (facePlayer)
        {
            Vector3 facing = player.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, facing);
        }

        (List<ColorType>, List<ColorType>, List<ColorType>) phaseColors = GameObject.FindWithTag("Boss").GetComponent<BossAttack>().GetPhaseColors();
        List<ColorType> colorsPerRarity = new List<ColorType>();

        int r = Random.Range(0, phaseColors.Item1.Count);
        colorsPerRarity.Add(phaseColors.Item1[r]);

        for(int i = 0; i < 100; i++)
        {
            r = Random.Range(0, phaseColors.Item2.Count);
            if(phaseColors.Item2[r] != colorsPerRarity[0])
            {
                break;
            }
        }
        colorsPerRarity.Add(phaseColors.Item2[r]);

        for (int i = 0; i < 100; i++)
        {
            r = Random.Range(0, phaseColors.Item3.Count);
            if (!colorsPerRarity.Contains(phaseColors.Item3[r]))
            {
                break;
            }
        }
        colorsPerRarity.Add(phaseColors.Item3[r]);

        for(int i = 0; i < transform.childCount; i++)
        {
            ProjectileBehavior PB = transform.GetChild(i).GetComponent<ProjectileBehavior>();
            PB.color = colorsPerRarity[PB.rarity];
        }
    }

    private void Update()
    {
        if(facePlayer)
        {
            Vector3 facing = player.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, facing);
        }
    }

    private void FixedUpdate()
    {
        if (spinSpeed > 0)
        {
            transform.eulerAngles += Vector3.forward * spinSpeed * Time.fixedDeltaTime;
        }
    }

    public void Release()
    {
        StartCoroutine(ReleaseCoroutine());
    }

    IEnumerator ReleaseCoroutine()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<ProjectileBehavior>().Activate();
        }

        yield return new WaitForSeconds(seperationDelay);

        while (transform.childCount > 0)
        {
            transform.GetChild(0).parent = null;
        }

        Destroy(gameObject);
    }
}
