using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    public Rigidbody2D rb;

    private PlayerMovement pm;
    private LogWritter logWritter;
    private PlayerStats playerStats;
    private TMPro.TextMeshProUGUI text;
    private Timer timer;
    
    void Start(){
        currentLevel = 0;
        pm = gameObject.GetComponent<PlayerMovement>();
        logWritter = gameObject.GetComponent<LogWritter>();
        playerStats = gameObject.GetComponent<PlayerStats>();
        TMPro.TextMeshProUGUI text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        timer = text.GetComponent<Timer>();
    }

    void Update(){
        // player fell
        if ((rb.position.y <= -6.0f) || ((timer.timeLeft <= 0f) && (currentLevel != 0))){
            restartCurrentLevel();
        }
        if (Input.GetKeyDown("o")){
            changeOfLevel();
        }
    }
    // Changing of level
    void OnCollisionEnter2D(Collision2D col){
    	if (col.collider.tag == "end"){
            changeOfLevel();
        }
    }

    public void restartCurrentLevel(){
        Debug.Log("Restarting level");
        SceneManager.LoadScene("Level " + currentLevel);
        pm.resetPosition();
        logWritter.resetLogger(currentLevel);
        playerStats.reinitStats();
        timer.restartTimer();
    }
    public void changeOfLevel(){
        Debug.Log("Change of level");
        currentLevel++;
        SceneManager.LoadScene("Level " + currentLevel);
        pm.resetPosition();
        logWritter.flushLevelLogger();
        playerStats.updateLevelEndingStats();
        playerStats.upgradeLevelNumber();
        logWritter.writeDownStats();
        logWritter.startNewLevelLogger(currentLevel);
        playerStats.reinitStats();
        timer.restartTimer();
    }
}