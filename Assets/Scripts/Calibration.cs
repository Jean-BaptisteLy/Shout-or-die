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
    AudioSource _audio;
    private float sensitivity = 100.0f;
    private float loudness = 0.0f;
    private PlayerMovement pm;

    void Start(){
        text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        coroutine = SaveLoudnessValues();
        valuesForMoveThreshold = new List<float>();
        valuesForJumpThreshold = new List<float>();
        _audio = GameObject.Find("Listener").GetComponent<AudioSource>();
        _audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;
        _audio.Play();
    }

    // Update is called once per frame
    void Update(){
        if (SceneManager.GetActiveScene().name == "Calibration"){
            loudness = GetAverageVolume() * sensitivity;
        // if (SceneManager.GetActiveScene().name == "Level 0"){
        //     pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
        // }
            if (Input.GetKeyDown(KeyCode.Return)){
                calibrate();
            }
            if ((totalSeconds - (DateTime.Now - startingTime).TotalSeconds <= 0f) && (valuesForMoveThreshold.Count == 10)){
                moveThreshReady = true;
                StopCoroutine(coroutine);
                // Debug.Log(getMoveThresh());
                text.text = "Now a little bit louder...\n\n Press Enter when you are ready";
            }
            if ((totalSeconds - (DateTime.Now - startingTime).TotalSeconds <= 0f) && (valuesForJumpThreshold.Count == 10)){
                StopCoroutine(coroutine);
                // Debug.Log(getJumpThresh());
                text.text = "Let's play!";
                coroutine = WaitForTwoSeconds();
                StartCoroutine(coroutine);
                moveThresh = getMoveThresh();
                jumpThresh = getJumpThresh();
                // change of scene
                SceneManager.LoadScene("Level 0");
                // initialize player thresholds here
                GameObject player = GameObject.Find("Player");
                // pm = player.GetComponent<PlayerMovement>();
                // pm.runLoudnessThreshold = moveThresh;
                // pm.jumpLoudnessThreshold = jumpThresh;
                // deactivate calibration
                // gameObject.SetActive(false);
            }
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
                // Debug.Log("Value added!");
                valuesForMoveThreshold.Add(loudness);
            }else{
                valuesForJumpThreshold.Add(loudness);
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
            // Debug.Log("moveThreshvalue = " + valuesForMoveThreshold[i]);
        }
        // Debug.Log("moveThresh = " + moveThresh/valuesForMoveThreshold.Count);
        return moveThresh/valuesForMoveThreshold.Count;
    }

    public float getJumpThresh(){
        jumpThresh = 0;
        for (int i = 0; i < valuesForJumpThreshold.Count; i++){
            jumpThresh += valuesForJumpThreshold[i];
            // Debug.Log("jumpThreshvalue = " + valuesForJumpThreshold[i]);
        }
        // Debug.Log("jumpThresh = " + jumpThresh/valuesForJumpThreshold.Count);
        return jumpThresh/valuesForJumpThreshold.Count;
    }


    private float GetAverageVolume() {

        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);

        foreach (float s in data) {
            a += Mathf.Abs(s);
        }

        return (a/256f);
    }


}
