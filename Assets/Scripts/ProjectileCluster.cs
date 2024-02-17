using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCluster : MonoBehaviour
{
    Transform player;
    [SerializeField] float yOffset;
    [SerializeField] bool startFacingPlayer;
    [SerializeField] bool facePlayer;
    [SerializeField] float spinSpeed;
    [SerializeField] float seperationDelay;
    [SerializeField] Vector2 randomXOffset;
    [SerializeField] Vector2 randomYOffset;
    [SerializeField] bool randomFlipX;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;

        float rxo = Random.Range(randomXOffset.x, randomXOffset.y);
        float ryo = Random.Range(randomYOffset.x, randomYOffset.y);
        if (randomFlipX) rxo *= Random.Range(0, 2) * 2 - 1;

        transform.position += Vector3.right * rxo + Vector3.up * (yOffset+ryo);

        if (facePlayer || startFacingPlayer)
        {
            Vector3 facing = player.position - transform.position;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, facing);
        }

        (List<ColorType>, List<ColorType>, List<ColorType>, List<ColorType>) phaseColors = GameObject.FindWithTag("Boss").GetComponent<BossAttack>().GetPhaseColors();
        List<ColorType> colorsPerRarity = new List<ColorType>();

        int r = Random.Range(0, phaseColors.Item1.Count);
        colorsPerRarity.Add(phaseColors.Item1[r]);

        for(int i = 0; i < 10; i++)
        {
            r = Random.Range(0, phaseColors.Item2.Count);
            if(phaseColors.Item2[r] != colorsPerRarity[0])
            {
                break;
            }
        }
        colorsPerRarity.Add(phaseColors.Item2[r]);

        for (int i = 0; i < 10; i++)
        {
            r = Random.Range(0, phaseColors.Item3.Count);
            if (!colorsPerRarity.Contains(phaseColors.Item3[r]))
            {
                break;
            }
        }
        colorsPerRarity.Add(phaseColors.Item3[r]);

        for (int i = 0; i < 100; i++)
        {
            r = Random.Range(0, phaseColors.Item4.Count);
            if (!colorsPerRarity.Contains(phaseColors.Item4[r]))
            {
                break;
            }
        }
        colorsPerRarity.Add(phaseColors.Item4[r]);

        for (int i = 0; i < transform.childCount; i++)
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
