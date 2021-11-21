using PathCreation;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// script used as level manager to manage spawning enemies for each wave
/// </summary>
public class EnemiesSpawner : MonoBehaviour
{
    [SerializeField] PathCreator pathCreator; //path for movement on the map
    string levelString = "level1"; //level string should always be "level" + currentLvl
    public Transform[] portals; //spawn portals
    public Transform target; // destination portal

    [SerializeField] TMPro.TextMeshProUGUI counterTMP; //countdown till the spawning starts

    [Header ("Wave info")]
    public TMPro.TextMeshProUGUI amountText; //show how many of the enemies left to spawn
    public TMPro.TextMeshProUGUI waveIndicator; //current lvl 
    public Image waveIcon;

    ObjectPooler objPooler;
    GameManager gameManager;

    int amountSpawned = 0;
    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < objPooler.poolDictionary[levelString].Count; i++)
        {
            //get and set wave info 
            EnemyScriptable enemy = objPooler.poolDictionary[levelString].Peek().GetComponent<EnemyController>().enemy;
            waveIcon.sprite = enemy.waveSprite;
            waveIndicator.text = "Wave: " + gameManager.currentWave;
            gameManager.waveSpawning = true;

            yield return new WaitForSeconds(enemy.spawnInterval);
            foreach (var portal in portals)
            {
                amountSpawned++;
                amountText.text = (objPooler.poolDictionary[levelString].Count - amountSpawned).ToString();
                gameManager.enemiesAliveInWave++;
                var go = objPooler.SpawnFromPool(levelString, portal.position + new Vector3(0, .5f, -1), Quaternion.identity);
                go.GetComponent<PathFollower>().pathCreator = pathCreator;
            }
        }
        gameManager.currentWave++;
        levelString = "level" + gameManager.currentWave;
        gameManager.waveSpawning = false;

        //spawning for a level finished
        //if (objPooler.poolDictionary[levelString].Count - amountSpawned == 0)
        //{
        //    currentLvl++;
        //    //check if it was last wave in the game
        //    if (currentLvl > GameManager.singleton.maxWaves)
        //    {
        //        GameManager.singleton.GameFinished(false);
        //    }
        //    else
        //    {
        //        levelString = "level" + currentLvl;
        //        StartCoroutine(SpawnAfterDelay(5));
        //    }
        //}
    }

    public void SpawnWave()
    {
        StartCoroutine(SpawnEnemies());
    }
    public void SpawnWaveWithDelay(int amount)
    {
        SetInfo();
        StartCoroutine(SpawnAfterDelay(amount));
    }

    private void Start()
    {
        objPooler = ObjectPooler.instance;
        gameManager = GameManager.singleton;
        SetInfo();
    }

    void SetInfo()
    {
        EnemyScriptable enemy = objPooler.poolDictionary[levelString].Peek().GetComponent<EnemyController>().enemy;
        waveIcon.sprite = enemy.waveSprite;
        waveIndicator.text = "Wave: " + gameManager.currentWave;
        amountText.text = (objPooler.poolDictionary[levelString].Count).ToString();
    }


    IEnumerator SpawnAfterDelay(int amount)
    {
        amountSpawned = 0;
        counterTMP.transform.parent.gameObject.SetActive(true);

        int counter = amount;
        while (counter > 0)
        {
            counterTMP.text = counter.ToString();
            yield return new WaitForSeconds(1);
            counter--;
        }
        SpawnWave();
        counterTMP.text = "";
        counterTMP.transform.parent.gameObject.SetActive(false);
    }


}
