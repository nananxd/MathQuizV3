using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class ResultsUiController : UIPanel
{
    [SerializeField] private TextMeshProUGUI summaryText;
    [SerializeField] private TextMeshProUGUI scoreGiveText;
    [SerializeField] private TextMeshProUGUI winOrLoseText;
    [SerializeField] private TextMeshProUGUI timeFinishText;
    [SerializeField] private Button continueBtn;

    [SerializeField] private CanvasGroup noticeText, coinIcon, scoreUi;

    [Header("For animation override")]
    [SerializeField] private List<Transform> animatedElements;
    private string winLoseValue;

    public Action OnClickContinue;
    public void Setup(string winOrLose, string summary, int scoreGive, Action onContinue,float timeFinish,bool isScoreCap = false)
    {
        winLoseValue = winOrLose;
        winOrLoseText.text = winOrLose;
        summaryText.text = summary;
        scoreGiveText.text =scoreGive.ToString();
        timeFinishText.text = timeFinish.ToString();
        OnClickContinue = onContinue;

        if (isScoreCap)
        {
            coinIcon.alpha = 0;
            scoreUi.alpha = 0;
            noticeText.alpha = 1f;
        }
        else
        {
            coinIcon.alpha = 1f;
            scoreUi.alpha = 1f;
            noticeText.alpha = 0;
        }

        continueBtn.onClick.AddListener(OnClickContinueButton);
    }

    public void Setup(string winOrLose, Action onContinue)
    {
        winOrLoseText.text = winOrLose;
        OnClickContinue = onContinue;
        continueBtn.onClick.AddListener(OnClickContinueButton);
    }

    public void Unsetup()
    {
        winOrLoseText.text = "";
        summaryText.text = "";
        scoreGiveText.text = "";

        continueBtn.onClick.RemoveListener(OnClickContinueButton);
    }

    private void OnClickContinueButton()
    {
        
        OnClickContinue?.Invoke();
        StartCoroutine(DelayRespawn());
        
    }

    private IEnumerator DelayRespawn()
    {
        yield return new WaitForSeconds(1f);

        var mapScreen = SceneManager.Instance.GetSceneByType<MapScreen>();
        if (mapScreen.CurrentEnemyNpc != null)
        {
            if (winLoseValue.Replace(" ","") == "YOUWIN")
            {
                EnemyNpcManager.Instance.RespawnEnemyNpc(mapScreen.CurrentEnemyNpc, true) ;
            }
            else
            {
                EnemyNpcManager.Instance.RespawnEnemyNpc(mapScreen.CurrentEnemyNpc, false);
            }
           
        }
    }
    public override void Show()
    {
        base.Show();

        var sequence = DOTween.Sequence();
    }
    public override void Hide()
    {
        var sequence = DOTween.Sequence();
        base.Hide();
    }


}
