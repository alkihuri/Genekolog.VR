using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpermManager : MonoBehaviour
{
    [SerializeField] private Sperm[] prefab;
    [SerializeField] private Transform stingerPoint;
    [SerializeField] private Vector3 size;
    [SerializeField] private uint startCountPrefab;
    [SerializeField] private SpermSettings spermSettings;
    private List<Sperm> sperms = new();

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, size);
    }

    private Vector3 GetRandomPoint() => transform.position + new Vector3(Random.Range(-size.x, size.x), 0, Random.Range(-size.z, size.z));

    private Vector3 GetSecondRandPoint()
    {
        var offset = new Vector3(Random.Range(-size.x, transform.InverseTransformDirection(stingerPoint.position).x - 0.25f), 0, Random.Range(-size.z, size.z));
        
        return transform.position + offset;
    }

    private void Start()
    {
        for (var i = 0; i < startCountPrefab; i++)
        {
            var newSperm = Instantiate(prefab[Random.Range(0, prefab.Length)], GetRandomPoint(), quaternion.identity, transform);
            newSperm.Initialize(spermSettings, GetRandomPoint);
            sperms.Add(newSperm);
        }

        StartCoroutine(ChangesMoveSperm());
    }

    private IEnumerator ChangesMoveSperm()
    {
        int counter = 0;
        while (counter < sperms.Count)
        {
            yield return new WaitForSeconds(.35f);
            sperms[counter].SetNewTarget(GetSecondRandPoint);
            sperms[counter].StopMove();
            counter++;
        }
    }
}