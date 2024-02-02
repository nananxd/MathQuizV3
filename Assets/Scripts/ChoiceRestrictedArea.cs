using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class ChoiceRestrictedArea : MonoBehaviour,IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var currentChoice = eventData.pointerDrag.GetComponent<ChoicesUI>();
        if (currentChoice)
        {
            Destroy(currentChoice.gameObject);
        }
    }

   
}
