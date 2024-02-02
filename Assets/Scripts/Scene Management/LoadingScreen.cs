using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class LoadingScreen : BaseScene
{
    [SerializeField] private Ease easeType;
    [SerializeField] private float duration;
    [SerializeField] private CanvasGroup canvasGroup;
    private Sequence sequence;
    public override void OnShow()
    {
        base.OnShow();
        //Initialize();
        //StartCoroutine(StartLoadingCoroutine());
    }

    public override void OnHide()
    {
        //DeInitialize();
       // StartCoroutine(StartDeLoadingCoroutine());
        base.OnHide();
    }

    public void Initialize()
    {
        //canvasGroup.DOFade(1f, duration).SetEase(easeType).Play();
        Show();
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = true;
        Debug.Log("Loading is called");
    }

    public void DeInitialize()
    {
        canvasGroup.DOFade(0, duration).SetEase(easeType).Play();
        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;
        Debug.Log("DeInitialize Loading");
    }

   

    public IEnumerator StartLoadingCoroutine()
    {
        yield return new WaitForSeconds(.05f);
        Initialize();
        yield return new WaitForSeconds(1f);
        DeInitialize();
        yield return new WaitForSeconds(.5f);
        //float time = 2f;
        //float currentTime = 0;
        //while (currentTime <= time)
        //{
        //    currentTime += Time.deltaTime;
        //}
    }

    private IEnumerator StartDeLoadingCoroutine()
    {
        yield return new WaitForSeconds(1f);
        DeInitialize();
        //float time = 2f;
        //float currentTime = 0;
        //while (currentTime <= time)
        //{
        //    currentTime += Time.deltaTime;
        //}
    }
}
