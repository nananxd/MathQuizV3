using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBattleScreen : BaseScene
{
    [SerializeField] private ResultsUiController results;

    public ResultsUiController Results { get => results; set => results = value; }

    public override void OnShow()
    {
        base.OnShow();
        Initialize();
    }

    public override void OnHide()
    {
        DeInitialize();
        base.OnHide();
    }

    private void Initialize()
    {
        results.ShowPanel();
        SoundManager.Instance.PlaySoundBG("WinSound");
    }

    private void DeInitialize()
    {
        
        results.HidePanel();
        results.Unsetup();
    }
}
