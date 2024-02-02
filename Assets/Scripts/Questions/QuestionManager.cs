using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class QuestionManager : UIPanel
{
    [SerializeField]List<string> generatedIndex = new List<string>();
    [SerializeField] private TextMeshProUGUI incompleteAnswerTxt;
    [SerializeField] private TextMeshProUGUI questionTxt;
    [SerializeField] private ChoiceUiController choiceUiController;
    [SerializeField] private Button submitButton, clearAnswer;
    QuestionScriptable currentQuestion;
    private List<string> choices;
    private int choicesToDisplayCount = 3;
    public List<string> incompleteAnswers;
    public List<string> incompleteAnswersCopy;
    public string question;
    public string correctAnswers;
    public string submittedAnswer;
   
    public QuestionScriptable CurrentQuestion
    {
        get { return currentQuestion; }
        private set { currentQuestion = value; }
    }

    public override void Show()
    {
        base.Show();
        submitButton.gameObject.SetActive(false);
        clearAnswer.gameObject.SetActive(false);
    }

    public override void Hide()
    {
        base.Hide();
        submitButton.gameObject.SetActive(true);
        clearAnswer.gameObject.SetActive(true);
    }
    public void SetQuestion()//change to setup nad the parent screen will be the one to initialize it
    {
        incompleteAnswerTxt.text = "";
        questionTxt.text = "";
        incompleteAnswers.Clear();
        incompleteAnswersCopy.Clear();
        correctAnswers = string.Empty;
        question = string.Empty;
        submittedAnswer = string.Empty;


        currentQuestion = QuestionsDataManager.Instance.GetRandomQuestionDataByTopic(GameManager.Instance.currentTopic,GameManager.Instance.currentDifficulty);
        incompleteAnswers = new List<string>(currentQuestion.incompleteAnswers);
        incompleteAnswersCopy = new List<string>(incompleteAnswers);
        questionTxt.text = currentQuestion.question;
        correctAnswers = currentQuestion.correctAnswer;

        for (int i = 0; i < incompleteAnswers.Count; i++)
        {
            if (incompleteAnswers[i]=="")
            {
                incompleteAnswers[i] = "( ? )";
            }
            incompleteAnswerTxt.text += incompleteAnswers[i];
            
        }

        InitializeChoices();
        choiceUiController.EnableAllChoicesButton();
        //ShowPanel();
    }


    public void ChooseAnswer(TextMeshProUGUI name)
    {
        submitButton.gameObject.SetActive(true);
        clearAnswer.gameObject.SetActive(true);
        var index = incompleteAnswers.FindIndex(s => s == "( ? )");
        if (index != -1)
        {
            //incompleteAnswers[index] = "("+name.text+")";
            incompleteAnswers[index] =name.text;
        }
       

        incompleteAnswerTxt.text = string.Empty;
        for (int i = 0; i < incompleteAnswers.Count; i++)
        {
            incompleteAnswerTxt.text += incompleteAnswers[i];
            submittedAnswer = incompleteAnswerTxt.text;
        }

        SoundManager.Instance.PlaySound("ButtonSound");
    }

    public void SubmitAnswer()
    {
        SoundManager.Instance.PlaySound("ButtonSound");
        var battleScreen = SceneManager.Instance.GetSceneByType<BattleScreen>();
        var battleScreenUI = SceneManager.Instance.GetSceneByType<BattleScreenUI>();
        if (CheckAnswer())
        {
            Debug.Log("CORRECT ANSWER- attacking enemy");//damage enemy health
            battleScreen.PlayerAvatar.SetEnemyReference(battleScreen.EnemyAvatar);
            StartCoroutine(ActivatePlayerVcam(battleScreen.PlayerAvatar.transform));
            StartCoroutine(DelayAttack());                      
            
            
        }
        else
        {
            Debug.Log("WRONG ANSWER - enemy will attack player");//damage player health
            battleScreen.EnemyAvatar.SetPlayerReference(battleScreen.PlayerAvatar);
            StartCoroutine(ActivateEnemyVcam(battleScreen.EnemyAvatar.transform));
            StartCoroutine(DelayEnemyAttack());
            
            
        }
        //battleScreenUI.CheckIfBattleDone();
        StartCoroutine(CheckIfBattleDoneCoroutine(battleScreenUI));
        //check player hp or enemy hp
        //incompleteAnswerTxt.text = "";
        questionTxt.text = "";
        HidePanel();
        //SetQuestion();
       
        //CheckIfGameEnd();
    }

    private IEnumerator DelayAttack()
    {     
        yield return new WaitForSeconds(1.5f);
        var battleScreen = SceneManager.Instance.GetSceneByType<BattleScreen>();
        var battleScreenUI = SceneManager.Instance.GetSceneByType<BattleScreenUI>();
        battleScreen.PlayerAvatar.MoveToEnemy(battleScreen.EnemyAvatar.transform);
        
    }

    private IEnumerator DelayEnemyAttack()
    {
        
        yield return new WaitForSeconds(1.5f);
        var battleScreen = SceneManager.Instance.GetSceneByType<BattleScreen>();
        var battleScreenUI = SceneManager.Instance.GetSceneByType<BattleScreenUI>();
        battleScreen.EnemyAvatar.MoveToEnemy(battleScreen.PlayerAvatar.transform);
    }
    private IEnumerator ActivatePlayerVcam(Transform player)
    {
        GameManager.Instance.playerBattleVcam.gameObject.SetActive(true);
        GameManager.Instance.playerBattleVcam.LookAt = player;
        yield return new WaitForSeconds(.5f);
        GameManager.Instance.playerBattleVcam.gameObject.SetActive(false);
        GameManager.Instance.playerBattleVcam.LookAt = null;
    }

    private IEnumerator ActivateEnemyVcam(Transform enemy)
    {
        GameManager.Instance.enemyBattleVcam.gameObject.SetActive(true);
        GameManager.Instance.enemyBattleVcam.LookAt = enemy;
        yield return new WaitForSeconds(.5f);
        GameManager.Instance.enemyBattleVcam.gameObject.SetActive(false);
        GameManager.Instance.enemyBattleVcam.LookAt = null;
    }

    public void CheckIfBattleDone(BattleScreen battleScreen)
    {
        var resultScreen = SceneManager.Instance.GetSceneByType<EndBattleScreen>();
        if (battleScreen.EnemyAvatar.IsDead())
        {
            Debug.Log("PLAYER WIN");
            resultScreen.Show();
        }
        else if (battleScreen.PlayerAvatar.IsDead())
        {
            Debug.Log("ENEMY WIN");
            resultScreen.Show();
        }

        
    }

    public void CheckIfGameEnd()
    {
        var battleScreen = SceneManager.Instance.GetSceneByType<BattleScreen>();
        if (!battleScreen.PlayerAvatar.IsDead() || !battleScreen.EnemyAvatar.IsDead())
        {
            ShowPanel();
        }
    }
    public bool CheckAnswer()
    {
        return submittedAnswer.Replace(" ","") == correctAnswers.Replace(" ","");
    }

    public void ClearAnswer()
    {
        submitButton.gameObject.SetActive(false);
        clearAnswer.gameObject.SetActive(false);

        incompleteAnswerTxt.text = string.Empty;
        incompleteAnswers = new List<string>(incompleteAnswersCopy);
        for (int i = 0; i < incompleteAnswers.Count; i++)
        {
            if (incompleteAnswers[i] == "")
            {
                incompleteAnswers[i] = "( ? )";
            }
            incompleteAnswerTxt.text += incompleteAnswers[i];
            submittedAnswer = "";
        }
       
        choiceUiController.EnableAllChoicesButton();
        SoundManager.Instance.PlaySound("ButtonSound");
    }

    private void CheckIncompleteAnswers()
    {
        for (int i = 0; i < incompleteAnswers.Count; i++)
        {
            if (incompleteAnswers[i] == "")
            {
                incompleteAnswers[i] = "?";
            }
            incompleteAnswerTxt.text += incompleteAnswers[i];
        }
    }

    public void InitializeChoices()
    {
        ClearGeneratedAnswer();
        choices = new List<string>();
       
       //var questionList = QuestionsDataManager.Instance.GetAllQuestionsDataByTopic(GameManager.Instance.currentTopic);
       // //chose random answer from other questions data
       // for (int i = 0; i < choicesToDisplayCount; i++)
       // {
       //     int randomNum = Random.Range(0,questionList.Count);
       //     while (generatedIndex.Contains(questionList[randomNum].missingAnswer[0]) || questionList[randomNum].missingAnswer[0] == currentQuestion.missingAnswer[0])
       //     {
       //         randomNum = Random.Range(0, questionList.Count);
       //     }
       //     generatedIndex.Add(questionList[randomNum].missingAnswer[0]);
       //     var questionChose = questionList[randomNum];
       //     int randomAnswer = Random.Range(0,questionChose.missingAnswer.Count);
       //     choices.Add(questionChose.missingAnswer[randomAnswer]);
           
       // }

        for (int i = 0; i < currentQuestion.questionsStepsToSolve.Count; i++)
        {
            choices.Add(currentQuestion.questionsStepsToSolve[i].explanation);
        }

        for (int j = 0; j < currentQuestion.missingAnswer.Count; j++)
        {
            choices.Add(currentQuestion.missingAnswer[j]);
        }

        ShuffleChoices(choices);
        choiceUiController.SetChoice(choices);
    }

    private void ShuffleChoices<T>(List<T> choicesList)
    {
        for (int i = 0; i < choicesList.Count; i++)
        {
            T temp = choicesList[i];
            int rand = Random.Range(0, choicesList.Count);
            choicesList[i] = choicesList[rand];
            choicesList[rand] = temp;
        }
    }

    public void ClearGeneratedAnswer()
    {
        generatedIndex.Clear();
    }
    private IEnumerator CheckIfBattleDoneCoroutine(BattleScreenUI battleScreenUI)
    {
        yield return new WaitForSeconds(3f);
        battleScreenUI.CheckIfBattleDone();
    }
}
