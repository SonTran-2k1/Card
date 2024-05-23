using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ring;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelSpawn : MonoBehaviour
{
    public List<GameObject> prefabToSpawn;
    public int spaceX;
    public int spaceY;
    //public Transform destination;

    private void Start()
    {
        SpawnPrefabFunction();
    }

    void SpawnPrefabFunction()
    {
        int countSpawn = prefabToSpawn.Count;
        for (int i = 0; i < countSpawn; i++)
        {
            int count = Random.Range(0, prefabToSpawn.Count);
            var A = Instantiate(prefabToSpawn[count], Vector3.zero, Quaternion.identity, transform);
            prefabToSpawn.RemoveAt(count);
            GameManager.Instance._gameController._listCard.Add(A.GetComponent<CardManager>());
            A.GetComponent<RectTransform>().transform.localPosition = Vector3.zero;
        }

        /*int countRow = 2;
        System.Random rng = new System.Random();
        GameManager.Instance._gameController._listCard =
            GameManager.Instance._gameController._listCard.OrderBy(a => rng.Next()).ToList();
        for (int i = 0; i < countRow; i++)
        {
            for (int j = 0; j < GameManager.Instance._gameController._listCard.Count / 2; j++)
            {
                GameManager.Instance._gameController._listCard[j].GetComponent<RectTransform>().localPosition =
                    new Vector3((j % 2 == 0 ? spaceX + j * 100 : -(spaceX + j * 100)), (i == 0 ? spaceY : -spaceY), 0);
                Bug.LogError(GameManager.Instance._gameController._listCard[j].GetComponent<RectTransform>()
                    .localPosition);
                //Bug.LogError(j);
            }
        }*/
    }
}
