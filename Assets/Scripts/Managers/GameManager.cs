using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public TopicData data;
    public DialougeManager dialougeManager;
    public Topic currentTopic;
    public Difficulty currentDifficulty;
    public string topicTitle;
    public int score,topicScore;
    public CinemachineVirtualCamera mapScreenVcam, battleScreenVcam,playerBattleVcam,enemyBattleVcam;
    public int currentLevelMaxScoreCap;
    private const string SCOREKEY = "ScoreKey";
    private const string TOPICKSCOREKEY = "TopicScoreKey";
    //playerprefs keys
    private const string LESSONEONEEASYKEY ="oneEcKey";
    private const string LESSONONEAVEKEY ="oneAveKey";
    private const string LESSONONEDIFFKEY ="oneDiffKey";

    private const string LESSONTWOEASYKEY ="twoEcKey";
    private const string LESSONTWOAVEKEY ="twoAveKey";
    private const string LESSONTWODIFFKEY ="twoDiffKey";

    private const string LESSONTHREEEASYKEY ="threeEcKey";
    private const string LESSONTHREEAVEKEY ="threeAveKey";
    private const string LESSONTHREEDIFFKEY ="threeDiffKey";
    //

    //Individual Score Base on Topic and Difficulty
    public int oneEcScore;
    public int oneAveScore;
    public int oneDiffScore;

    public int twoEcScore;
    public int twoAveScore;
    public int twoDiffScore;

    public int threeEcScore;
    public int threeAveScore;
    public int threeDiffScore;
    //

    private int lessonOneMaxScore = 10;
    private int lessonTwoMaxScore = 20;
    private int lessonThreeMaxScore = 30;
    private static GameManager instance = null;
    private int rand;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public int LessonOneMaxScore { get => lessonOneMaxScore; private set => lessonOneMaxScore = value; }
    public int LessonTwoMaxScore { get => lessonTwoMaxScore; private set => lessonTwoMaxScore = value; }
    public int LessonThreeMaxScore { get => lessonThreeMaxScore; private set => lessonThreeMaxScore = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
        //initiate 1st topic
        //initialize 1st topic tutorial and examples
        //initialize dialogue for 1st topic
        //initialize 1st scene
       // dialougeManager.Setup(data.dialouges);
        //dialougeManager.SetDialogue();
        SceneManager.Instance.Initialize();
       // var scene = SceneManager.Instance.GetSceneByName("");
    }

    public void SetVirtualCamera(bool isMapVcam)
    {
        if (isMapVcam)
        {
            mapScreenVcam.gameObject.SetActive(true);
            battleScreenVcam.gameObject.SetActive(false);
        }
        else
        {
            mapScreenVcam.gameObject.SetActive(false);
            battleScreenVcam.gameObject.SetActive(true);
        }
    }

    public void ShakeCamera()
    {
        var battleVcam = battleScreenVcam.gameObject.GetComponent<CameraShake>();
        if (battleVcam != null)
        {

            battleVcam.ShakeCamera();
        }
    }

    public void ResetScore()
    {
        PlayerPrefs.SetInt(SCOREKEY,0);
    }
    public int GiveScore()
    {
        #region
        //switch (currentDifficulty)
        //{
        //    case Difficulty.E:
        //        rand = 1;
        //        break;
        //    case Difficulty.A:
        //        rand = 2;
        //        break;
        //    case Difficulty.D:
        //        rand = 3;
        //        break;

        //}

        ////var rand = Random.Range(5, 15);
        //score += rand;
        //topicScore = score;
        //SetScore();
        #endregion
        GiveScoreBaseOnTopicAndDifficulty();
        return rand;
    }

    public void GiveScoreBaseOnTopicAndDifficulty()
    {
        int scoreGiven = 0;
        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.E)
        {
            scoreGiven = 1;
            oneEcScore += scoreGiven;
            rand = scoreGiven;
        }

        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.A)
        {
            scoreGiven = 2;
            oneAveScore += scoreGiven;
            rand = scoreGiven;
        }

        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.D)
        {
            scoreGiven = 3;
            oneDiffScore += scoreGiven;
            rand = scoreGiven;
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.E)
        {
            scoreGiven = 1;
            twoEcScore += scoreGiven;
            rand = scoreGiven;
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.A)
        {
            scoreGiven = 2;
            twoAveScore += scoreGiven;
            rand = scoreGiven;
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.D)
        {
            scoreGiven = 3;
            twoDiffScore += scoreGiven;
            rand = scoreGiven;
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.E)
        {
            scoreGiven = 1;
            threeEcScore += scoreGiven;
            rand = scoreGiven;
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.A)
        {
            scoreGiven = 2;
            threeAveScore += scoreGiven;
            rand = scoreGiven;
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.D)
        {
            scoreGiven = 3;
            threeDiffScore += scoreGiven;
            rand = scoreGiven;
        }
        score += rand;
        topicScore = score;
        SetScore();
        SaveCurrentTopicAndDifficultyScore();
    }

    public int GetScoreBaseOnTopic()
    {
       var currentScore =  LoadCurrentTopicAndDifficultyScore();
       return currentScore;
    }

    public void SetScore()
    {
        PlayerPrefs.SetInt(SCOREKEY, score);
        PlayerPrefs.SetInt(TOPICKSCOREKEY, topicScore);
    }

    public int GetScore()
    {
        score = PlayerPrefs.GetInt(SCOREKEY);
       
        return score;
    }

    public int GetTopicScore()
    {
        topicScore = PlayerPrefs.GetInt(TOPICKSCOREKEY);
        return topicScore;
    }

    public int GetCurrentMaxScoreCap()
    {
        switch (currentDifficulty)
        {
            case Difficulty.E:
                currentLevelMaxScoreCap = lessonOneMaxScore;
                break;
            case Difficulty.A:
                currentLevelMaxScoreCap = lessonTwoMaxScore;
                break;
            case Difficulty.D:
                currentLevelMaxScoreCap = lessonThreeMaxScore;
                break;
           
        }
        return currentLevelMaxScoreCap;
    }

    public int CurrentMaxScore()
    {
        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.E)
        {
            currentLevelMaxScoreCap = lessonOneMaxScore;
        }

        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.A)
        {
            currentLevelMaxScoreCap = lessonOneMaxScore * 2;
        }

        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.D)
        {
            currentLevelMaxScoreCap = lessonOneMaxScore * 3;
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.E)
        {
            currentLevelMaxScoreCap = lessonOneMaxScore;
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.A)
        {
            currentLevelMaxScoreCap = lessonOneMaxScore * 2;
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.D)
        {
            currentLevelMaxScoreCap = lessonOneMaxScore * 3;
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.E)
        {
            currentLevelMaxScoreCap = lessonOneMaxScore;
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.A)
        {
            currentLevelMaxScoreCap = lessonOneMaxScore * 2;
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.D)
        {
            currentLevelMaxScoreCap = lessonOneMaxScore * 3;
        }

        return currentLevelMaxScoreCap;
    }

    public int LoadCurrentTopicAndDifficultyScore()
    {
        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.E)
        {
            oneEcScore =  PlayerPrefs.GetInt(LESSONEONEEASYKEY);
            return oneEcScore;
        }

        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.A)
        {
            oneAveScore=  PlayerPrefs.GetInt(LESSONONEAVEKEY);
            return oneAveScore;
        }

        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.D)
        {
            oneDiffScore = PlayerPrefs.GetInt(LESSONONEDIFFKEY);
            return oneDiffScore;
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.E)
        {
            twoEcScore = PlayerPrefs.GetInt(LESSONTWOEASYKEY);
            return twoEcScore;
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.A)
        {
            twoAveScore = PlayerPrefs.GetInt(LESSONTWOAVEKEY);
            return twoAveScore;
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.D)
        {
            twoDiffScore =  PlayerPrefs.GetInt(LESSONTWODIFFKEY);
            return twoDiffScore;
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.E)
        {
            threeEcScore = PlayerPrefs.GetInt(LESSONTHREEEASYKEY);
            return threeEcScore;
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.A)
        {
            threeAveScore = PlayerPrefs.GetInt(LESSONTHREEAVEKEY);
            return threeAveScore;
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.D)
        {
            threeDiffScore = PlayerPrefs.GetInt(LESSONTHREEDIFFKEY);
            return threeDiffScore;
        }

        return 0;
    }
    private void SaveCurrentTopicAndDifficultyScore()
    {
        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.E)
        {
            PlayerPrefs.SetInt(LESSONEONEEASYKEY, oneEcScore);
        }

        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.A)
        {
            PlayerPrefs.SetInt(LESSONONEAVEKEY, oneAveScore);
        }

        if (currentTopic == Topic.Easy && currentDifficulty == Difficulty.D)
        {
            PlayerPrefs.SetInt(LESSONONEDIFFKEY, oneDiffScore);
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.E)
        {
            PlayerPrefs.SetInt(LESSONTWOEASYKEY, twoEcScore);
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.A)
        {
            PlayerPrefs.SetInt(LESSONTWOAVEKEY, twoAveScore);
        }

        if (currentTopic == Topic.Average && currentDifficulty == Difficulty.D)
        {
            PlayerPrefs.SetInt(LESSONTWODIFFKEY, twoDiffScore);
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.E)
        {
            PlayerPrefs.SetInt(LESSONTHREEEASYKEY, threeEcScore);
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.A)
        {
            PlayerPrefs.SetInt(LESSONTHREEAVEKEY, threeAveScore);
        }

        if (currentTopic == Topic.Difficult && currentDifficulty == Difficulty.D)
        {
            PlayerPrefs.SetInt(LESSONTHREEDIFFKEY, threeDiffScore);
        }
    }


}

public enum Topic
{
    Easy,
    Average,
    Difficult   
}

public enum Difficulty
{
    E,
    A,
    D
}
