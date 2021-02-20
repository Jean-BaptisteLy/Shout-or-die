using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    public float elapsedTime = 0f;
    public float timeLeft;
    public float timeStay;
    private PlayerStats ps;
    private ProcessData pd;
    private PlayerMovement pm;
    // Start is called before the first frame update
    void Start(){
        text = GetComponent<TMPro.TextMeshProUGUI>();
        ps = GameObject.Find("Player").GetComponent<PlayerStats>();
        //pd = gameObject.GetComponent<ProcessData>();
        pd = GameObject.Find("Player").GetComponent<ProcessData>();
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        timeLeft = ps.timeOptimal;
        timeStay = ps.timeOptimal;
    }

    // Update is called once per frame
    void Update(){
        elapsedTime += Time.deltaTime;
        timeLeft -= Time.deltaTime;
        if (pm.loudness < pm.runLoudnessThreshold) {
            timeStay -= Time.deltaTime;
        }
        if (ps.currentLevel == 0){
            text.text = "Elapsed time: " + Mathf.Round(elapsedTime);
        }else{
            // slow and bad
            if(ps.currentCategory != 0) {
                //Debug.Log("Timer.cs : playerCategory = 0");
                text.text = "Time left: " + Mathf.Round(timeLeft)+ "\nTime stay: " + Mathf.Round(timeStay);
            }else{
                text.text = "Elapsed time: " + Mathf.Round(elapsedTime);
            }
        }
    }

    public void restartTimer(){
        elapsedTime = 0.0f;
        //timeLeft = ps.timeOptimal;
        if (pd.playerCategory == 0) {
            timeLeft = 9999; // infini
        }
        else if (pd.playerCategory == 1) {
            // time stay
            timeStay = (ps.timeOptimal / 2) / pd.timeRatioList[pd.timeRatioList.Count - 1];
        }
        else if (pd.playerCategory == 2) {
            // time initial
            timeLeft = (ps.timeOptimal / 10) / pd.timeRatioList[pd.timeRatioList.Count - 1];
        }
        else if (pd.playerCategory == 3) {
            // time stay
            timeStay = (ps.timeOptimal / 2) / pd.timeRatioList[pd.timeRatioList.Count - 1];
            // time initial
            timeLeft = (ps.timeOptimal / 10) / pd.timeRatioList[pd.timeRatioList.Count - 1];
        }
        else {
            Debug.Log("Problème Timer.cs... Impossible.");
        }
    }
}
