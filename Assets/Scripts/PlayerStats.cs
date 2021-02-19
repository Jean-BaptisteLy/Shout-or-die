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
    // Start is called before the first frame update
    void Start(){
        pd = gameObject.GetComponent<ProcessData>();
        // nbCoins = 0;
        startingTime = DateTime.Now;
        // Initialize dictionary of level information
        levelInfo.Add("Level0", Tuple.Create(6, 80.0f));
        levelInfo.Add("Level1", Tuple.Create(7, 80.0f));
        levelInfo.Add("Level2", Tuple.Create(4, 80.0f));
        levelInfo.Add("Level3", Tuple.Create(5, 80.0f));
        coinsTotal = levelInfo["Level" + currentLevel].Item1;
        timeOptimal = levelInfo["Level" + currentLevel].Item2;
        text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        timer = text.GetComponent<Timer>();
        // Tuple.Create(this.nbCoins, this.totalTime)
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void addCoin(){
        nbCoins++;
        // coin that adds time if player is mauvais mais rapide
        if (currentCategory == 1){
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

    public Tuple<int, double> getStats(){
        return Tuple.Create(this.nbCoins, this.totalTime);
    }

    public void updateLevelEndingStats(){
        endingTime = DateTime.Now;
        totalTime = (endingTime - startingTime).TotalSeconds;
        // Debug.Log("Inside update level ending stats");
        // TO DO: extract this info (timeopt and totalcoins) automatically from level info once it has been generated
        // Debug.Log("updatelevelendinstats calling addplayerstats");
        // no player category for the very first level (initialization level)
        // if (currentLevel != 0){
        //     Debug.Log("Current level: " + currentLevel);
        //     // TODO: check if it works
        //     coinsTotal = levelInfo["Level" + currentLevel].Item1;
        //     timeOptimal = levelInfo["Level" + currentLevel].Item2;
        //     // replace by thing here below later
        //     // coinsTotal = levelInfo["Level" + currentLevel + "." + currentCategory].Item1;
        //     // timeOptimal = levelInfo["Level" + currentLevel + "." + currentCategory].Item2;
        // }
        pd.addPlayerStats(nbCoins, coinsTotal, (float)totalTime, (float)timeOptimal);
        pd.upgradelastPlayedLevelNumber();
        
    }

    public void upgradeLevelNumber(){
        currentLevel++;
    }


    
}
