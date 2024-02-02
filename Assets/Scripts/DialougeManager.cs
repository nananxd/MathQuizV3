using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


[Serializable]
public class Dialouge
{
    public string speaker;
    [TextArea(3,10)] [NonReorderable]
    public List<string> linesToSay;
    public Sprite speakerImage;
    public bool alreadyRead;
    public int scoreRequirement;
    public int scoreLimit;
    public string spriteBase64;


    public Dialouge(string _speaker,List<string>_lineToSay,bool _alreadyRead,int _scoreReq,int _scoreLimit,Sprite _spriteBase64)
    {
        speaker = _speaker;
        linesToSay = _lineToSay;
        alreadyRead = _alreadyRead;
        scoreRequirement = _scoreReq;
        scoreLimit = _scoreLimit;
        spriteBase64 = ConvertSpriteToBase64(_spriteBase64);
    }

    private string ConvertSpriteToBase64(Sprite sprite)
    {
        if (sprite == null)       
            return null;
        
        Texture2D tex = sprite.texture;
        byte[] bytes = tex.EncodeToPNG();
        return Convert.ToBase64String(bytes);
    }
}

public class DialougeManager : BaseScene
{
    //private List<Dialouge> currentTopicDialouges;
    private Dialouge currentDialogue;
    
    [SerializeField] private TextMeshProUGUI speaker;
    [SerializeField] private TextMeshProUGUI dialogueTxt;
    [SerializeField] private Image speakerImageRight;
    [SerializeField] private Image speakerImageLeft;

    [SerializeField] private CanvasGroup panelCanvasGroup;
    [SerializeField] private Button nextButton;

    private int dialogueCounter = 0;

    public Action OnFinishDialogue;
    //get current topic dialouge
    [Header("DOTWEEN Poperties")]
    [SerializeField] private float duration;
    [SerializeField] private Ease easeType;

    public void Setup(Dialouge topicDialogue)
    {
        if (!topicDialogue.alreadyRead)
        {
            currentDialogue = topicDialogue;
        }
                   
    }

    public void SetDialogue(bool isLeft)
    {       
        speaker.text = currentDialogue.speaker;
        ShowDialougeWithSpeaker(isLeft);
        //speakerImageRight.sprite = currentDialogue.speakerImage;
        PopulateDialogue();
             
    }

    private void ShowDialougeWithSpeaker(bool isLeft)
    {
        if (currentDialogue.speakerImage == null)
        {
            speakerImageRight.gameObject.SetActive(false);
        }

        if (isLeft)
        {
            speakerImageRight.gameObject.SetActive(false);
            speakerImageLeft.sprite = currentDialogue.speakerImage;
            
        }
        else
        {
            speakerImageLeft.gameObject.SetActive(false);
            speakerImageRight.sprite = currentDialogue.speakerImage;
            
        }
    }

    public void OnClickNext()
    {
        Debug.Log("Click");
        PopulateDialogue();
        SoundManager.Instance.PlaySound("ButtonSound");
    }

    private void PopulateDialogue()
    {
                 

        if (dialogueCounter < currentDialogue.linesToSay.Count)
        {
            dialogueTxt.text = currentDialogue.linesToSay[dialogueCounter];
            StartCoroutine(AnimateTextCharacter());
            dialogueCounter++;
        }
        else
        {
            dialogueCounter = 0;
            OnFinishDialogue?.Invoke();
            currentDialogue.alreadyRead = true;
            DialogDataManager.Instance.SaveDialogData();
            Hide();
        }
    }

    public override void OnShow()
    {
        base.OnShow();
        speaker.text = currentDialogue.speaker;
        speakerImageRight.sprite = currentDialogue.speakerImage;
    }

    public override void OnHide()
    {
        ResetValues();
        base.OnHide();      
    }

    private void ResetValues()
    {
        speaker.text = string.Empty;
        dialogueTxt.text = string.Empty;
        speakerImageRight.sprite = null;
        speakerImageLeft.sprite = null;
    }

    private IEnumerator AnimateTextCharacter()
    {
        string originalTxt = dialogueTxt.text;
        dialogueTxt.text = "";
        foreach (var c in originalTxt)
        {
            dialogueTxt.text += c;
            dialogueTxt.DOFade(1f, duration).SetEase(easeType);
            yield return new WaitForSeconds(.05f);
        }

       
    }

   
    
}
