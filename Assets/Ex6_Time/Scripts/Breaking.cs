using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Breaking : MonoBehaviour
{
    [SerializeField] private Text timerCounterText, pin1Text, pin2Text, pin3Text;
    [SerializeField] private GameObject losingPanel, victoryPanel, gamePanel;
    [SerializeField] private GameObject pin1, pin2, pin3;
    [SerializeField] private Button hammerBtn, screwdriverBtn, forkBtn, drillBtn;
    private Vector2 pin1LocalPos, pin2LocalPos, pin3LocalPos;
    private float tmr, endGameTmr;
    private int pin1Position, pin2Position, pin3Position;
    private int[] targetPosition = { 2, 7, 0 };
    private bool victory, loss;


    void Start()
    {
        victory = false;
        loss = false;
        tmr = 300f;
        endGameTmr = 0;
        pin1Position = 5;
        pin2Position = 5;
        pin3Position = 5;

        pin1LocalPos = pin1.transform.localPosition;
        pin2LocalPos = pin2.transform.localPosition;
        pin3LocalPos = pin3.transform.localPosition;

        //Debug.Log(string.Join(", ", targetPosition));
    }

    private void GameOver()
    {
        loss = true;
        StopGame();
    }

    private void StopGame()
    {
        hammerBtn.interactable = false;
        screwdriverBtn.interactable = false;
        forkBtn.interactable = false;
        drillBtn.interactable = false;
    }

    private void VictoryCheck()
    {
        if (pin1Position == targetPosition[0] && pin2Position == targetPosition[1] && pin3Position == targetPosition[2])
        {
            victory = true;
            StopGame();
        }
    }

    private void PinsTextUpdate()
    {
        pin1Text.text = pin1Position.ToString();
        pin2Text.text = pin2Position.ToString();
        pin3Text.text = pin3Position.ToString();
    }

    private void PinsPositionUpdate()
    {
        pin1.transform.localPosition = new Vector3(pin1LocalPos.x, 10f * pin1Position - 70f);
        pin2.transform.localPosition = new Vector3(pin2LocalPos.x, 10f * pin2Position - 70f);
        pin3.transform.localPosition = new Vector3(pin3LocalPos.x, 10f * pin3Position - 70f);
    }

    private void CheckPosRange()
    {
        if (pin1Position < 0)
            pin1Position = 0;
        else if (pin1Position > 10)
            pin1Position = 10;

        if (pin2Position < 0)
            pin2Position = 0;
        else if (pin2Position > 10)
            pin2Position = 10;

        if (pin3Position < 0)
            pin3Position = 0;
        else if (pin3Position > 10)
            pin3Position = 10;
    }

    public void OnHammerClick()
    {
        pin1Position++;
        pin2Position--;

        CheckPosRange();

        PinsTextUpdate();
        PinsPositionUpdate();
        VictoryCheck();
    }

    public void OnScrewdriverClick()
    {
        pin1Position--;
        pin2Position += 2;
        pin3Position--;

        CheckPosRange();

        PinsTextUpdate();
        PinsPositionUpdate();
        VictoryCheck();
    }

    public void OnForkClick()
    {
        pin1Position--;
        pin2Position--;
        pin3Position++;

        CheckPosRange();

        PinsTextUpdate();
        PinsPositionUpdate();
        VictoryCheck();
    }

    public void OnDrillClick()
    {
        pin1Position++;
        pin2Position--;
        pin3Position -= 2;

        CheckPosRange();

        PinsTextUpdate();
        PinsPositionUpdate();
        VictoryCheck();
    }

    public void Restart()
    {
        victory = false;
        loss = false;
        tmr = 300f;
        endGameTmr = 0;
        pin1Position = 5;
        pin2Position = 5;
        pin3Position = 5;

        pin1LocalPos = pin1.transform.localPosition;
        pin2LocalPos = pin2.transform.localPosition;
        pin3LocalPos = pin3.transform.localPosition;

        hammerBtn.interactable = true;
        screwdriverBtn.interactable = true;
        forkBtn.interactable = true;
        drillBtn.interactable = true;

        PinsTextUpdate();
        PinsPositionUpdate();

        victoryPanel.SetActive(false);
        losingPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    void Update()
    {
        if (!victory)
            tmr -= Time.deltaTime;

        if (tmr <= 0)
        {
            GameOver();
            tmr = 0;
        }
        timerCounterText.text = Mathf.Round(tmr).ToString();

        if (victory)
        {
            endGameTmr += Time.deltaTime;
            if (endGameTmr > 1.5f)
            {
                victoryPanel.SetActive(true);
                gamePanel.SetActive(false);
            }
        }

        if (loss)
        {
            endGameTmr += Time.deltaTime;
            if (endGameTmr > 1.5f)
            {
                losingPanel.SetActive(true);
                gamePanel.SetActive(false);
            }
        }
    }
}
