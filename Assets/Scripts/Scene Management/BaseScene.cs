using DG.Tweening;
using UnityEngine;

public class BaseScene : MonoBehaviour
{

   
    public bool isUI;

    public string sceneName;
    public GameObject childObject;
    public CanvasGroup childObjectCanvas;
    public bool showAtStart;

    public void Show()
    {
        if (isUI)
        {
            SetCanvasGroup(childObjectCanvas,true);
        }
        else
        {
            childObject.SetActive(true);
        }
        
        OnShow();
    }

    public void Hide()
    {
        if (isUI)
        {
            SetCanvasGroup(childObjectCanvas,false);
        }
        else
        {
            childObject.SetActive(false);
        }
        
        OnHide();
    }

    public virtual void OnShow()
    {
    }

    public virtual void OnHide()
    {
    }

    private void SetCanvasGroup(CanvasGroup canvasGroup,bool isShown)
    {
        if (isShown)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }
    }
}