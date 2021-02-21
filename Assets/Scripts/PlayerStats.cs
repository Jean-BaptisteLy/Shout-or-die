using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class PlayerStats : MonoBehaviour
{
    public int nbCoins = 0;
    public double totalTime;
    public DateTime startingTime;
    public DateTime endingTime;
    // Possible categories:
    // 0: slow and bad 
    // 1: fast but bad
    // 2: slow but good
    // 3: fast and good
    public int currentCategory;
    private ProcessData pd;
    // level-associated data
    public Dictionary<string,Tuple<int, float>> levelInfo = new Dictionary<string,Tuple<int, float>>();
    public int coinsTotal;
    public float timeOptimal;
    public int currentLevel = 0;
    public TMPro.TextMeshProUGUI text;
    public Timer timer;

    void Start(){
        pd = gameObject.GetComponent<ProcessData>();
        // nbCoins = 0;
        startingTime = DateTime.Now;
        // Initialize dictionary of level information
        levelInfo.Add("Level0", Tuple.Create(6, 20.0f));
        levelInfo.Add("Level1", Tuple.Create(7, 30.0f));
        levelInfo.Add("Level2", Tuple.Create(4, 40.0f));
        levelInfo.Add("Level3", Tuple.Create(5, 50.0f));
        coinsTotal = levelInfo["Level" + currentLevel].Item1;
        // // temps optimal adapté au ratio temps du joueur
        // timeOptimal = levelInfo["Level" + currentLevel].Item2 + (levelInfo["Level" + currentLevel].Item2 * (1 - pd.timeRatioList[pd.timeRatioList.Count - 1]));
        text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        timer = text.GetComponent<Timer>();
    }

    public void addCoin(){
        nbCoins++;
        // coin that adds time if player is mauvais mais rapide
        if (currentCategory == 1 || currentCategory == 3) {
            timer.timeLeft += 5f;
        }
    }

    public void reinitStats(){
        nbCoins = 0;
        startingTime = DateTime.Now;
        coinsTotal = levelInfo["Level" + currentLevel].Item1;
        // Debug.Log("coinsTotal: " + coinsTotal);
        timeOptimal = levelInfo["Level" + currentLevel].Item2;
    }

    public Tuple<int, int, float, float> getStats(){
        // Debug.Log("Get stats, current level: " + currentLevel);
        int levelN = currentLevel - 1;
        int ccollected = this.nbCoins;
        int coinsOnlevel = levelInfo["Level" + levelN].Item1;
        float playerTime = (float)this.totalTime;
        float optTime = levelInfo["Level" + levelN].Item2;
        return Tuple.Create(ccollected, coinsOnlevel, playerTime, optTime);
    }

    public void updateLevelEndingStats(){
        // Debug.Log("updateStats from level " + currentLevel);
        endingTime = DateTime.Now;
        totalTime = (endingTime - startingTime).TotalSeconds;
        if (currentLevel <= 3){
        // no player category for the very first level (initialization level)
            if (currentLevel != 0){
            // temps optimal adapté au ratio temps du joueur
                timeOptimal = levelInfo["Level" + currentLevel].Item2 + (levelInfo["Level" + currentLevel].Item2 * (1 - pd.timeRatioList[pd.timeRatioList.Count - 1]));
            }else{
                timeOptimal = levelInfo["Level0"].Item2;
            }
            pd.addPlayerStats(nbCoins, coinsTotal, (float)totalTime, (float)timeOptimal);
            pd.upgradelastPlayedLevelNumber();
            currentCategory = pd.playerCategory;
        }
        
    }

    public void upgradeLevelNumber(){
        currentLevel++;
    }

    


    
}
