using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelSpawn : MonoBehaviour
{
    public List<GameObject> prefabToSpawn;
    public List<Transform> spawnTransform;
    public Transform destination;

    private void Start()
    {
        GameManager.Instance._gameController.destination = destination;
        SpawnPrefabFunction();
    }

    void SpawnPrefabFunction()
    {
        int countSpawn = prefabToSpawn.Count;
        for (int i = 0; i < countSpawn; i++)
        {
            int count = Random.Range(0, prefabToSpawn.Count);
            var A = Instantiate(prefabToSpawn[count], spawnTransform[i].position, Quaternion.identity, transform);
            prefabToSpawn.RemoveAt(count);
            GameManager.Instance._gameController._listCard.Add(A.GetComponent<CardManager>());
        }
    }
}
