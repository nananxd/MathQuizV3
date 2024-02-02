using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UniRx;


public class TopicUiController : UI_Button
{
    [Header("FOR FUNCTIONALITY")]
    [SerializeField] private Image backgroundVisuals;
    [SerializeField] private Image lockImage;
    [SerializeField] private Image background;
    [SerializeField] private Image coinImage;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private Button topicBtn;


    [Header("FOR OVERRIDE ANIMATION FUNCTIONALITY")]
    [SerializeField] private Mask mask;
    [SerializeField] private RectTransform buttonTransform;

    [SerializeField] private Color normalColor;
    [SerializeField] private Color hoverColor;

    [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite lockSprite;
    [SerializeField] private Sprite coinSprite;

    [SerializeField] private int scoreRequirement;

    private TopicUIData currentUiData;
    private Sprite backgroundImage;
    private Sprite lockBackgroundImage;
    private Sprite normalStateSprite;
    private Vector2 originalTransform;

    public bool isLock = false;
    public bool isScoreRequireMeet;
    public const string BOOLSCOREKEY = "boolScoreKey";
    public void Setup(TopicUIData topicUiData)
    {
        currentUiData = topicUiData;

        title.text = currentUiData.title;
        //description.text = currentUiData.description;
        backgroundImage = currentUiData.backgroundImage;
        lockBackgroundImage = currentUiData.lockBackgroundImage;
        normalStateSprite = currentUiData.normalStateBackground;

        backgroundVisuals.sprite = isLock ? lockBackgroundImage : backgroundImage;
        lockImage.sprite = currentUiData.lockIcon;
        lockImage.gameObject.SetActive(isLock);

        topicBtn.onClick.AddListener(OnClickTopic);
        originalTransform = buttonTransform.anchoredPosition;
        background.sprite = defaultSprite;
        scoreRequirement = currentUiData.scoreRequirement;
        CheckForScore();
    }

    public void OnClickTopic()
    {
        //loading screen
        //setup data for tutorial,map enemy data
        SoundManager.Instance.PlaySound("TopicButton");
        var loading = SceneManager.Instance.GetSceneByName("Loading Screen");
        var loadingOverlay = loading.GetComponent<LoadingScreen>();

        var tutorial = SceneManager.Instance.GetSceneByName("Tutorial Screen");
        var tutorialScreen = tutorial.GetComponent<TutorialScreen>();

        var topic = SceneManager.Instance.GetSceneByName("Topic Ui Screen");
        var topicScreen = topic.GetComponent<TopicSelectionUiScreen>();
        var loading1 = SceneManager.Instance.GetSceneByType<LoadingScreen>();

        GameManager.Instance.topicTitle = currentUiData.title;
        GameManager.Instance.currentTopic = currentUiData.topic;
        var tutorialData = TutorialsDataManager.Instance.GetTutorialDataByTopic(GameManager.Instance.currentTopic);
        var tutorialSlides = TutorialsDataManager.Instance.GetTutorialSlidesByTopic(GameManager.Instance.currentTopic);
        //tutorialScreen.SetTutorialData(tutorialData);
        tutorialScreen.SetTutorialData(tutorialSlides);

        topicScreen.DiffUIController.Show();
       // loading1.Show();
        //loading1.StartCoroutine(loading1.StartLoadingCoroutine());
        //loadingOverlay.Show();
       // tutorialScreen.Show();
        //topicScreen.Hide();
        //loadingOverlay.Hide();
    }

    public override void OnClickAnimate()
    {
        //base.OnClickAnimate();
        if (!isLock)
        {
            mask.enabled = false;

            buttonTransform.DOAnchorPosY(buttonTransform.anchoredPosition.y + 40f, duration).SetEase(easeType);
            backgroundVisuals.transform.DOScale(new Vector3(1.09f, 1.09f, 1.09f), duration).SetEase(easeType);
            backgroundVisuals.sprite = normalStateSprite;
            background.sprite = hoverSprite;
        }
        
    }

    public override void OnDeselectAnimate()
    {
        //base.OnDeselectAnimate();
        if (!isLock)
        {
            buttonTransform.DOAnchorPosY(originalTransform.y, duration).SetEase(easeType);
            backgroundVisuals.transform.DOScale(Vector3.one, duration).SetEase(easeType);
            backgroundVisuals.sprite = backgroundImage;
            background.sprite = defaultSprite;
            mask.enabled = true;
        }
        
    }

    public void CheckForScore()
    {
       
        if (GameManager.Instance.GetTopicScore() >= scoreRequirement)
        {
            

            isLock = false;
            topicBtn.interactable = true;
            background.sprite = defaultSprite;
            lockImage.gameObject.SetActive(false);
            description.text = "";
            coinImage.gameObject.SetActive(false);
        }
        else
        {
            isLock = true;
            topicBtn.interactable = false;
            background.sprite = lockSprite;
            lockImage.gameObject.SetActive(true);
            description.text = scoreRequirement.ToString();
            coinImage.gameObject.SetActive(true);
            coinImage.sprite = coinSprite;
        }
    }

    private void SaveBoolToPlayerPrefs(bool value)
    {
        PlayerPrefs.SetInt(BOOLSCOREKEY,value?1:0 );
    }

    private bool LoadBoolFromPlayerPrefs()
    {
        int currentValue =  PlayerPrefs.GetInt(BOOLSCOREKEY, 0);
        return currentValue == 1;
    }
}
