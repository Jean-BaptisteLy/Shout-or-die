using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoryText : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TextMeshProUGUI text;
    private GameObject player;
    private PlayerStats playerStats;
    void Start(){
        text = GetComponent<TMPro.TextMeshProUGUI>();
        // to change and get from level-specific info 
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update(){
        Debug.Log("Current level: " + playerStats.currentLevel);
        if(playerStats.currentLevel!=0){
            text.text = "Player category: " + playerStats.currentCategory;
        }
        
    }
}
