using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmBattleController : UIPanel
{
    [SerializeField] private Button yesBtn, noBtn;
    // Start is called before the first frame update
    public void Setup()
    {
        yesBtn.onClick.AddListener(OnClickYes);
        noBtn.onClick.AddListener(OnClickNo);
       HidePanel();
       
    }

    public void Unsetup()
    {
        yesBtn.onClick.RemoveListener(OnClickYes);
        noBtn.onClick.RemoveListener(OnClickNo);
        HidePanel();
    }

    public void OnClickYes()
    {
        //proceed to battle
        HidePanel();
        var mapScreen = SceneManager.Instance.GetSceneByType<MapScreen>();
        EnemyNpcManager.Instance.DespawnEnemyNpc(mapScreen.CurrentEnemyNpc);
        mapScreen.GoToBattle();
        
    }

    public void OnClickNo()
    {
        var mapScreen = SceneManager.Instance.GetSceneByType<MapScreen>();
        HidePanel();
        mapScreen.Player.Setup();
        EnemyNpcManager.Instance.DespawnEnemyNpc(mapScreen.CurrentEnemyNpc);
        EnemyNpcManager.Instance.RespawnEnemyNpc(mapScreen.CurrentEnemyNpc,false);
    }
}
