using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopicSelectionUiScreen : BaseScene
{
    [SerializeField] private List<TopicUIData> topics;
    [SerializeField] private TopicUiController topicPrefab;
    [SerializeField] private RectTransform parent;
    [SerializeField] private DifficultyUIController diffUIController;

    private List<TopicUiController> currentTopics = new List<TopicUiController>();

    public DifficultyUIController DiffUIController { get => diffUIController; set => diffUIController = value; }

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
       
        for (int i = 0; i < topics.Count; i++)
        {
           var go =  Instantiate(topicPrefab, parent);
            go.Setup(topics[i]);
            currentTopics.Add(go);
        }
    }

    private void DeInitialize()
    {
        
        for (int i = 0; i < currentTopics.Count; i++)
        {
             Destroy(currentTopics[i].gameObject);
            currentTopics[i] = null;
        }
        currentTopics.Clear();
    }

    public void SetDifficulty(DifficultyUI difficulty)
    {
        SoundManager.Instance.PlaySound("TopicButton");
        GameManager.Instance.currentDifficulty = difficulty.difficulty;
        var loading = SceneManager.Instance.GetSceneByType<LoadingScreen>();
        var tutorial = SceneManager.Instance.GetSceneByType<TutorialScreen>();
        var topic = SceneManager.Instance.GetSceneByType<TopicSelectionUiScreen>();
        var mapuiScreen = SceneManager.Instance.GetSceneByType<MapUiScreen>();
        var map = SceneManager.Instance.GetSceneByType<MapScreen>();

        map.startFromStartingPos = true;
        mapuiScreen.isMapUIShown = true;
        map.respawnAllEnemy = true;

        diffUIController.Hide();
        loading.Show();
        loading.StartCoroutine(loading.StartLoadingCoroutine());
        //loadingOverlay.Show();
        tutorial.Show();
        topic.Hide();
        //loadingOverlay.Hide();
    }
}
