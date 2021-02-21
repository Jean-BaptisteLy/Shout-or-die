using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LogWritter : MonoBehaviour{

    public List<string> timeValues = new List<string>();
    public List<float> loudnessValues = new List<float>();

    private PlayerMovement pm;
    private DateTime startTime;
    private double totalTime;
    private IEnumerator coroutine;
    private PlayerStats ps;

    private StreamWriter writer;
    // file containing loudness data of current level
    private string loudnessFile;
    // file containing stats
    private string statsFile;
    private StreamWriter statsWriter;
    private int currentLevel;
    public float nbSeconds = 1.0f;

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
        loudnessValues = new List<float>();
        startTime = DateTime.Now;
        if (!File.Exists(loudnessFile)){
            writer = new StreamWriter(loudnessFile);
        }else{
            File.Delete(loudnessFile);
            writer = new StreamWriter(loudnessFile);
        }
        writer.WriteLine("Time, Loudness");
        // Write loudness info every nbSeconds
        coroutine = WaitAndWrite();
        StartCoroutine(coroutine);
        // Debug.Log("start coroutine");
    }

    public void resetLogger(int currentLevel){
        // Debug.Log("ResetLogger");
        StopCoroutine(coroutine);
        // Debug.Log("resetLogger a stop coroutine");
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
            // Debug.Log("flushLevelLogger a stop coroutine");
        }
        //writer.Close();
        // writer = null;
    }

    private IEnumerator WaitAndWrite(){
        Debug.Log("la coroutine commence");
        while(true){
            // Debug.Log("coroutine");
            totalTime = (DateTime.Now - startTime).TotalSeconds;
            // timeValues.Add(totalTime.ToString()); // to uncomment maybe
            float loudness = pm.loudness;
            loudnessValues.Add(loudness);
            string line = totalTime + "," + loudness.ToString();
            writer.WriteLine(line);

            // writer.Flush(); // to uncomment maybe
            yield return new WaitForSeconds(nbSeconds);
        }
        // Debug.Log("c'est fini la coroutine");
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
        // Debug.Log(stats);
        statsWriter.WriteLine(currentLevel + "," + stats.Item1 + "," + stats.Item2);
        statsWriter.Flush();
    }

    public List<float> getLoudnessData(){
        return loudnessValues;
    }

    void OnDestroy(){
        if (writer != null){
            writer.Flush();
            writer.Close();
        }
        if (statsWriter != null){
            // Debug.Log("update level ending stats bc OnDestroy :( ");
            ps.updateLevelEndingStats();
            // Debug.Log("after update level ending stats bc onDestroy");
            writeDownStats();
            statsWriter.Close();
            // Debug.Log("statsWriter closed");
        }
    }
}
