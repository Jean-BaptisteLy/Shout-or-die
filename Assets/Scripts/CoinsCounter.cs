using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsCounter : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    private int collectedCoins;
    private int totalCoins;
    private GameObject player;
    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start(){
        text = GetComponent<TMPro.TextMeshProUGUI>();
        // to change and get from level-specific info 
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        totalCoins = playerStats.coinsTotal;
    }

    // Update is called once per frame
    void Update(){
        collectedCoins = playerStats.nbCoins;
        totalCoins = playerStats.coinsTotal;
        // Debug.Log("Coins counter, total coins: " + totalCoins);
        text.text = "Coins: " + collectedCoins + "/" + totalCoins;
    }

}
