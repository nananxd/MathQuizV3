using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class AnswerPanelUIController : MonoBehaviour , IDropHandler
{
    public string currentAnswer;
    [SerializeField] private List<ChoicesUI> currentChoices = new List<ChoicesUI>();
    [SerializeField] private Button answerButton;
    private ChoicesUI selectedAns;

    public void OnDrop(PointerEventData eventData)
    {
        var ans = eventData.pointerDrag.GetComponent<ChoicesUI>();
        if (ans != null)
        {
            SetChildren(ans);
        }
    }

   

    public void SetChildren(ChoicesUI answer)
    {
        answer.transform.SetParent(transform);
        currentChoices.Add(answer);
        //answer.index = answer.transform.GetSiblingIndex();
    }

    public void Swap(ChoicesUI choice1, ChoicesUI choice2)
    {

        var index1 = choice1.transform.GetSiblingIndex();
        var index2 = choice2.index;

        choice1.transform.SetSiblingIndex(index2);
        choice2.transform.SetSiblingIndex(index1);

        int tempIndex1 = 0;
        int tempIndex2 = 0;

        for (int i = 0; i < currentChoices.Count; i++)
        {
            if (currentChoices[i].choiceValue == choice1.choiceValue)
            {
                tempIndex1 = i;
            }
            else if (currentChoices[i].choiceValue == choice2.choiceValue)
            {
                tempIndex2 = i;
            }
        }

        var temp = currentChoices[tempIndex1];
        currentChoices[tempIndex1] = currentChoices[tempIndex2];
        currentChoices[tempIndex2] = temp;
    }

    public void RemoveFromList(ChoicesUI choiceToRemove)
    {
        currentChoices.Remove(choiceToRemove);
        
    }

    public void AddAnswer()
    {
        currentAnswer = string.Empty;
        for (int i = 0; i < currentChoices.Count; i++)
        {
            currentAnswer += currentChoices[i].name;
        }

        var screen = SceneManager.Instance.GetSceneByType<BattleScreenUI>();
        screen.AnswerSteps.PopulateAnswer(currentAnswer);
        ClearAnswerField();
    }

    private void ClearAnswerField()
    {
        foreach (var choice in currentChoices)
        {
            Destroy(choice.gameObject);
        }
        currentChoices.Clear();
    }

   
    
}
   

    

