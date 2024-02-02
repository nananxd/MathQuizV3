using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreUiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt;
    public void UpdateScore(int addedScore)
    {
        if (addedScore == 0)
        {
            scoreTxt.text = "0";
        }
        else
        {
            scoreTxt.text = addedScore.ToString();
        }
       
    }

    public void UpdateScore()
    {
        if (GameManager.Instance.GetScoreBaseOnTopic() == 0)
        {
            scoreTxt.text = "0" +"/" + GameManager.Instance.GetCurrentMaxScoreCap();
        }
        else
        {
            scoreTxt.text = GameManager.Instance.GetScoreBaseOnTopic().ToString()+"/"+GameManager.Instance.GetCurrentMaxScoreCap();
        }
    }
}
