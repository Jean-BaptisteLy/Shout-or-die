using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class LogWritter : MonoBehaviour{

    public List<string> timeValues = new List<string>();
    public List<string> loudnessValues = new List<string>();

    private string datetime;
    private PlayerMovement pm;
    private DateTime start_time;
    private double total_time;
    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start(){
        // Debug.Log("LogWritter started");
        pm = gameObject.GetComponent<PlayerMovement>();
        
        start_time = DateTime.Now;
        // Log file identifier
        datetime = string.Format("data-{0:yyyy-MM-dd_hh-mm-ss}", DateTime.Now);

        coroutine = WaitAndWrite();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    // void Update(){
    //     if (Input.GetKeyDown(KeyCode.X))
    //     {
    //     //     OnlyX.Add("X");
    //         total_time = (DateTime.Now - start_time).TotalSeconds;
    //         timeValues.Add(total_time.ToString());
    //         loudnessValues.Add(pm.loudness.ToString());
    //     }

    //     string filePath = getPath();
 
    //     StreamWriter writer = new StreamWriter(filePath);

    //     writer.WriteLine("Time, Loudness");

    //     // iterate through values to be written
    //     for (int i = 0; i < timeValues.Count; ++i) {
    //         writer.WriteLine(timeValues[i] + "," + loudnessValues[i]);
    //     }
       
    //     writer.Flush();
    //     writer.Close();
    // }

    private IEnumerator WaitAndWrite(){
        while(true){
            total_time = (DateTime.Now - start_time).TotalSeconds;
            timeValues.Add(total_time.ToString());
            loudnessValues.Add(pm.loudness.ToString());

            string filePath = getPath();
    
            StreamWriter writer = new StreamWriter(filePath);

            writer.WriteLine("Time, Loudness");

            // iterate through values to be written
            for (int i = 0; i < timeValues.Count; ++i) {
                writer.WriteLine(timeValues[i] + "," + loudnessValues[i]);
            }
        
            writer.Flush();
            writer.Close();
            yield return new WaitForSeconds(2.0f);
        }
    }

    private string getPath(){
        // Debug.Log(Application.dataPath);
        // Debug.Log("Inside getPath");
        return Application.dataPath + "/../Data/"  + datetime + ".csv";
    }
}
