using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int currentLevel;
    void Start(){
        currentLevel = 0;
    }
    void OnCollisionEnter2D(Collision2D col){
    	if (col.collider.tag == "end"){
            Debug.Log("Change of level");
            currentLevel++;
            SceneManager.LoadScene("Level " + currentLevel);
            //Application.LoadLevelAdditive(1); //1 is the build order it could be 1065 for you if you have that many scenes
        }
    }

    public void restartCurrentLevel(){
        Debug.Log("Restarting level");
        SceneManager.LoadScene("Level " + currentLevel);
    }
}