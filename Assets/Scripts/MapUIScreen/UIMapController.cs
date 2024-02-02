using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIMapController : UIPanel
{
    [SerializeField] private Image mapImage,enemyImage;
    [SerializeField] private TextMeshProUGUI mapNameTxt,enemyCountTxt;
    [SerializeField] private int currentEnemyCount;
    private int maxEnemyCount;
    public void Setup()
    {
        var map = UIMapDataManager.Instance.GetMapByTopic(GameManager.Instance.currentTopic);
        mapImage.sprite = map.mapSprite;
        enemyImage.sprite = map.enemySprie;
        mapNameTxt.text = map.mapName;
        //enemyCountTxt.text = map.enemyCount.ToString();
        //maxEnemyCount = map.enemyCount;
        //currentEnemyCount = maxEnemyCount;
        UpdateEnemyCount();
    }

    public void Unsetup()

    {
        
    }

    public void HiideMapUI()
    {
        var mapuiScreen = SceneManager.Instance.GetSceneByType<MapUiScreen>();
        mapuiScreen.isMapUIShown = false;
        Hide();
        
    }

    public void UpdateEnemyCountUI()
    {
        enemyCountTxt.text = currentEnemyCount.ToString() + "/" + maxEnemyCount.ToString();
    }
    public void UpdateEnemyCount()
    {
        currentEnemyCount--;
    }
}
