using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    public Rigidbody2D rb;
    void Start(){
        currentLevel = 0;
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
            PlayerMovement pm = gameObject.GetComponent<PlayerMovement>();
            pm.resetPosition();
        }
    }

    public void restartCurrentLevel(){
        Debug.Log("Restarting level");
        // Destroy(gameObject);
        SceneManager.LoadScene("Level " + currentLevel);
        PlayerMovement pm = gameObject.GetComponent<PlayerMovement>();
        pm.resetPosition();
    }
}