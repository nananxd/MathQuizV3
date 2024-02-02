using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyDataManager : MonoBehaviour
{
    public List<EnemyData> enemyData;
    public List<EnemyStatsData> enemyStats;

    private static EnemyDataManager instance = null;
    public static EnemyDataManager Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public EnemyData GetEnemyDataByTopic(Topic topics)
    {
        for (int i = 0; i < enemyData.Count; i++)
        {
            if (enemyData[i].topic == topics)
            {
                return enemyData[i];
            }
        }

        return null;
    }

    public EnemyStatsData GetEnemyByTopic(Topic topic)
    {
        var chosenEnemy = enemyStats.Find(x => x.topic == topic);
        if (chosenEnemy != null)
        {
            return chosenEnemy;
        }
        return null;
    }
}
