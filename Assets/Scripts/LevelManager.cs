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
    private ProcessData pd;
    private LogWritter logWritter;
    private PlayerStats playerStats;
    private TMPro.TextMeshProUGUI text;
    private Timer timer;
    private MenuPause mp;
    
    void Start(){
        currentLevel = 0;
        pm = gameObject.GetComponent<PlayerMovement>();
        pd = gameObject.GetComponent<ProcessData>();
        logWritter = gameObject.GetComponent<LogWritter>();
        playerStats = gameObject.GetComponent<PlayerStats>();
        TMPro.TextMeshProUGUI text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        timer = text.GetComponent<Timer>();
        mp = gameObject.GetComponent<MenuPause>();
    }

    void Update(){
        // player fell
        if ( (rb.position.y <= -6.0f) || ( (timer.timeLeft <= 0f || timer.timeStay <= 0f) && (currentLevel != 0) && (playerStats.currentCategory != 0) ) ){
            restartCurrentLevel();
        }
        if (Input.GetKeyDown("o")){
            changeOfLevel();
        }
        removeAllCoins();
    }
    // Changing of level
    void OnCollisionEnter2D(Collision2D col){
    	if (col.collider.tag == "end"){
            //  Debug.Log("Level while collision: " + currentLevel);
            if (currentLevel == 3){
                // Debug.Log("Finished all levels");
                playerStats.updateLevelEndingStats();
                playerStats.upgradeLevelNumber();
                // playerStats.reinitStats();
                timer.restartTimer();
                logWritter.writeDownStats();
                logWritter.flushLevelLogger();
                logWritter.updateLevelNumber();
                // logWritter.startNewLevelLogger(currentLevel);
                SceneManager.LoadScene("Results");
                GameObject player = GameObject.Find("Player");
                player.SetActive(false);
            }else{
                changeOfLevel();
            }
        }

        else if (col.collider.tag == "Death") {
            restartCurrentLevel();
        }
    }

    public void restartCurrentLevel(){ // OK
        Debug.Log("Restarting current level");
        SceneManager.LoadScene("Level " + currentLevel);
        pm.resetPosition();
        //logWritter.resetLogger(currentLevel);
        playerStats.reinitStats();
        timer.restartTimer();
        logWritter.resetLogger(currentLevel);
    }

    public void changeOfLevel(){ // BUG
        Debug.Log("Change of level");
        currentLevel++;
        SceneManager.LoadScene("Level " + currentLevel);
        pm.resetPosition();
        mp.inverseFeedbackBool();
        //logWritter.flushLevelLogger();
        playerStats.updateLevelEndingStats();
        playerStats.upgradeLevelNumber();
        playerStats.reinitStats();
        timer.restartTimer();
        logWritter.writeDownStats();
        logWritter.flushLevelLogger();
        logWritter.startNewLevelLogger(currentLevel);
        //playerStats.reinitStats();
        //timer.restartTimer();
        //removeAllCoins();
    }
    public void removeAllCoins() {
        if (playerStats.currentCategory == 0 && playerStats.currentLevel != 0) {
            foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject))){
                if (obj.tag == "coin") {
                    Destroy(obj);
                }
            }
        }
    }
}