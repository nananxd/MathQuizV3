using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI choiceUiText;
    [SerializeField] private Button currentButton;
    [SerializeField] private UIButton currentUIButton;
    [SerializeField] private Color defaultColor, selectedColor;

    public UIButton CurrentUIButton { get => currentUIButton; set => currentUIButton = value; }
    public Button CurrentButton { get => currentButton; set => currentButton = value; }
   
    public void SetText(string textToPut)
    {
        choiceUiText.text = textToPut;
    }

    public void ClearText()
    {
        choiceUiText.text = "";
    }

    public void ChangeTextColor(bool isSelected)
    {
        if (isSelected)
        {
            choiceUiText.color = selectedColor;
        }
        else
        {
            choiceUiText.color = defaultColor;
        }
    }
}
