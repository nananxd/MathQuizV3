using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UIButton : MonoBehaviour
{
    [SerializeField] private Sprite selectedSprite, defaultSprite;
    [SerializeField] private Button buttonImage;
    [SerializeField] private Color defaultColor, selectedColor;

    [Header("DG Tweening properties")]
    [SerializeField] private Ease easeType;
    [SerializeField] private float duration;
    [SerializeField] private Vector3 toScale;
    private Vector3 fromScale;
    private ColorBlock colorBlock;

    private void Start()
    {
        fromScale = buttonImage.transform.localScale;
    }
    public void OnDeselect( )
    {
        buttonImage.image.sprite = defaultSprite;
        buttonImage.image.color = defaultColor;
        ResetScale();
    }

    public void OnSelect( )
    {
        buttonImage.image.sprite = selectedSprite;
        buttonImage.image.color = selectedColor;
        AnimateScale();
    }

    public void AnimateScale()
    {
        buttonImage.transform.DOScale(toScale, duration).SetEase(easeType);
    }

    public void ResetScale()
    {
        buttonImage.transform.DOScale(fromScale, duration).SetEase(easeType);
    }
}
