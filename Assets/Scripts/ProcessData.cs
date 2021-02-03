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
    private List<float> timeRatioList;

    private int playerCategory;
    private int lastPlayedLevel;

    void Start(){
        lastPlayedLevel = 0;

        coinsRatioList = new List<float>();
        timeRatioList = new List<float>();
    }
    public void addPlayerStats(int coinsCollected, int coinsTotal, float timePlayer, float timeOptimal){
        // filePath = getPath();   
        // StreamReader sr = new StreamReader(filePath);
        // string line = sr.ReadLine();
        // // Read and display lines from the file until the end of
        // // the file is reached.
        // while ((line = sr.ReadLine()) != null){
        //     string[] lineValues= line.Split(',');
        //     addRatios(lineValues);
        //     // do something with this data
        //     //Debug.Log((line));
        // }
        
        // add ratios to their list, then updates the user category
        // Debug.Log("addplayerstats");
        addRatios(coinsCollected, coinsTotal, timePlayer, timeOptimal);
        playerCategory = getUpdatedCategory();
        // Debug.Log("afeter getUpdatedCategory middle statsplayer");
        // Debug.Log("Level: " + lastPlayedLevel + ", Player Category: " + playerCategory);
        lastPlayedLevel++;
        // Debug.Log("end of addplayerstats");

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
    // private void addRatios(string[] lineValues){
        // int level = Int32.Parse(lineValues[0]);
        // int coinsCollected = Int32.Parse(lineValues[1]);
        // int coinsTotal = Int32.Parse(lineValues[2]);
        // float timePlayer =  float.Parse(lineValues[3]);
        // float timeOptimal = float.Parse(lineValues[4]);

        // Calculate ratios
        float levelCoinsRatio = coinsCollected/coinsTotal;
        float levelTimeRatio;
        if (lastPlayedLevel == 0){
            levelTimeRatio = 0.5f + (20.0f - timePlayer)/(2.0f*20.0f);
        }else{
            levelTimeRatio = timePlayer/timeOptimal;
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
        // Debug.Log("inside getUpdatedCat");
        // gamma for quality: importance of current score
        float gammaOne = 0.7f;
        // gamma for time: importance of current score
        float gammaTwo = 0.7f;

        // to automatize
        float fOne;
        float fTwo;

        if (lastPlayedLevel == 0){
            // do something else
            fOne = coinsRatioList[lastPlayedLevel];
            // specific data-processing for level 0 because it is an initialization level
            fTwo = timeRatioList[lastPlayedLevel];
            
        }else{
            fOne = gammaOne*coinsRatioList[lastPlayedLevel] + (1-gammaOne)*coinsRatioList[lastPlayedLevel-1];
            fTwo = gammaTwo*timeRatioList[lastPlayedLevel] + (1-gammaTwo)*timeRatioList[lastPlayedLevel-1];
        }

        if (fOne >= 0.5){
            if (fTwo >= 0.5){
                playerCategory = 3;
            }
            else{
                playerCategory = 2;
            }
        }else{
            if(fTwo >= 0.5){
                playerCategory = 1;
            }else{
                playerCategory = 0;
            }

        }
        return playerCategory;
    }

}
