using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIPanel : MonoBehaviour
{
    [SerializeField] protected Ease easeType;
    [SerializeField] protected float duration;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform panelTransform;

    private Sequence sequence;

    private void Update()
    {
        // for debugging purpose
        if (Input.GetKeyDown(KeyCode.A))
        {
            ShowPanel();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            HidePanel();
        }
    }
    private void Awake()
    {
        sequence = DOTween.Sequence();
    }

    public void ShowPanel()
    {
        Show();
    }

    public void HidePanel()
    {
        Hide();
    }

    public virtual void Show()
    {
        //panelTransform.anchorMin = Vector2.zero;
        //panelTransform.anchorMax = Vector2.one;

        Vector2 newAnchorMin = new Vector2(0,0);
        Vector2 newAnchorMax = new Vector2(1f,1f);

       // DOTween.KillAll();
       
        sequence.Append(panelTransform.DOAnchorMin(newAnchorMin, duration).SetEase(easeType));
        sequence.Join(panelTransform.DOAnchorMax(newAnchorMax, duration).SetEase(easeType));
        sequence.Join(canvasGroup.DOFade(1f, .5f).SetEase(easeType));
        sequence.Play();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;

        SoundManager.Instance.PlaySound("PanelSound");
    }

    public virtual void Hide()
    {
        Vector2 newAnchorMin = new Vector2(-1f, 0);
        Vector2 newAnchorMax = new Vector2(0, 1f);

        //DOTween.KillAll();
       
        sequence.Append(panelTransform.DOAnchorMin(newAnchorMin, duration).SetEase(easeType));
        sequence.Join(panelTransform.DOAnchorMax(newAnchorMax, duration).SetEase(easeType));
        sequence.Join(canvasGroup.DOFade(0, duration).SetEase(easeType));

        sequence.Play();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        SoundManager.Instance.PlaySound("PanelSound");
    }
}
