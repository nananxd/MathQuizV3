using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Button : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [Header("FOR ANIMATION")]
    [SerializeField] protected Ease easeType;
    [SerializeField] protected float duration;
    

    private Vector3 defaultScale;
    private Vector3 hoverScale;
   

    private void Awake()
    {
        defaultScale = transform.localScale;
        hoverScale = new Vector3(1.1f, 1.1f, 1.1f);
    }

    public virtual void OnClickAnimate() // or on hover
    {
       
        transform.DOScale(hoverScale, duration).SetEase(easeType);
    }

    public virtual void OnDeselectAnimate()
    {
        transform.DOScale(defaultScale, duration).SetEase(easeType);
       
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnClickAnimate();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnDeselectAnimate();
    }
}