using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScreen : BaseScene
{
    [SerializeField] private TutorialData currentTutorial;
    [SerializeField] private TutorialDataController tutorialController;
    [SerializeField] private TutorialSlidesData currentTutSlides;
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
        var screen = SceneManager.Instance.GetSceneByName("Dialogue Screen");
        //tutorialController.Setup(currentTutorial);
        tutorialController.Setup(currentTutSlides);
        //var dialogScreen = screen.GetComponent<DialougeManager>();

        //dialogScreen.OnFinishDialogue += OnFinishTutorial;
        //dialogScreen.Setup(currentTutorial.tutorialData.dialogues);
        //dialogScreen.SetDialogue(false);
        //screen.Show();

    }

    private void DeInitialize()
    {
        //currentTutorial = null;
        //tutorialController.Unsetup();
        tutorialController.UnSetup();
        //var screen = SceneManager.Instance.GetSceneByName("Dialogue Screen");
        //var dialogScreen = screen.GetComponent<DialougeManager>();

        //dialogScreen.OnFinishDialogue -= OnFinishTutorial;
    }

    public void SetTutorialData(TutorialData tutorialData)
    {
        currentTutorial = tutorialData;
    }

    public void SetTutorialData(TutorialSlidesData tutorialSlidesData)
    {
        currentTutSlides = tutorialSlidesData;
    }
    public void OnFinishTutorial()
    {
       // Debug.Log("Tutorial Finish Loading map screen and map ui screen");
        var screenMap = SceneManager.Instance.GetSceneByType<MapScreen>();
        var screenMapUI = SceneManager.Instance.GetSceneByType<MapUiScreen>();
        var loadingScreen = SceneManager.Instance.GetSceneByType<LoadingScreen>();

        loadingScreen.Show();
        screenMap.Show();
        screenMapUI.Show();
        Hide();
        loadingScreen.Hide();
    }

    
}
