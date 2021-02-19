using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Calibration : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    private List<float> valuesForMoveThreshold;
    private List<float> valuesForJumpThreshold;
    private float secondsLeft;
    private IEnumerator coroutine;
    private DateTime startingTime;
    private float totalSeconds = 5f;
    private bool moveThreshReady = false;

    // Start is called before the first frame update
    void Start(){
        text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        secondsLeft = 5;
        coroutine = SaveLoudnessValues();
        valuesForMoveThreshold = new List<float>();
        valuesForJumpThreshold = new List<float>();
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Return)){
            calibrate();
        }
        if ((totalSeconds - (DateTime.Now - startingTime).TotalSeconds <= 0f) && (moveThreshReady)){
            StopCoroutine(coroutine);
            text.text = "Now a little bit louder...\n\n Press Enter when you are ready";
        }

        
    }

    public void calibrate(){
        startingTime = DateTime.Now;
        if (!moveThreshReady){
            text.text = "Generate sound with your average voice volume...\n"; 
        }
        // else{
        //     text.text = "Now a little bit louder..."
        // }
        // Debug.Log("SecondsLeft: " + (totalSeconds - (DateTime.Now - startingTime).TotalSeconds) + "and" + secondsLeft*10);
        moveThreshReady = true;
        StartCoroutine(coroutine);
        // now a little bit louder
    }

    private IEnumerator SaveLoudnessValues(){
        while(true){
            // Debug.Log("Saving loudness info");
            text.text = Mathf.Round(totalSeconds - (float)(DateTime.Now - startingTime).TotalSeconds) + "...";
            if (!moveThreshReady){
                valuesForMoveThreshold.Add(1.0f);
            }else{
                valuesForJumpThreshold.Add(2.0f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
