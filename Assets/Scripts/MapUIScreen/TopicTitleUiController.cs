using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TopicTitleUiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleTopic;
    string difficulty = "";
    public void UpdateTitleText()
    {
       
        switch (GameManager.Instance.currentDifficulty)
        {
            case Difficulty.E:
                difficulty = "<color=green>EASY</color>";
                break;
            case Difficulty.A:
                difficulty = "<color=yellow>AVERAGE</color>";
                break;
            case Difficulty.D:
                difficulty = "<color=red>DIFFICULT</color>";
                break;
           
        }
        titleTopic.text = GameManager.Instance.topicTitle + "\n" + difficulty;
    }
}
