using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialDataUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI explanationText;
    [SerializeField] private TextMeshProUGUI exampleText;//optional
    [SerializeField] private Image exampleSprite;//optional

    private Color currentColor;
    public void Setup(string _explanation,string _example = null,Sprite _exampleSprite = null)
    {
        explanationText.text = _explanation;
        exampleText.text = _example;
        exampleSprite.sprite = _exampleSprite;
        currentColor = exampleSprite.color;

        if (exampleSprite.sprite == null)
        {
            
            currentColor.a = 0;
            exampleSprite.color = currentColor;
        }
        else
        {
            currentColor.a = 1f;
            exampleSprite.color = currentColor;
        }
    }
}
