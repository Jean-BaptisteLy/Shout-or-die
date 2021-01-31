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
    void Start(){
        filePath = getPath();   
        // csvTable = new DataTable();  
        // CsvRead csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(filePath)), true);
        // csvTable.Load(csvReader);
        // Debug.Log("This is C#");
        //words = System.IO.File.ReadAllText (filePath).Split(","[0]);
        // string word;
        // foreach (string word in words){
        //     Debug.Log(word);
        //     }
        // for (int i = 0; i <= wordList.size-1; i++){
        //     Debug.Log(wordList[i]);
        //     }
        StreamReader sr = new StreamReader(filePath);
        string line = sr.ReadLine();
        // Read and display lines from the file until the end of
        // the file is reached.
        while ((line = sr.ReadLine()) != null){
            string[] lineValues= line.Split(',');
            addRatios(lineValues);
            // do something with this data
            //Debug.Log((line));
        }


    }
    // Update is called once per frame
    void Update(){
        
    }

    private string getPath(){
        // return Application.dataPath + "/../Data/data-2021-01-28_04-44-33.csv";
        return Application.dataPath + "/../Data/test.csv";
    }

    private void addRatios(string[] lineValues){
        int level = Int32.Parse(lineValues[0]);
        int coinsCollected = Int32.Parse(lineValues[1]);
        int coinsTotal = Int32.Parse(lineValues[2]);
        float timePlayer =  float.Parse(lineValues[3]);
        float timeOptimal = float.Parse(lineValues[4]);

        // Calculate ratios
        float levelCoinsRatio = coinsCollected/coinsTotal;
        float levelTimeRatio = timePlayer/timeOptimal;

        if (level == 0){
           coinsRatioList = new List<float>();
           timeRatioList = new List<float>();
        }

        // Add them to lists
        coinsRatioList.Add(levelCoinsRatio);
        timeRatioList.Add(levelTimeRatio);    
    }

    private int getUpdatedCategory(){
        // gamma for quality: importance of current score
        float gammaOne = 0.7f;
        // gamma for time: importance of current score
        float gammaTwo = 0.7f;

        // to automatize
        int lastPlayedLevel = 1;
        float fOne = gammaOne*coinsRatioList[lastPlayedLevel] + (1-gammaOne)*coinsRatioList[lastPlayedLevel-1];
        float fTwo = gammaTwo*timeRatioList[lastPlayedLevel] + (1-gammaTwo)*timeRatioList[lastPlayedLevel-1];

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
