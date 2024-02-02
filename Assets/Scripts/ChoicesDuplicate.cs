using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ChoicesDuplicate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TextMeshProUGUI nameTxt;
    private Choices currentChoice;
    private string choiceValue;
    public bool isDragging = false;

    private void Update()
    {
        //if (isDragging)
        //{
        //    transform.position = Input.mousePosition;
        //}
        
    }

    public void Setup(Choices choice)
    {
        currentChoice = choice;
        currentChoice.name = choice.name;
        currentChoice.choicesValue = choice.choicesValue;

        nameTxt.text = currentChoice.name;
        choiceValue = currentChoice.choicesValue;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position =eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //transform.position = Input.mousePosition;
        isDragging = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
