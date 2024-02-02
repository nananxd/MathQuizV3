using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;

public class TutorialElementController : MonoBehaviour
{
    private List<TutorialElementFader> canvasFader;
    private int currentElement;
    private int totalElements;
    private void OnEnable()
    {
        //Setup();
    }
    public void Setup()
    {
        canvasFader = new List<TutorialElementFader>();
        canvasFader = GetComponentsInChildren<TutorialElementFader>().ToList();
       
        //for (int i = 0; i < canvasFader.Count; i++)
        //{
        //    canvasFader[i].Setup();
        //    canvasFader[i].CanvasFader.DOFade(1f, 1f).SetEase(Ease.OutBack).SetDelay(canvasFader[i].Duration).OnComplete(() => canvasFader[i].isShown = true);
        //}
    }

    public void HideOnStart()
    {
        foreach (var item in canvasFader)
        {
            
            item.Setup();
            item.HideElement();
        }
    }
    public void HideAllElement()
    {
        currentElement = 0;
        foreach (var item in canvasFader)
        {
            item.isShown = false;
            item.Setup();
            item.HideElement();
        }
    }

    public void RevealElement()
    {
        totalElements = canvasFader.Count;
        if (currentElement > totalElements)
        {
            return;
        }
        currentElement++;
        if (currentElement <= totalElements)
        {
            
            canvasFader[currentElement-1].Setup();
            canvasFader[currentElement-1].CanvasFader.DOFade(1f, 1f).SetEase(Ease.OutBack).OnComplete(() => {
                //canvasFader[currentElement-1].isShown = true;
               // Debug.Log("OnCompleteCall");
            });
            canvasFader[currentElement - 1].isShown = true;
        }
       
        
    }

    public bool IsAllElementReveal()
    {
       var check =  canvasFader.All(e => e.isShown);
        //Debug.Log(check);
       return check;
    }
}
