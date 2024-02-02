using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUiScreen : BaseScene
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private BackgroundScroll backgroundScroller;


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
        startButton.onClick.AddListener(OnClickStart);
        exitButton.onClick.AddListener(OnGameExit);
        SoundManager.Instance.PlaySoundBG("MainMenuBG");
        backgroundScroller.enabled = true;
    }

    private void DeInitialize()
    {
       // SoundManager.Instance.StopSound("MainMenuBG");
        startButton.onClick.RemoveListener(OnClickStart);
        exitButton.onClick.RemoveListener(OnGameExit);
        backgroundScroller.enabled = false;
    }

    private void OnClickStart()
    {
        SoundManager.Instance.PlaySound("ButtonSound");
        var scene =  SceneManager.Instance.GetSceneByName("Topic Ui Screen");
        scene.Show();
        Hide();
    }

    private void OnGameExit()
    {
        SoundManager.Instance.PlaySound("ButtonSound");
        Application.Quit();
    }
}
