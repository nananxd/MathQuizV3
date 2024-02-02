using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UISlider : MonoBehaviour
{
    [SerializeField] private Ease easeType;
    [SerializeField] private float duration;
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderImage;
    [SerializeField] private Color neutralColor;
    [SerializeField] private Color warningColor;
    [SerializeField] private Color dangerColor;
    float currentHealth = 100;
    public void LerpSlider(float givenValue)
    {
        slider.DOValue(givenValue,duration).SetEase(easeType).SetDelay(.3f);
        var percent = currentHealth * CurrentSliderValue() *100;
        Debug.Log("PERCENT:" + percent);
        if (percent >= 80)
        {
            sliderImage.DOColor(neutralColor, duration).SetEase(easeType);
        }
        else if (percent >= 50)
        {
            sliderImage.DOColor(warningColor, duration).SetEase(easeType);
        }
        else
        {
            sliderImage.DOColor(dangerColor, duration).SetEase(easeType);
        }
    }

    public void SetUp(int maxValue,bool isWholeNumber)
    {
        slider.wholeNumbers = isWholeNumber;
        slider.maxValue = maxValue;
        slider.value = slider.maxValue;
    }

    public float CurrentSliderValue()
    {
        return slider.value;
    }

    public void TestSlider()
    {
       
        currentHealth -= 15;
        LerpSlider(currentHealth);
    }
}
