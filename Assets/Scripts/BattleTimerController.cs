using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleGrade
{
    S,
    A,
    B,
    C,
    D,
    E,
    F
}
public class BattleTimerController : UIPanel
{
    [SerializeField] private TextMeshProUGUI countdownTxt;
    [SerializeField] private float timer;
    [SerializeField] private float timeBattleFinish;
    private bool isTimerRunning;

    private float countdownTimer = 5f;
    private bool isCountdownStart;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCountdown();
        }

        if (isTimerRunning)
        {
            timer += Time.deltaTime;
        }

        if (isCountdownStart)
        {
            Setup();
        }

        StopCountdown();
    }
    public void StartBattleTimer()
    {
        timeBattleFinish = 0;
        isTimerRunning = true;
    }

    public void StopBattleTimer()
    {
        timeBattleFinish = timer;
        timer = 0;
        isTimerRunning = false;
    }

    public float GetTimeFinish()
    {
        return timer;
    }

    public void StartCountdown()
    {
        isCountdownStart = true;
        ShowPanel();
    }

    public void StopCountdown()
    {
        if (isCountdownStart && countdownTimer <= 0)
        {
            isCountdownStart = false;
            countdownTimer = 5f;
            Unsetup();
        }
    }

    private void Setup()
    {
       
        countdownTimer -= Time.deltaTime;
        countdownTxt.text = countdownTimer.ToString("f0");
        if (countdownTimer < 1) countdownTxt.text = "Solve!";
    }

    private void Unsetup()
    {
        HidePanel();
    }

    
}
