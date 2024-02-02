using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScreenUI : BaseScene
{
    [SerializeField] private BattleTimerController battleTimer;
    [SerializeField] private ChoicesUIController choicesUIController;
    [SerializeField] private AnswerPanelUIController answerPanel;
    [SerializeField] private AnswerStepsUIController answerSteps;
    [SerializeField] private QuestionManager questionManager;
    [SerializeField] private PlayerChoicesUI playerChoiceUI;
    [SerializeField] private Button submitAnswer;
    [SerializeField] private Button solveButton;
    [SerializeField] private Button complainButton; // hint
    [SerializeField] private Button runButton; // escape summary
    [SerializeField] private Button doNothingButton;
    [SerializeField] private Button runPanelCloseButton;

    [SerializeField] private StepSummaryController stepSummary;//when run button press
    [SerializeField] private HintUIController hintController;


    public BattleTimerController BattleTimer { get => battleTimer; set => battleTimer = value; }
    public QuestionManager QuestionMangr { get => questionManager; set => questionManager = value; }
    public AnswerPanelUIController AnswerPanel
    {
        get { return answerPanel; }
        set { answerPanel = value; }
    }
    public ChoicesUIController ChoiceUiController
    {
        get { return choicesUIController; }
        set { choicesUIController = value; }
    }

    public AnswerStepsUIController AnswerSteps
    {
        get { return answerSteps; }
        set { answerSteps = value; }
    }
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
        //choicesUIController.Setup();
        submitAnswer.onClick.AddListener(SubmitAndCheckAnswer);

        solveButton.onClick.AddListener(OnSolveClick);
        complainButton.onClick.AddListener(OnComplainClick);
        runButton.onClick.AddListener(OnRunClick);
        doNothingButton.onClick.AddListener(OnDoNothingClick);
        runPanelCloseButton.onClick.AddListener(CloseRunPanel);
        //battleTimer.StartBattleTimer();
        //battleTimer.StartCountdown();
        playerChoiceUI.Show();
        questionManager.SetQuestion();
        
    }

    private void DeInitialize()
    {
        //choicesUIController.Unsetup();
        //answerSteps.DePopulateAnswer();

        submitAnswer.onClick.RemoveListener(SubmitAndCheckAnswer);

        solveButton.onClick.RemoveListener(OnSolveClick);
        complainButton.onClick.RemoveListener(OnComplainClick);
        runButton.onClick.RemoveListener(OnRunClick);
        doNothingButton.onClick.RemoveListener(OnDoNothingClick);
        runPanelCloseButton.onClick.RemoveListener(CloseRunPanel);

        playerChoiceUI.ShowPanel();
        choicesUIController.HidePanel();
        questionManager.HidePanel();
        stepSummary.Unsetup();
        stepSummary.gameObject.SetActive(false);
    }
    public void CheckIfBattleDone()
    {
        var battleResults = SceneManager.Instance.GetSceneByType<EndBattleScreen>();
        var mapUi = SceneManager.Instance.GetSceneByType<MapUiScreen>();
        var battleScreen = SceneManager.Instance.GetSceneByType<BattleScreen>();
        var mapScreen = SceneManager.Instance.GetSceneByType<MapScreen>();

        if (battleScreen.EnemyAvatar.IsDead())
        {

            mapScreen.CurrentEnemyNpc.isKilled = true;

            int scoreGive;
            bool isScoreCap;

            if (GameManager.Instance.GetScoreBaseOnTopic() < GameManager.Instance.GetCurrentMaxScoreCap())
            {
                scoreGive = GameManager.Instance.GiveScore();
                
                mapUi.QuestController.UpdateQuest();
                isScoreCap = false;
            }
            else
            {
                scoreGive = 0;
                isScoreCap = true;
                //GameManager.Instance.ResetScore();
            }
            var timeFinish = battleTimer.GetTimeFinish();
            battleTimer.StopBattleTimer();
            battleResults.Results.Setup("YOU WIN","Congratulations you defeated the enemy !", scoreGive, ProceedToMap,timeFinish,isScoreCap);
            battleResults.Results.ShowPanel();
            battleResults.Show();
            
            
        }
        else if (battleScreen.PlayerAvatar.IsDead())
        {
            var timeFinish = battleTimer.GetTimeFinish();
            battleTimer.StopBattleTimer();
            Debug.Log("ENEMY WIN");
            battleResults.Results.Setup("YOU LOSE"," You have been defeated to the enemy !",0, ProceedToMap, timeFinish);
            battleResults.Results.ShowPanel();
            battleResults.Show();
        }
    }

    private void SubmitAndCheckAnswer()
    {
        var battleResults = SceneManager.Instance.GetSceneByType<EndBattleScreen>();
        var mapUi = SceneManager.Instance.GetSceneByType<MapUiScreen>();
        var battleScreen = SceneManager.Instance.GetSceneByType<BattleScreen>();

        var enemyStoredAnswer = battleScreen.EnemyAvatar.GetAnswer();
        var lastAnswer = answerSteps.GetLastAnswer();
        var timeFinish = battleTimer.GetTimeFinish();
        battleTimer.StopBattleTimer();

        if (lastAnswer.answer == enemyStoredAnswer)
        {
            Debug.Log("Correct answer");
            mapUi.AddScore(1);
            battleResults.Results.Setup("TAMA ANG SAGOT",lastAnswer.answer,1,ProceedToMap, timeFinish);
            battleResults.Results.ShowPanel();
            battleResults.Show();
        }
        else
        {
            battleResults.Results.Setup("MALI ANG SAGOT", lastAnswer.answer, 0, ProceedToMap, timeFinish);
            battleResults.Show();
            Debug.Log("Incorrect answer");
        }


    }

    private void ProceedToMap()
    {
        var battleResults = SceneManager.Instance.GetSceneByType<EndBattleScreen>();
        var battleScreen = SceneManager.Instance.GetSceneByType<BattleScreen>();
        var battleUI = SceneManager.Instance.GetSceneByType<BattleScreenUI>();
        var loading = SceneManager.Instance.GetSceneByType<LoadingScreen>();
        
        
        loading.Show();
        loading.StartCoroutine(loading.StartLoadingCoroutine());
        battleScreen.Hide();
        StartCoroutine(ProceedToMapCoroutine());
        battleResults.Hide();
        Hide();
        battleScreen.isBattleFinished = true;
        
        //loading.Hide();

    }

    private void CloseRunPanel()
    {      
        ProceedToMap();
        StartCoroutine(CloseRunPanelCoroutine());
        SoundManager.Instance.PlaySound("ButtonSound");
    }

    private IEnumerator CloseRunPanelCoroutine()
    {
        yield return new WaitForSeconds(1f);
        var mapScreen = SceneManager.Instance.GetSceneByType<MapScreen>();
        EnemyNpcManager.Instance.RespawnEnemyNpc(mapScreen.CurrentEnemyNpc,false);
    }

    private IEnumerator ProceedToMapCoroutine()
    {
        yield return new WaitForSeconds(.5f);
        var map = SceneManager.Instance.GetSceneByType<MapScreen>();
        var mapUi = SceneManager.Instance.GetSceneByType<MapUiScreen>();

        map.Show();
        mapUi.Show();
    }

    private void OnSolveClick()
    {
        SoundManager.Instance.PlaySound("ButtonSound");
        //choicesUIController.ShowPanel();      
        playerChoiceUI.Hide();
        questionManager.ShowPanel();
        //questionManager.SetQuestion();
    }

    private void OnComplainClick()
    {
        SoundManager.Instance.PlaySound("ButtonSound");
        hintController.Setup();
        Debug.Log("Complain click hint will be given");
    }

    private void OnRunClick()
    {
        SoundManager.Instance.PlaySound("ButtonSound");
        Debug.Log("Run is click give the step by step process to solve the question");
        stepSummary.gameObject.SetActive(true);
        stepSummary.Setup();
    }

    private void OnDoNothingClick()
    {
        SoundManager.Instance.PlaySound("ButtonSound");
        Debug.Log("do nothing click player failed");
        ProceedToMap();
        StartCoroutine(CloseRunPanelCoroutine());
    }
    
}
