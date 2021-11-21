using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GamePhase
{
    Start, Game, End
}
/// <summary>
/// script used to control state of the game and all the necessary info on the scene
/// it's also used to manage health, gold
/// </summary>
public class GameManager : MonoBehaviour
{
    public GamePhase gamePhase;
    public int maxWaves;
    public int currentWave = 1;
    public int enemiesAliveInWave = 0;
    public bool waveSpawning = false;
    [SerializeField] EnemiesSpawner enemySpawner;
    public static GameManager singleton;
    [Header ("Player Health")]
    public int currentPlayerHP;
    public int maxPlayerHP;
    [Header("Player Gold")]
    public int currentGold = 550;
    [SerializeField] TMPro.TextMeshProUGUI currentGoldTMP;
    [HideInInspector] public int currentScore = 0;
    [Header("Score")]
    [SerializeField] TMPro.TextMeshProUGUI currentScoreTMP;
    [SerializeField] TMPro.TextMeshProUGUI highScoreTMP;
    [Header("Game finish")]
    [SerializeField] TMPro.TextMeshProUGUI finishTextTMP;
    [SerializeField] GameObject finishPanel;
    [SerializeField] GameObject towerPlacementCanvas;
    [Header ("Health")]
    [SerializeField] Image playerHealthBar;
    [SerializeField] TMPro.TextMeshProUGUI amountTMP;


    private void Awake()
    {
        if(singleton == null)
        {
            singleton = this;
        }
    }
    
    public void ShowGold()
    {
        currentGoldTMP.text = currentGold.ToString();
    }

    //called when enemy dies
    public void AddGold(int goldAmount)
    {
        enemiesAliveInWave--;
        currentGold += goldAmount;
        //score will be same as gold earned
        AddScore(goldAmount);
        ShowGold();
        CheckAndGoNextWave();
    }
    
    void CheckAndGoNextWave()
    {
        if (enemiesAliveInWave == 0 && !waveSpawning)
        {
            if (currentWave <= maxWaves)
            {
                enemySpawner.SpawnWaveWithDelay(5);
            }
            else
            {
                GameFinished();
            }
        }
    }

    private void Start()
    {
        ShowGold();
        SetStartScore();
    }

    public void StartGame()
    {
        SetStartScore();
        gamePhase = GamePhase.Game;
        enemySpawner.SpawnWave();
    }

    void SetStartScore()
    {
        currentScore = 0;
        currentScoreTMP.text = "0";
        highScoreTMP.text = HighScore.GetHighScore().ToString();
        playerHealthBar.fillAmount = (float) currentPlayerHP / (float) maxPlayerHP;
        amountTMP.text = currentPlayerHP.ToString();
    }

    public void AddScore(int scoreAmount)
    {
        currentScore += scoreAmount;
        if (currentScore > HighScore.GetHighScore())
            HighScore.SetHighScore(currentScore);

        currentScoreTMP.text = currentScore.ToString();
        highScoreTMP.text = HighScore.GetHighScore().ToString();
    }

    public void GameFinished(bool lost=false)
    {
        if (lost)
            finishTextTMP.text = "Game Over!";
        else finishTextTMP.text = "Game finished!";
     
        Time.timeScale = 0;
        gamePhase = GamePhase.End;
        finishPanel.SetActive(true);
        towerPlacementCanvas.SetActive(false);
    }

    public void StartNewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //called when enemy reaches target portal
    public void RemovePlayerHP(int amount)
    {
        enemiesAliveInWave--;
        currentPlayerHP -= amount;
        playerHealthBar.fillAmount = (float)currentPlayerHP / (float)maxPlayerHP;
        amountTMP.text = currentPlayerHP.ToString();

        if (currentPlayerHP <= 0)
            GameFinished(true);
        else
        {
            CheckAndGoNextWave();
        }
    }
}
