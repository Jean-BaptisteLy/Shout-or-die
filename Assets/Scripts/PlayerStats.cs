using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class PlayerStats : MonoBehaviour
{
    public int nbCoins;
    public double totalTime;
    public DateTime startingTime;
    public DateTime endingTime;
    // Possible categories:
    // 0: slow and bad 
    // 1: fast but bad
    // 2: slow but good
    // 3: fast and good
    public int currentCategory;
    // Start is called before the first frame update
    void Start(){
        nbCoins = 0;
        startingTime = DateTime.Now;
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void addCoin(){
        nbCoins++;
    }

    public void reinitStats(){
        nbCoins = 0;
        startingTime = DateTime.Now;
    }

    public Tuple<int, double> getStats(){
        return Tuple.Create(this.nbCoins, this.totalTime);
    }

    public void updateLevelEndingStats(){
        endingTime = DateTime.Now;
        totalTime = (endingTime - startingTime).TotalSeconds;
        // write this info on log file
        
    }


    
}
