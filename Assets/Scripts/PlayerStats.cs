using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int nbCoins = 0;
    public float time = 0f;
    // Possible categories:
    // 0: slow and bad 
    // 1: fast but bad
    // 2: slow but good
    // 3: fast and good
    public int currentCategory;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void addCoin(){
        nbCoins++;
    }

    public void updateTime(float newTime){
        time = newTime;
    }
    
}
