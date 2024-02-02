using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScreen : BaseScene
{
    [SerializeField] private EnemyAvatarBattle enemyAvatar;
    [SerializeField] private PlayerAvatarBattle playerAvatar;
    [SerializeField] private Transform enemySpawnPoint;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private BackgroundScaler backgroundScaler;

    //[SerializeField] private GameObject enemyPrefab;
    //[SerializeField] private GameObject playerPrefab;
    //[SerializeField] private Transform enemyPosition;
    //[SerializeField] private Transform playerPosition;

    private GameObject player;
    private GameObject enemy;

    public bool isBattleFinished;
    public EnemyAvatarBattle EnemyAvatar { get => enemyAvatar; set => enemyAvatar = value; }
    public PlayerAvatarBattle PlayerAvatar { get => playerAvatar; set => playerAvatar = value; }
   

    public override void OnShow()
    {
        base.OnShow();
        InitializeBattle();
    }

    public override void OnHide()
    {
        DeInitializeBattle();
        base.OnHide();
    }

    private void InitializeBattle()
    {
        Debug.Log("Initialize Battle");
        SetupBattleField();
        backgroundScaler.AdjustBackground();
        SoundManager.Instance.PlaySoundBG("BattleSound");
       
       // player = Instantiate(playerPrefab, playerPosition.position, playerPosition.rotation);
       // enemy = Instantiate(enemyPrefab, enemyPosition.position, enemyPosition.rotation);
    }

    private void DeInitializeBattle()
    {

        //player = null;
        //enemy = null;
        SoundManager.Instance.StopSound("BattleSound");
        if (playerAvatar !=null && enemyAvatar != null)
        {
            var player = playerAvatar.gameObject;
            var enemy = enemyAvatar.gameObject;
            playerAvatar = null;
            enemyAvatar = null;
            Destroy(player);
            Destroy(enemy);
        }
       
    }

    public void SetupBattleField()
    {
        //setup topic , questions , answer , hints and step by step process
        //set up player sprite and enemy sprite
        //enemy setup
        var chosenEnemy = EnemyDataManager.Instance.GetEnemyByTopic(GameManager.Instance.currentTopic);
        var enemy = Instantiate(chosenEnemy.enemyPrefab,enemySpawnPoint);
        enemy.transform.position = enemySpawnPoint.transform.position;
        enemyAvatar = enemy.GetComponent<EnemyAvatarBattle>();
        enemyAvatar.Setup(chosenEnemy.health,chosenEnemy.damage,chosenEnemy.enemyName);
        enemyAvatar.UpdatePosition();

        //player setup
        var player = Instantiate(playerStats.modelPrefab, playerSpawnPoint);
        player.transform.position = playerSpawnPoint.transform.position;
        playerAvatar = player.GetComponent<PlayerAvatarBattle>();
        playerAvatar.Setup(playerStats.health,playerStats.damage);
        playerAvatar.UpdatePosition();

        #region old code
        //var enemy =  EnemyDataManager.Instance.GetEnemyDataByTopic(GameManager.Instance.currentTopic);
        //var question = QuestionsDataManager.Instance.GetQuestionByTopic(GameManager.Instance.currentTopic);
        //enemyAvatar.Setup(enemy,question);
        //enemyAvatar.SetupForStartingDialogue();
        #endregion
    }


}
