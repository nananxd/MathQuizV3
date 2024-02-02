using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestController : UIPanel
{
    public const string KILLONEECKEY = "oneKillEc";
    public const string KILLONEAVEKEY = "oneKillAve";
    public const string KILLONEDIFFKEY = "oneKillDiff";

    public const string KILLTWOECKEY = "twoKillEc";
    public const string KILLTWOAVEKEY = "twoKillAve";
    public const string KILLTWODIFFKEY = "twoKillDiff";

    public const string KILLTHREEECKEY = "threeKillEc";
    public const string KILLTHREEAVEKEY = "threeKillAve";
    public const string KILLTHREEDIFFKEY = "threeKillDiff";

    [SerializeField] private TextMeshProUGUI questTxt;
    [SerializeField] private QuestData currentQuest;

    [SerializeField] private int oneKillCountEc,
                                 oneKillCountAve,
                                 oneKillCountDiff,
                                 twoKillCountEc,
                                 twoKillCountAve,
                                 twoKillCountDiff,
                                 threeKillCountEc,
                                 threeKillCountAve,
                                 threeKillCountDiff;

    [SerializeField] private int requireKills;
    [SerializeField] private int currentTopicKillCount;
    private string questTitle;
    private string questDetail;

    public override void Show()
    {
        base.Show();
        UpdateQuestUI();
        CheckQuest();
    }
    public void Setup()
    {
        var quest = QuestDataManager.Instance.GetQuestByTopic(GameManager.Instance.currentTopic);
        currentQuest = quest;
        requireKills = quest.questRequiredKill;
        questTitle = quest.questTitle;
        questDetail = quest.questDetail;
        LoadCurrentKillFromPlayerPrefs();
        UpdateQuestUI();       
        Hide();
    }

    public void Unsetup()
    {
        //currentQuest = null;
        //currentKillCount = 0;
    }

    public void UpdateQuest()
    {
        currentTopicKillCount = GetCurrentKillCount();
        if (currentTopicKillCount <= requireKills)
        {
            //currentKillCountTwoCubes++;
            UpdateKillCount();
            UpdateQuestUI();
            SaveCurrentKillToPlayerPrefs();
            CheckQuest();
            
        }
        
    }

    

    

    public void UpdateQuestUI()
    {
        currentTopicKillCount = GetCurrentKillCount();
        if (currentTopicKillCount <= requireKills)
        {
            questTxt.text = questDetail + " " + "<color=green>" + currentTopicKillCount + "/" + requireKills + "</color>";
        }
        else
        {
            questTxt.text = questDetail + " " + "<color=green>" + requireKills + "/" + requireKills + "</color>";
        }
       
        
    }

    public void CheckQuest()
    {
        currentTopicKillCount = GetCurrentKillCount();
        if (currentTopicKillCount >= requireKills)
        {
            questTxt.text = "<color=green>Quest Completed</color>";
        }
    }

    private void SaveCurrentKillToPlayerPrefs()
    {
        

        if(GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            PlayerPrefs.SetInt(KILLONEECKEY, oneKillCountEc);
        }

        if (GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            PlayerPrefs.SetInt(KILLONEAVEKEY, oneKillCountAve);
        }

        if (GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            PlayerPrefs.SetInt(KILLONEDIFFKEY, oneKillCountDiff);
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            PlayerPrefs.SetInt(KILLTWOECKEY, twoKillCountEc);
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            PlayerPrefs.SetInt(KILLTWOAVEKEY, twoKillCountAve);
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            PlayerPrefs.SetInt(KILLTWODIFFKEY, twoKillCountDiff);
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            PlayerPrefs.SetInt(KILLTHREEECKEY, threeKillCountEc);
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            PlayerPrefs.SetInt(KILLTHREEAVEKEY, threeKillCountAve);
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            PlayerPrefs.SetInt(KILLTHREEDIFFKEY, threeKillCountDiff);
        }

    }

    private void LoadCurrentKillFromPlayerPrefs()
    {
       

        if (GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            oneKillCountEc = PlayerPrefs.GetInt(KILLONEECKEY);
        }

        if (GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            oneKillCountAve = PlayerPrefs.GetInt(KILLONEAVEKEY);
        }

        if (GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            oneKillCountDiff = PlayerPrefs.GetInt(KILLONEDIFFKEY);
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            twoKillCountEc = PlayerPrefs.GetInt(KILLTWOECKEY);
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            twoKillCountAve = PlayerPrefs.GetInt(KILLTWOAVEKEY);
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            twoKillCountDiff = PlayerPrefs.GetInt(KILLTWODIFFKEY);
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            threeKillCountEc = PlayerPrefs.GetInt(KILLTHREEECKEY);
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            threeKillCountAve = PlayerPrefs.GetInt(KILLTHREEAVEKEY);
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            threeKillCountDiff = PlayerPrefs.GetInt(KILLTHREEDIFFKEY);
        }

    }

    public void UpdateKillCount()
    {
       

        if (GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            oneKillCountEc++;
        }

        if (GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            oneKillCountAve++;
        }

        if (GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            oneKillCountDiff++;
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            twoKillCountEc++;
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            twoKillCountAve++;
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            twoKillCountDiff++;
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            threeKillCountEc++;
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            threeKillCountAve++;
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            threeKillCountDiff++;
        }

        SaveCurrentKillToPlayerPrefs();
    }
    public int GetCurrentKillCount()
    {
        int currentKill = 0;
        

        if (GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            currentKill = oneKillCountEc;
        }

        if (GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            currentKill = oneKillCountAve;
        }

        if (GameManager.Instance.currentTopic == Topic.Easy && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            currentKill = oneKillCountDiff;
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            currentKill = twoKillCountEc;
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            currentKill = twoKillCountAve;
        }

        if (GameManager.Instance.currentTopic == Topic.Average && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            currentKill = twoKillCountDiff;
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.E)
        {
            currentKill = threeKillCountEc;
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.A)
        {
            currentKill = threeKillCountAve;
        }

        if (GameManager.Instance.currentTopic == Topic.Difficult && GameManager.Instance.currentDifficulty == Difficulty.D)
        {
            currentKill = threeKillCountDiff;
        }

        return currentKill;
    }
    
}
