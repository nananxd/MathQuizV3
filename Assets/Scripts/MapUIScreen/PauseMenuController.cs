using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : UIPanel
{
    public void OnClickMenu()
    {
        Show();
        //Time.timeScale = 0;
    }

    public void OnResume()
    {
        //Time.timeScale = 1f;
        Hide();
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnHome()
    {
        var map = SceneManager.Instance.GetSceneByType<MapScreen>();
        var mapUi = SceneManager.Instance.GetSceneByType<MapUiScreen>();
        var mainMenu = SceneManager.Instance.GetSceneByType<MainMenuUiScreen>();
        var loading = SceneManager.Instance.GetSceneByType<LoadingScreen>();

        mapUi.isMapUIShown = false;
        StartCoroutine(loading.StartLoadingCoroutine());
        map.Hide();
        mapUi.Hide();
        Hide();
        mainMenu.Show();
        
       
    }

    public void OnHelp()
    {
        var tutorial = SceneManager.Instance.GetSceneByType<TutorialScreen>();
        tutorial.Show();
    }
}
