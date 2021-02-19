using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    private List<float> valuesForMoveThreshold;
    private List<float> valuesForJumpThreshold;
    private float secondsLeft;

    // Start is called before the first frame update
    void Start(){
        text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        secondsLeft = 5;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown(KeyCode.Return)){
            calibrate();
        }

        
    }

    public void calibrate(){
        text.text = "Generate sound with your average voice volume...\n"; 
        Debug.Log("SecondsLeft: " + secondsLeft + "and" + secondsLeft*10);
        while((secondsLeft > 0) && ((secondsLeft*10)%5 == 0)){
            // get loudness value
            text.text = Mathf.Round(secondsLeft) + "...";
            Debug.Log("Saving loudness info");
            // valuesForMoveThreshold.Add(loudnessValue);
        }
        // yield WaitForSeconds(5);

        // now a little bit louder
    }
    public void startCountDown(){
        secondsLeft -= Time.deltaTime; 
    }
}
