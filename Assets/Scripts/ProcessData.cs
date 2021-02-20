using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
// using LumenWorks.Framework.IO.Csv;

public class ProcessData : MonoBehaviour
{   
    private string filePath;
    //private DataTable csvTable;
    private string[] words;
    private List<float> coinsRatioList;
    public List<float> timeRatioList;

    public int playerCategory;
    private int lastPlayedLevel;

    private PlayerMovement pm;
    private int totalJumps;

    void Start(){
        lastPlayedLevel = 0;

        coinsRatioList = new List<float>();
        timeRatioList = new List<float>();
        pm = gameObject.GetComponent<PlayerMovement>();

    }
    public void addPlayerStats(int coinsCollected, int coinsTotal, float timePlayer, float timeOptimal){
        processDataCurve();
        addRatios(coinsCollected, coinsTotal, timePlayer, timeOptimal);
        playerCategory = getUpdatedCategory();

    }
    // Update is called once per frame
    void Update(){
        
    }
    public void processData(){
        // put what we have on start here
    }
    private string getPath(){
        // return Application.dataPath + "/../Data/data-2021-01-28_04-44-33.csv";
        return Application.dataPath + "/../Data/test.csv";
    }

    private void addRatios(int coinsCollected, int coinsTotal, float timePlayer, float timeOptimal){
        // Calculate ratios
        float levelCoinsRatio = (coinsCollected*1.0f)/coinsTotal;
        // Debug.Log("levelCoinsRatio: " + coinsCollected + "/" + coinsTotal +"= " + levelCoinsRatio);
        float levelTimeRatio;
        Debug.Log("LevelInfo: " + "timeOpt: " + timeOptimal + "timePlayer" + timePlayer);
        if (lastPlayedLevel == 0){
            if (timePlayer < 20.0f){
                levelTimeRatio = 1.0f;
            }else{
                levelTimeRatio = 20.0f/timePlayer;
            }
            // levelTimeRatio = 1 - timePlayer/timeOptimal;
        }else{
            levelTimeRatio = timeOptimal/timePlayer;
            if (levelTimeRatio > 1.0f){
                levelTimeRatio = 1.0f;
            }
            // levelTimeRatio = = 0.5f*timePlayer/timeOptimal + 0.5f*timeRatioList[timeRatioList.Count - 1];
        }

        // Add them to lists
        coinsRatioList.Add(levelCoinsRatio);
        timeRatioList.Add(levelTimeRatio);    
    }

    private int getUpdatedCategory(){
            // Possible categories:
            // 0: slow and bad 
            // 1: fast but bad
            // 2: slow but good
            // 3: fast and good
        // gamma for quality: importance of current score
        float gammaOne = 0.7f;
        // gamma for time: importance of current score
        float gammaTwo = 0.7f;

        // to automatize
        float fOne;
        float fTwo;

        if (lastPlayedLevel == 0){
            // do something else
            // Debug.Log("Level0 coins Ratio: " + coinsRatioList[lastPlayedLevel]);
            fOne = coinsRatioList[lastPlayedLevel];
            // specific data-processing for level 0 because it is an initialization level
            fTwo = timeRatioList[lastPlayedLevel];
            
        }else{
            fOne = gammaOne*coinsRatioList[lastPlayedLevel] + (1-gammaOne)*coinsRatioList[lastPlayedLevel-1];
            fTwo = gammaTwo*timeRatioList[lastPlayedLevel] + (1-gammaTwo)*timeRatioList[lastPlayedLevel-1];
        }
        Debug.Log("Last played level: " + lastPlayedLevel);
        Debug.Log("fOne: " + fOne + ", fTwo:" + fTwo);
        if (fOne >= 0.5){
            if (fTwo >= 0.5){
                // Bon et rapide
                playerCategory = 3;
            }
            else{
                // Bon mais lent
                playerCategory = 2;
            }
        }else{
            // Mauvais mais rapide
            if(fTwo >= 0.5){
                playerCategory = 1;
            }else{
                // Mauvais et lent
                playerCategory = 0;
            }

        }
        return playerCategory;
    }


    private float processDataCurve(){
        List<float> loudnessData = gameObject.GetComponent<LogWritter>().getLoudnessData();
        float jumpThresh = pm.jumpLoudnessThreshold;
        totalJumps = 0;
        // Debug.Log("Total data lines: " + loudnessData.Count);
        for (int i = 1; i < loudnessData.Count; i++){
            // check if player jumped
            if ((loudnessData[i-1] < jumpThresh) && (loudnessData[i] >= jumpThresh)){
                // Debug.Log("Jumped at " + i);
                totalJumps++;
            }
        }
        // Debug.Log("Total jumps: " + totalJumps);
        return 1.0f;
    }

    public void upgradelastPlayedLevelNumber(){
        lastPlayedLevel++;
    }

}
