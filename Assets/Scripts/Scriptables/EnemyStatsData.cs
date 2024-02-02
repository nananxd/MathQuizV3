using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsData", menuName = "ScriptableObjects/Enemy Stats Data", order = 1)]

public class EnemyStatsData : ScriptableObject
{
    public Topic topic;
    public string enemyName;
    public int health;
    public int damage;
    public GameObject enemyPrefab;
}
