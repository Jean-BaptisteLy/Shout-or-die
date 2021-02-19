using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    public float elapsedTime = 0f;
    public float timeLeft;
    private PlayerStats ps;
    private ProcessData pd;
    // Start is called before the first frame update
    void Start(){
        text = GetComponent<TMPro.TextMeshProUGUI>();
        ps = GameObject.Find("Player").GetComponent<PlayerStats>();
        //pd = gameObject.GetComponent<ProcessData>();
        pd = GameObject.Find("Player").GetComponent<ProcessData>();
        timeLeft = ps.timeOptimal;
    }

    // Update is called once per frame
    void Update(){
        elapsedTime += Time.deltaTime;
        timeLeft -= Time.deltaTime;
        if (ps.currentLevel == 0){
            text.text = "Elapsed time: " + Mathf.Round(elapsedTime);
        }else{
            // slow and bad
            if(pd.playerCategory != 0) {
                Debug.Log("Timer.cs : playerCategory = 0");
                text.text = "Time left: " + Mathf.Round(timeLeft);
            }else{
                text.text = "Elapsed time: " + Mathf.Round(elapsedTime);
            }
        }
    }

    public void restartTimer(){
        elapsedTime = 0f;
        timeLeft = ps.timeOptimal;
    }
}
