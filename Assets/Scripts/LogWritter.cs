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
    private PlayerStats ps;

    private StreamWriter writer; // to uncomment
    // file containing loudness data of current level
    private string loudnessFile;
    // file containing stats
    private string statsFile;
    private StreamWriter statsWriter;
    private int currentLevel;

    // Start is called before the first frame update
    void Start(){
        // Debug.Log("LogWritter started");
        currentLevel = 0;
        pm = gameObject.GetComponent<PlayerMovement>();
        ps = gameObject.GetComponent<PlayerStats>();
        startNewLevelLogger(0);
        // stats file
        statsFile = getStatsPath();
        if (!File.Exists(statsFile)){
            statsWriter = new StreamWriter(statsFile);
        }else{
            File.Delete(statsFile);
            statsWriter = new StreamWriter(statsFile);
        }
        statsWriter.WriteLine("level,ccollected,ctotal,time,optttime");
        
    }

    public void startNewLevelLogger(int currentLevel){
        // Debug.Log("startNewleveLogger");

        // this function must be call at the beggining of each, and flush only to be called when changing the level
        loudnessFile = getLoudnessPath(currentLevel);
        this.currentLevel = currentLevel;
        startTime = DateTime.Now;
        if (!File.Exists(loudnessFile)){
            writer = new StreamWriter(loudnessFile); // to uncomment
        }else{
            File.Delete(loudnessFile);
            writer = new StreamWriter(loudnessFile); // to uncomment
        }
        writer.WriteLine("Time, Loudness"); // to uncomment
        // Write loudness info every 2 seconds
        coroutine = WaitAndWrite();
        StartCoroutine(coroutine);
    }

    public void resetLogger(int currentLevel){
        // Debug.Log("ResetLogger");
        StopCoroutine(coroutine);
        // writer.Close();
        if (writer != null){
             writer.Close();
        }
        startNewLevelLogger(currentLevel);
    }

    public void flushLevelLogger(){
        // Debug.Log("flushLevelLogger");
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

    private string getLoudnessPath(int level){
        string filename = "loudnessDataN" + level;
        return Application.dataPath + "/../Data/" + filename + ".csv";
    }

    private string getStatsPath(){
        string filename = "stats";
        return Application.dataPath + "/../Data/" + filename + ".csv";
    }

    public void writeDownStats(){
        Tuple<int, double> stats = ps.getStats();
        statsWriter.WriteLine(currentLevel + "," + stats.Item1 + "," + stats.Item2);
        statsWriter.Flush();
    }

    void OnDestroy(){
        if (writer != null){
            writer.Flush();
            writer.Close();
        }
        if (statsWriter != null){
            ps.updateLevelEndingStats();
            writeDownStats();
            // statsWriter.Flush();
            statsWriter.Close();
        }
    }
}
