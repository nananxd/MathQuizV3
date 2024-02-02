using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnswerStepsUIController : MonoBehaviour
{
    [SerializeField] private RectTransform parent;
    [SerializeField] private AnswerUI answerUI;

    [SerializeField] private List<AnswerUI> answerList = new List<AnswerUI>();

    public List<AnswerUI> AnswerList { get => answerList; set => answerList = value; }

    public void PopulateAnswer(string answer)
    {
        var go = Instantiate(answerUI, parent);
        go.Setup(answer);
        go.gameObject.SetActive(true);
        AnswerList.Add(go);
    }

    public void DePopulateAnswer()
    {
        foreach (var answer in answerList)
        {
            Destroy(answer.gameObject);
        }

        answerList.Clear();
    }

    public AnswerUI GetLastAnswer()
    {
        if (answerList.Count == 0)
        {
            return null;
        }
        return answerList[answerList.Count - 1];
    }

    
}
