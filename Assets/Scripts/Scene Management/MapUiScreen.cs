using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUiScreen : BaseScene
{
    [SerializeField] private int score;
    [SerializeField] private ScoreUiController scoreUiController;
    [SerializeField] private Joystick joystick;
    [SerializeField] private RectTransform joystickHandle;
    [SerializeField] private ConfirmBattleController conFirmBattle;
    [SerializeField] private UIMapController mapController;
    [SerializeField] private QuestController questController;
    [SerializeField] private TopicTitleUiController topicTitle;
    public bool isMapUIShown = false;
    public int Score { get => score; set => score = value; }
    public ConfirmBattleController ConFirmBattle { get => conFirmBattle; set => conFirmBattle = value; }
    public UIMapController MapController { get => mapController; set => mapController = value; }
    public QuestController QuestController { get => questController; set => questController = value; }
    public TopicTitleUiController TopicTitle { get => topicTitle; set => topicTitle = value; }

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
        // scoreUiController.UpdateScore(GameManager.Instance.GetScore());
        scoreUiController.UpdateScore();
        TopicTitle.UpdateTitleText();
        joystick.Input = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
        conFirmBattle.Setup();
        mapController.Setup();
        questController.Setup();
        if (isMapUIShown)
        {
            mapController.Show();
        }
    }

    private void DeInitialize()
    {
        joystick.Input = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
        conFirmBattle.Unsetup();
        questController.Unsetup();
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }
}
