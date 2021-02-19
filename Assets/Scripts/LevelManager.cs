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
    
    void Start(){
        currentLevel = 0;
        pm = gameObject.GetComponent<PlayerMovement>();
        pd = gameObject.GetComponent<ProcessData>();
        logWritter = gameObject.GetComponent<LogWritter>();
        playerStats = gameObject.GetComponent<PlayerStats>();
        TMPro.TextMeshProUGUI text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        timer = text.GetComponent<Timer>();
    }

    void Update(){
        // player fell
        if ((rb.position.y <= -6.0f) || ((timer.timeLeft <= 0f) && (currentLevel != 0) && (playerStats.currentCategory != 0)) || timer.timeStay <= 0){
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
        else if (col.collider.tag == "Death") {
            restartCurrentLevel();
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
        removeAllCoins();
    }
    public void removeAllCoins() {
        if (pd.playerCategory == 0) {
            foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
            {
                if (obj.tag == "coin") {
                    Destroy(obj);
                }
            }
        }
    }
}