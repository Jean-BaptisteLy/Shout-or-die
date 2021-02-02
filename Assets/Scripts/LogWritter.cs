using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LogWritter : MonoBehaviour{

    public List<string> timeValues = new List<string>();
    public List<string> loudnessValues = new List<string>();

    private PlayerMovement pm;
    private DateTime startTime;
    private double totalTime;
    private IEnumerator coroutine;

    private StreamWriter writer; // to uncomment
    // file containing loudness data of current level
    private string filePath;
    private int currentLevel;

    // Start is called before the first frame update
    void Start(){
        Debug.Log("LogWritter started");
        currentLevel = 0;
        pm = gameObject.GetComponent<PlayerMovement>();
        startNewLevelLogger(0);
    }

    public void startNewLevelLogger(int currentLevel){
        Debug.Log("startNewleveLogger");

        // this function must be call at the beggining of each, and flush only to be called when changing the level
        filePath = getPath(currentLevel);
        this.currentLevel = currentLevel;
        startTime = DateTime.Now;
        if (!File.Exists(filePath)){
            writer = new StreamWriter(filePath); // to uncomment
        }else{
            File.Delete(filePath);
            writer = new StreamWriter(filePath); // to uncomment
        }
        writer.WriteLine("Time, Loudness"); // to uncomment
        // Write loudness info every 2 seconds
        coroutine = WaitAndWrite();
        StartCoroutine(coroutine);
    }

    public void resetLogger(int currentLevel){
        Debug.Log("ResetLogger");
        StopCoroutine(coroutine);
        // writer.Close();
        if (writer != null){
             writer.Close();
        }
        startNewLevelLogger(currentLevel);
    }

    public void flushLevelLogger(){
        Debug.Log("flushLevelLogger");
        if (writer != null){
            writer.Flush();
            StopCoroutine(coroutine);
        }
        //writer.Close();
        // writer = null;
    }

    private IEnumerator WaitAndWrite(){
        while(true){
            totalTime = (DateTime.Now - startTime).TotalSeconds;
            // timeValues.Add(totalTime.ToString()); // to uncomment maybe
            // loudnessValues.Add(pm.loudness.ToString()); // to uncomment maybe
            string line = totalTime + "," + pm.loudness.ToString();
            writer.WriteLine(line); // to uncomment
        

            // writer.Flush(); // to uncomment maybe
            yield return new WaitForSeconds(2.0f);
        }
    }

    void OnDestroy(){
        if (writer != null){
            writer.Flush();
            writer.Close();
        }
    }

    private string getPath(int level){
        string filename = "loudnessDataN" + level;
        return Application.dataPath + "/../Data/" + filename + ".csv";
    }
}
