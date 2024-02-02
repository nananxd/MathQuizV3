using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StepUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stepTxt,exampleTxt;
    [SerializeField] private Image exampleImage;
    
    public void Setup(string stepDesc,string exampleDesc = null,Sprite exampleSprite = null)
    {
        stepTxt.text = stepDesc;
        exampleTxt.text = exampleDesc;

        if (exampleSprite == null)
        {
            exampleImage.gameObject.SetActive(false);
        }
        else
        {
            exampleImage.gameObject.SetActive(true);
            exampleImage.sprite = exampleSprite;
        }
    }
}
