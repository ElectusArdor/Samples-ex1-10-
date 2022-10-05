using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startGamePanel, pausePanel, warningText, gameOverPanel, victoryPanel;
    [SerializeField] private Image raidTimerFace, eatTimerFace, harvestTimerFace, trainWarriorTimerFace, trainPeasantTimerFace;
    [SerializeField] private Image trainWarriorBtnHighlight, trainPeasantBtnHighlight;
    [SerializeField] private Image pauseBtnImg, soundBtnImg;
    [SerializeField] private Sprite pauseBtnSprite_On, pauseBtnSprite_Off, soundBtnSprite_On, soundBtnSprite_Off;
    [SerializeField] private Button trainWarriorBtn, trainPeasantBtn, pauseBtn, soundBtn, increaseSpeedBtn;
    [SerializeField] private Text raidCountText, eatCountText, harvestCountText, warriorCountText, peasantCountText, wheatCountText, wheatTargetCountText, gameOverStatisticText, victoryStatisticText;
    [SerializeField] private Text increaseSpeedBtnText;
    [SerializeField] private AudioSource music, victoryMusic, defeatMusic, onClickSound, harvestSound, eatSound, raidSound, peasantSound, warriorSound;
    [SerializeField] private float trainWarriorTime, trainPeasantTime, raidTime, eatTime, harvestTime, raidCounter;
    [SerializeField] private int targetWheatCount, peasantCost, warriorCost;
    [SerializeField] private int peasantProduction, warriorVoracity;
    private Color trainWarriorBtnHighlightColor, trainPeasantBtnHighlightColor;
    private float trainWarriorTmr, trainPeasantTmr, raidTmr, eatTmr, harvestTmr;
    private int raidCount, eatCount, harvestCount, warriorCount, peasantCount, wheatCount, warriorTrained, warriorsLost, wheatHarvested, raidsReflected, eaten;
    private bool sound, pause, increasedSpeed;

    void Start()
    {
        sound = true;

        trainWarriorBtnHighlightColor = trainWarriorBtnHighlight.color;
        trainPeasantBtnHighlightColor = trainPeasantBtnHighlight.color;

        raidCount = 0;
        warriorCount = 0;
        peasantCount = 3;
        wheatCount = 10;
        eatCount = warriorVoracity * warriorCount;
        harvestCount = peasantProduction * peasantCount;

        trainWarriorTmr = -1000f;
        trainPeasantTmr = -1000f;
        raidTmr = raidTime;
        eatTmr = eatTime;
        harvestTmr = harvestTime;

        wheatTargetCountText.text = "/ " + targetWheatCount.ToString();
        UpdateText();

        Time.timeScale = 0;
        pauseBtn.interactable = false;
        increaseSpeedBtn.interactable = false;
    }

    public void OnClickSound()
    {
        if (sound)
            onClickSound.Play();
    }

    public void OnStartClick()
    {
        Time.timeScale = 1;
        pauseBtn.interactable = true;
        increaseSpeedBtn.interactable = true;

        startGamePanel.SetActive(false);
    }

    public void OnMuteClick()
    {
        sound = !sound;

        if (sound)
        {
            soundBtnImg.sprite = soundBtnSprite_On;
            music.Play();
        }
        else
        {
            soundBtnImg.sprite = soundBtnSprite_Off;
            music.Stop();
        }
    }

    public void OnPauseClick()
    {
        pause = !pause;

        if (pause)
        {
            Time.timeScale = 0;
            pauseBtnImg.sprite = pauseBtnSprite_On;
            pausePanel.SetActive(true);

            increaseSpeedBtn.interactable = false;

            music.Stop();
        }
        else
        {
            Time.timeScale = 1;
            pauseBtnImg.sprite = pauseBtnSprite_Off;
            pausePanel.SetActive(false);

            increaseSpeedBtn.interactable = true;
            music.Play();
        }
    }

    public void OnSpeedClick()
    {
        increasedSpeed = !increasedSpeed;

        if (increasedSpeed)
        {
            Time.timeScale = 5;
            increaseSpeedBtnText.text = "x5";
        }
        else
        {
            Time.timeScale = 1;
            increaseSpeedBtnText.text = "x1";
        }
    }

    private void UpdateText()
    {
        raidCountText.text = raidCount.ToString();
        eatCountText.text = eatCount.ToString();
        harvestCountText.text = harvestCount.ToString();
        warriorCountText.text = warriorCount.ToString();
        peasantCountText.text = peasantCount.ToString();
        wheatCountText.text = wheatCount.ToString();
    }

    private void TimersCounter()
    {
        trainWarriorTmr -= Time.deltaTime;
        trainPeasantTmr -= Time.deltaTime;
        raidTmr -= Time.deltaTime;
        eatTmr -= Time.deltaTime;
        harvestTmr -= Time.deltaTime;

        raidTimerFace.fillAmount = (1 - raidTmr / raidTime);
        eatTimerFace.fillAmount = (1 - eatTmr / eatTime);
        harvestTimerFace.fillAmount = (1 - harvestTmr / harvestTime);
        trainWarriorTimerFace.fillAmount = (1 - trainWarriorTmr / trainWarriorTime);
        trainPeasantTimerFace.fillAmount = (1 - trainPeasantTmr / trainPeasantTime);
    }

    private void RaidTimerTick()
    {
        if (sound)
            raidSound.Play();

        warriorCount -= raidCount;

        if (warriorCount < 0)
        {
            warriorsLost += raidCount + warriorCount;
            GameOver();
        }
        else
            warriorsLost += raidCount;

        raidCount = Mathf.RoundToInt(raidCounter);
        if (raidCount < 0)
            raidCount = 0;
        raidCounter += 0.6f;

        raidTmr = raidTime;

        UpdateText();
        raidsReflected++;
    }

    private void EatTimerTick()
    {
        if (sound)
            eatSound.Play();

        wheatCount -= eatCount;
        if (wheatCount < 0)
        {
            wheatCount = 0;
            warriorCount--;
            eatCount = warriorVoracity * warriorCount;

            WarningMessage("Воины умирают с голоду!");
        }

        eatTmr = eatTime;

        UpdateText();
        eaten += eatCount;
    }

    private void HarvestTimerTick()
    {
        if (sound)
            harvestSound.Play();

        wheatCount += harvestCount;
        if (wheatCount >= targetWheatCount)
            Victory();

        harvestTmr = harvestTime;

        UpdateText();
        wheatHarvested += harvestCount;
    }

    private void PeasantTimerTick()
    {
        if (sound)
            peasantSound.Play();
        
        peasantCount++;
        harvestCount = peasantProduction * peasantCount;

        trainPeasantTmr = -1000f;

        trainPeasantBtn.interactable = true;
        trainPeasantBtnHighlight.color = trainPeasantBtnHighlightColor;

        UpdateText();
    }

    private void WarriorTimerTick()
    {
        if (sound)
            warriorSound.Play();

        warriorCount++;
        eatCount = warriorCount * warriorVoracity;

        trainWarriorTmr = -1000f;

        trainWarriorBtn.interactable = true;
        trainWarriorBtnHighlight.color = trainWarriorBtnHighlightColor;

        UpdateText();
        warriorTrained++;
    }

    private void WarningMessage(string message)
    {
        warningText.SetActive(true);
        warningText.GetComponent<Text>().text = message;

        warningText.GetComponent<MessageLifeTime>().alpha = 2.5f;
        warningText.GetComponent<MessageLifeTime>().savePositionTmr = 3f;

        warningText.transform.localPosition = Vector2.zero;
    }

    private string Statistic()
    {
        return $"\n\n{peasantCount - 3}\n{warriorTrained}\n{warriorsLost}\n{wheatHarvested}\n{eaten}\n{raidsReflected - 4}";
    }

    private void Victory()
    {
        Time.timeScale = 0;
        victoryPanel.SetActive(true);
        victoryStatisticText.text = Statistic();

        if (sound)
            victoryMusic.Play();
        music.Stop();
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        gameOverStatisticText.text = Statistic();

        if (sound)
            defeatMusic.Play();
        music.Stop();
    }

    public void Restart()
    {
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);

        raidCount = 0;
        raidCounter = -1.2f;
        warriorCount = 0;
        peasantCount = 3;
        wheatCount = 10;

        warriorTrained = 0;
        warriorsLost = 0;
        wheatHarvested = 0;
        raidsReflected = 0;
        eaten = 0;

        trainWarriorTmr = -1000f;
        trainPeasantTmr = -1000f;
        raidTmr = raidTime;
        eatTmr = eatTime;
        harvestTmr = harvestTime;

        eatCount = warriorVoracity * warriorCount;
        harvestCount = peasantProduction * peasantCount;

        trainWarriorBtn.interactable = true;
        trainWarriorBtnHighlight.color = trainWarriorBtnHighlightColor;

        trainPeasantBtn.interactable = true;
        trainPeasantBtnHighlight.color = trainPeasantBtnHighlightColor;

        Time.timeScale = 1;
        increaseSpeedBtnText.text = "x1";
        increasedSpeed = false;

        victoryMusic.Stop();
        defeatMusic.Stop();
        if (sound)
            music.Play();

        UpdateText();
    }

    public void CreateUnit(string unitType)
    {
        if (unitType == "Warrior")
        {
            if (wheatCount >= warriorCost)
            {
                wheatCount -= warriorCost;
                trainWarriorTmr = trainWarriorTime;

                trainWarriorBtn.interactable = false;
                trainWarriorBtnHighlight.color = new Color(0, 0, 0, 0);
            }
            else
                WarningMessage($"Не хватает {warriorCost - wheatCount} пшеницы");
        }
        else
        {
            if (wheatCount >= peasantCost)
            {
                wheatCount -= peasantCost;
                trainPeasantTmr = trainPeasantTime;

                trainPeasantBtn.interactable = false;
                trainPeasantBtnHighlight.color = new Color(0, 0, 0, 0);
            }
            else
                WarningMessage($"Не хватает {peasantCost - wheatCount} пшеницы");
        }
    }

    void Update()
    {
        TimersCounter();

        if (trainWarriorTmr <= 0 & trainWarriorTmr >= -Time.deltaTime)
            WarriorTimerTick();

        if (trainPeasantTmr <= 0 & trainPeasantTmr >= -Time.deltaTime)
            PeasantTimerTick();

        if (raidTmr < 0)
            RaidTimerTick();

        if (eatTmr < 0)
            EatTimerTick();

        if (harvestTmr < 0)
            HarvestTimerTick();

        UpdateText();
    }
}
