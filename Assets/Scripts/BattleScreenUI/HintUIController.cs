using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintUIController : UIPanel
{
    [SerializeField] private HintData hintData;
    [SerializeField] private TextMeshProUGUI explanationTxt;
    [SerializeField] private Button closeButton;
    public void Setup()
    {
        var topic = GameManager.Instance.currentTopic;
        hintData = HintDataManager.Instance.GetHintByTopic(topic);
        var chosen = Random.Range(0, hintData.hints.Count);
        var hint = hintData.hints[chosen];
        explanationTxt.text = hint.explanation;
        closeButton.onClick.AddListener(OnClose);
        Show();
    }

    

    public void Unsetup()
    {
        explanationTxt.text = "";
    }

    private void OnClose()
    {
        SoundManager.Instance.PlaySound("ButtonSound");
        Hide();
    }
}
