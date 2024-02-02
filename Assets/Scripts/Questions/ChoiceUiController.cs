using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChoiceUiController : MonoBehaviour
{
    [SerializeField] private List<ChoiceUI> choicesButton;
    public void SetChoice(List<string> choices)
    {
        for (int i = 0; i < choices.Count; i++)
        {
            choicesButton[i].SetText(choices[i]);
        }
    }

    public void ClearChoices()
    {
        foreach (var item in choicesButton)
        {
            item.ClearText();
        }
    }

    public void DisableOtherChoicesButton(Button currentButton)
    {
        for (int i = 0; i < choicesButton.Count; i++)
        {
            if (choicesButton[i].CurrentButton != currentButton)
            {
                choicesButton[i].CurrentButton.interactable = false;
                choicesButton[i].CurrentUIButton.ResetScale();
                choicesButton[i].CurrentUIButton.OnDeselect();
                choicesButton[i].ChangeTextColor(false);
            }
            else
            {
                choicesButton[i].CurrentButton.interactable = true;
                choicesButton[i].CurrentUIButton.AnimateScale();
                choicesButton[i].CurrentUIButton.OnSelect();
                choicesButton[i].ChangeTextColor(true);
            }
        }
    }

    public void EnableAllChoicesButton()
    {
        foreach (var item in choicesButton)
        {
            item.CurrentButton.interactable = true;
            item.CurrentUIButton.ResetScale();
            item.CurrentUIButton.OnDeselect();
            item.ChangeTextColor(false);
        }
    }
}
