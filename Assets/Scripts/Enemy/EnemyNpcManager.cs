using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyNpcManager : MonoBehaviour
{
    public List<EnemyNpc> enemies;

    private static EnemyNpcManager instance = null;
    public static EnemyNpcManager Instance
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

    public void FindEnemyToRespawn()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].isHidden)
            {
                enemies[i].ShowEnemyNpc();
            }
        }
    }

    public void RespawnEnemyNpc(EnemyNpc enemy,bool isKilled)
    {     
        var selectedEnemy = enemies.Find(e => e == enemy);
        //var selectedEnemy = enemies.FindIndex(e => enemy);
        if (selectedEnemy != null)
        {
            selectedEnemy.SetWaitTime(isKilled);
            selectedEnemy.ShowEnemyNpc();
            //enemies[selectedEnemy].ShowEnemyNpc();
        }
    }

    public void DespawnEnemyNpc(EnemyNpc enemy)
    {
        var selectedEnemy = enemies.Find(e => e == enemy);
       // var selectedEnemy = enemies.FindIndex(e => enemy);
        if (selectedEnemy != null)
        {
            selectedEnemy.HideEnemyNpc();
            //enemies[selectedEnemy].HideEnemyNpc();
        }
    }

    public void RespawnAllEnemy()
    {
        var enemy = enemies.Where(e => e.topic == GameManager.Instance.currentTopic).ToList();
        foreach (var item in enemy)
        {
            item.RespawnNoDelay();
        }
    }

    
}
