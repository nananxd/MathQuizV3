using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoicesUI : MonoBehaviour, IPointerUpHandler, IBeginDragHandler, IDragHandler, IEndDragHandler,IDropHandler
{
    [SerializeField] private TextMeshProUGUI choiceNameTxt;
    [SerializeField] private Button clickBtn;

    private Image currentImage;

   
    private Choices choice;
    private bool isBeingDrag;

    public string choiceValue;
    public bool isDuplicate;
    public int index;
    public void Setup(Choices dataChoice)
    {
        choice = dataChoice;
        choice.name = dataChoice.name;
        choice.choicesValue = dataChoice.choicesValue;
        choiceNameTxt.text = choice.name;
        choiceValue = choice.choicesValue;

        currentImage = GetComponent<Image>();
        //clickBtn.onClick.AddListener(OnClickChoice);
    }

    public void OnClickChoice()
    {
        SceneManager.Instance.GetSceneByType<BattleScreenUI>().ChoiceUiController.InstantiateSelectedChoices(choice);
        Debug.Log("Click Choices");
        
    }

    

    public void OnPointerUp(PointerEventData eventData)
    {
        //OnClickChoice();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        currentImage.raycastTarget = false;
        if (transform.parent == SceneManager.Instance.GetSceneByType<BattleScreenUI>().ChoiceUiController.canvasParent)
            return;
       

        if (transform.parent == SceneManager.Instance.GetSceneByType<BattleScreenUI>().AnswerPanel.transform)
        {
            index = transform.GetSiblingIndex();
            transform.SetParent(SceneManager.Instance.GetSceneByType<BattleScreenUI>().ChoiceUiController.canvasParent, false);
            SceneManager.Instance.GetSceneByType<BattleScreenUI>().AnswerPanel.RemoveFromList(this);
            
        }
        else
        {
            SceneManager.Instance.GetSceneByType<BattleScreenUI>().ChoiceUiController.SpawnChoices(index);
            transform.SetParent(SceneManager.Instance.GetSceneByType<BattleScreenUI>().ChoiceUiController.canvasParent, false);
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currentImage.raycastTarget = true;
    }

   

    public void OnDrop(PointerEventData eventData)
    {
        var currentData = eventData.pointerDrag.GetComponent<ChoicesUI>();

        if (currentData != null)
        {
            var screen =  SceneManager.Instance.GetSceneByType<BattleScreenUI>();
            screen.AnswerPanel.SetChildren(currentData);
            screen.AnswerPanel.Swap(this,currentData);
        }

        //if (transform.parent == SceneManager.Instance.GetSceneByType<BattleScreenUI>().ChoiceUiController.parent)
        //{
        //    Destroy(currentData.gameObject);
        //}
    }
}
