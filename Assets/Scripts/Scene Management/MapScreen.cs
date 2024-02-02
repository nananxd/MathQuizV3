using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapScreen : BaseScene
{
    private const string DIALOGCOUNTERKEY = "dialogCounterKey";
    [SerializeField] private PlayerController player;
    [SerializeField] private EnemyArea enemyArea;
    [SerializeField] private GameObject lesson1NpcMonsterParent, lesson2NpcMonsterParent, lesson3NpcMonsterParent;
    [SerializeField] private GameObject lesson1Map, lesson2Map, lesson3Map;
    [SerializeField] private Transform startingPos;
    
    private EnemyNpc currentEnemyNpc;
    private int dialogCounter = -1;//counter for current dialog that save in playerprefs

    public bool startFromStartingPos;
    public bool respawnAllEnemy;
    public PlayerController Player { get => player; set => player = value; }
    public EnemyNpc CurrentEnemyNpc { get => currentEnemyNpc; set => currentEnemyNpc = value; }

    public override void OnShow()
    {
        base.OnShow();
        Initialize();
        
    }

    public override void OnHide()
    {
        DeInitialize();
        base.OnHide();
    }

    private void Initialize()
    {
       
        GameManager.Instance.SetVirtualCamera(true);
        SetPlayerPosition();
        SetMapEnemyNpc();

        SetupWorld();
        Player.Setup();
        enemyArea.Setup();
        StartCoroutine(ShowDialogCoroutine());
        SoundManager.Instance.PlaySoundBG("MapBG");
        SoundManager.Instance.StopSound("MainMenuBG");

        
        if (currentEnemyNpc!=null && currentEnemyNpc.isHidden && currentEnemyNpc.topic == GameManager.Instance.currentTopic)
        {
            EnemyNpcManager.Instance.RespawnEnemyNpc(currentEnemyNpc,false);
        }
        else if (currentEnemyNpc != null && currentEnemyNpc.isKilled && currentEnemyNpc.isHidden && currentEnemyNpc.topic == GameManager.Instance.currentTopic)
        {
            EnemyNpcManager.Instance.RespawnEnemyNpc(currentEnemyNpc, true);
        }

        SetEnemyNpc();
    }

    private void DeInitialize()
    {
        SoundManager.Instance.StopSound("MapBG");
        GameManager.Instance.SetVirtualCamera(false);
        Player.UnSetup();
        UnSetupWorld();
        enemyArea.Unsetup();
    }

    private void SetupWorld()
    {
        //setup world background, npc's,enemy, and  
    }

    private void UnSetupWorld()
    {

    }

    public void GoToBattle()
    {
        var screenLoading = SceneManager.Instance.GetSceneByType<LoadingScreen>();

        screenLoading.Show();
        Hide();
        Player.UnSetup();
        StartCoroutine(screenLoading.StartLoadingCoroutine());

        StartCoroutine(GoToBattleCoroutine());
    }

    private IEnumerator GoToBattleCoroutine()
    {
        yield return new WaitForSeconds(1.3f);
        var screenBattle = SceneManager.Instance.GetSceneByType<BattleScreen>();
        var screenBattleUI = SceneManager.Instance.GetSceneByType<BattleScreenUI>();
        var screenMapUI = SceneManager.Instance.GetSceneByType<MapUiScreen>();


        screenMapUI.Hide();
        screenBattle.Show();
        screenBattleUI.Show();
        //screenBattle.SetupBattleField();
        //screenBattle.SetupBattleData();

        //screenLoading.Hide();

    }

    public void ShowDialog()
    {
        DialogDataManager.Instance.LoadDialogData();
        var currentScore = GameManager.Instance.GetScore();
        var dialogManager = SceneManager.Instance.GetSceneByType<DialougeManager>();
        var currentDialog = DialogDataManager.Instance.GetDialog();
        if (currentDialog != null)
        {
            if (currentScore >= currentDialog.scoreRequirement && currentScore <= currentDialog.scoreLimit)
            {
                dialogManager.Setup(currentDialog);
                dialogManager.SetDialogue(false);
                dialogManager.Show();
            }
        }
        

    }

    private IEnumerator ShowDialogCoroutine()
    {
        yield return new WaitForSeconds(1f);
        ShowDialog();
    }

    public void SetEnemyNpc()
    {
        switch (GameManager.Instance.currentTopic)
        {
            case Topic.Easy:
                lesson1NpcMonsterParent.SetActive(true);
                lesson2NpcMonsterParent.SetActive(false);
                lesson3NpcMonsterParent.SetActive(false);

                lesson1Map.SetActive(true);
                lesson2Map.SetActive(false);
                lesson3Map.SetActive(false);
                break;
            case Topic.Average:
                lesson1NpcMonsterParent.SetActive(false);
                lesson2NpcMonsterParent.SetActive(true);
                lesson3NpcMonsterParent.SetActive(false);

                lesson1Map.SetActive(false);
                lesson2Map.SetActive(true);
                lesson3Map.SetActive(false);
                break;
            case Topic.Difficult:
                lesson1NpcMonsterParent.SetActive(false);
                lesson2NpcMonsterParent.SetActive(false);
                lesson3NpcMonsterParent.SetActive(true);

                lesson1Map.SetActive(false);
                lesson2Map.SetActive(false);
                lesson3Map.SetActive(true);
                break;
            
        }
    }

    public void SetPlayerPosition()
    {
        if (startFromStartingPos)
        {
            player.transform.position = startingPos.position;
            startFromStartingPos = false;
        }
    }

    public void SetMapEnemyNpc()
    {
        if (respawnAllEnemy)
        {
            EnemyNpcManager.Instance.RespawnAllEnemy();
            respawnAllEnemy = false;
        }
    }
   

    
}
