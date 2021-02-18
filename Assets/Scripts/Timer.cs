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
    // Start is called before the first frame update
    void Start(){
        text = GetComponent<TMPro.TextMeshProUGUI>();
        ps = GameObject.Find("Player").GetComponent<PlayerStats>();
        timeLeft = ps.timeOptimal;
    }

    // Update is called once per frame
    void Update(){
        elapsedTime += Time.deltaTime;
        timeLeft -= Time.deltaTime;
        if (ps.currentLevel == 0){
            text.text = "Elapsed time: " + Mathf.Round(elapsedTime);
        }else{
            text.text = "Time left: " + Mathf.Round(timeLeft);
        }
    }

    public void restartTimer(){
        elapsedTime = 0f;
        timeLeft = ps.timeOptimal;
    }
}
