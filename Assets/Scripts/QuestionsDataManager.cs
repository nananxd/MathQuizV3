using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestionsDataManager : MonoBehaviour
{
    public List<QuestionsData> questions;
    public List<QuestionScriptable> questionsData;
    public List<QuestionScriptable> questionsByDifficulty;
    private static QuestionsDataManager instance = null;

    [SerializeField]private List<int> generatedIndex = new List<int>();
    private List<QuestionScriptable> chosenScriptable = new List<QuestionScriptable>();
    public static QuestionsDataManager Instance
    {
        get
        {
            return instance;
        }
    }

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
    public QuestionsData GetQuestionByTopic(Topic topic)
    {
        for (int i = 0; i < questions.Count; i++)
        {
            if (questions[i].topic == topic)
            {
                return questions[i];
            }
        }

        return null;
    }

    public QuestionScriptable GetQuestionDataByTopic(Topic topic)
    {
        var chosenQuestion = questionsData.Find(x => x.topic == topic);
        if (chosenQuestion != null)
        {
            return chosenQuestion;
        }
        return null;
    }

    public QuestionScriptable GetRandomQuestionDataByTopic(Topic topic,Difficulty diff)
    {
        
        var difficulty = GameManager.Instance.currentDifficulty;
        //var questions = questionsData.FindAll(x => x.topic == topic && x.difficulty == diff);
        var questions = questionsData.Where(q => q.topic == topic && q.difficulty ==diff).ToList();
        questionsByDifficulty = questions;
        int random = Random.Range(0, questions.Count);
        // int random = Random.Range(0, questionsByDifficulty.Count);
        if (generatedIndex.Count >= questions.Count)
        {
            generatedIndex.Clear();
        }
        while (generatedIndex.Contains(random))
        {
            random = Random.Range(0, questions.Count);
        }
        generatedIndex.Add(random);
        #region
        //if (generatedIndex.Count >= questions.Count)
        //{
        //    generatedIndex.Clear();
        //}
        //for (int i = 0; i < generatedIndex.Count; i++)
        //{
        //    int random;
        //    do
        //    {
        //        random = Random.Range(0, questions.Count);
        //    } while (generatedIndex.Contains(random));

        //    generatedIndex.Add(random);
        //    if (questions != null)
        //    {
        //        return questions[random];
        //    }

        //}
        #endregion
        if (questions != null)
        {
            return questions[random];
        }
        //if (questionsByDifficulty.Count != 0)
        //{
        //    return questionsByDifficulty[random];
        //}
        return null;
    }

    public List<QuestionScriptable> GetAllQuestionsDataByTopic(Topic topic)
    {
        var questions = questionsData.FindAll(x => x.topic == topic);
        if (questions != null)
        {
            return questions;
        }
        return null;
    }
}
