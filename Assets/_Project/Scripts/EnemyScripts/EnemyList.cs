using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy List", menuName = "Farm and Bunnies/Wave/New Enemy List", order = 1)]
public class EnemyList : ScriptableObject
{
    public List<EnemySpawnData> enemiesToSpawn;
}

[System.Serializable]
public class EnemySpawnData
{
    public GameObject prefabToSpawn;
    public float timeToSpawn;
    public int crateTargetIndex;
}
