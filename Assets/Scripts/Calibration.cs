using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Calibration : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    public List<float> valuesForMoveThreshold;
    public List<float> valuesForJumpThreshold;
    private IEnumerator coroutine;
    private DateTime startingTime;
    private float totalSeconds = 5f;
    private bool moveThreshReady = false;
    public float moveThresh;
    public float jumpThresh;

    // Start is called before the first frame update
    void Start(){
        text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        coroutine = SaveLoudnessValues();
        valuesForMoveThreshold = new List<float>();
        valuesForJumpThreshold = new List<float>();
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Return)){
            calibrate();
        }
        if ((totalSeconds - (DateTime.Now - startingTime).TotalSeconds <= 0f) && (valuesForMoveThreshold.Count == 10)){
            moveThreshReady = true;
            StopCoroutine(coroutine);
            Debug.Log(getMoveThresh());
            text.text = "Now a little bit louder...\n\n Press Enter when you are ready";
        }
        if ((totalSeconds - (DateTime.Now - startingTime).TotalSeconds <= 0f) && (valuesForJumpThreshold.Count == 10)){
            StopCoroutine(coroutine);
            Debug.Log(getJumpThresh());
            text.text = "Let's play!";
            coroutine = WaitForTwoSeconds();
            StartCoroutine(coroutine);
            // TODO: initialize player thresholds here
            // change of scene
            SceneManager.LoadScene("Level 0");
            gameObject.SetActive(false);
        }
    }

    public void calibrate(){
        startingTime = DateTime.Now;
        if (!moveThreshReady){
            text.text = "Generate sound with your average voice volume...\n"; 
        }
        StartCoroutine(coroutine);

    }

    private IEnumerator SaveLoudnessValues(){
        while(true){
            // Debug.Log("Saving loudness info");
            text.text = Mathf.Round(totalSeconds - (float)(DateTime.Now - startingTime).TotalSeconds) + "...";
            if (!moveThreshReady){
                Debug.Log("Value added!");
                valuesForMoveThreshold.Add(1.0f);
            }else{
                valuesForJumpThreshold.Add(2.0f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator WaitForTwoSeconds(){
        yield return new WaitForSeconds(2);
    }

    public float getMoveThresh(){
        moveThresh = 0;
        for (int i = 0; i < valuesForMoveThreshold.Count; i++){
            moveThresh += valuesForMoveThreshold[i];
            Debug.Log("moveThreshvalue = " + valuesForMoveThreshold[i]);
        }
        Debug.Log("moveThresh = " + moveThresh);
        return moveThresh/valuesForMoveThreshold.Count;
    }

    public float getJumpThresh(){
        jumpThresh = 0;
        for (int i = 0; i < valuesForJumpThreshold.Count; i++){
            jumpThresh += valuesForJumpThreshold[i];
            Debug.Log("jumpThreshvalue = " + valuesForJumpThreshold[i]);
        }
        Debug.Log("jumpThresh = " + jumpThresh);
        return jumpThresh/valuesForJumpThreshold.Count;
    }


}
