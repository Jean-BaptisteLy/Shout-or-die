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
    private ElapsedTime elapsedTime;
    // private GameObject canvas;
    
    void Start(){
        currentLevel = 0;
        pm = gameObject.GetComponent<PlayerMovement>();
        logWritter = gameObject.GetComponent<LogWritter>();
        playerStats = gameObject.GetComponent<PlayerStats>();
        TMPro.TextMeshProUGUI text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        elapsedTime = text.GetComponent<ElapsedTime>();
    }

    void Update(){
        // player fell
        if (rb.position.y <= -6.0f){
            restartCurrentLevel();
        }
    }
    // Changing of level
    void OnCollisionEnter2D(Collision2D col){
    	if (col.collider.tag == "end"){
            Debug.Log("Change of level");
            currentLevel++;
            SceneManager.LoadScene("Level " + currentLevel);
            pm.resetPosition();
            logWritter.flushLevelLogger();
            playerStats.updateLevelEndingStats();
            logWritter.writeDownStats();
            logWritter.startNewLevelLogger(currentLevel);
            playerStats.reinitStats();
        }
    }

    public void restartCurrentLevel(){
        Debug.Log("Restarting level");
        SceneManager.LoadScene("Level " + currentLevel);
        pm.resetPosition();
        logWritter.resetLogger(currentLevel);
        playerStats.reinitStats();
        // ElapsedTime.restartElapsedTime();
        elapsedTime.restartElapsedTime();
    }
}