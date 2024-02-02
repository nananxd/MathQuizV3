using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TutorialElementFader : MonoBehaviour
{

    [SerializeField] private float duration;
    private CanvasGroup canvasFader;
    public bool isShown = false;

    
    public CanvasGroup CanvasFader { get => canvasFader; set => canvasFader = value; }
    public float Duration { get => duration; set => duration = value; }

    public void Setup()
    {
        canvasFader = GetComponent<CanvasGroup>();
       
    }

    public void HideElement()
    {
        canvasFader.alpha = 0;
        //canvasFader.blocksRaycasts = false;
        //canvasFader.interactable = false;
    }

    public void ShowElement()
    {
        canvasFader.alpha = 1f;
        canvasFader.blocksRaycasts = true;
        canvasFader.interactable = true;
    }
}
