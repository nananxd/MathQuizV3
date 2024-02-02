using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI answerTxt;
    [HideInInspector ]public string answer;
    public void Setup(string answer)
    {
        answerTxt.text = answer;
        this.answer = answer;
    }
}
