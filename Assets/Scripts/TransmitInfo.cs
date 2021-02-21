using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransmitInfo : MonoBehaviour{

    public float ratioCoins;
    private float ratioTime;
    private float curveScore;
    private int playerCategory;
    private TMPro.TextMeshProUGUI text;
    void Start(){
        
    }

    void Update(){
        if (SceneManager.GetActiveScene().name == "Results"){
            displayOverviewResults();
        }
    }

    public void setInfo(int playerCategory, float ratioCoins, float ratioTime, float curveScore){
        this.playerCategory = playerCategory;
        this.ratioCoins = ratioCoins;
        this.ratioTime = ratioTime;
        this.curveScore = curveScore;
    }

    public void displayOverviewResults(){
        text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        text.text = "Final results:\n" + "\nPlayer category:"+ playerCategory + "\nCoins ratio: " + ratioCoins + "\nTime ratio: " + ratioTime + "\nCurve score: " + curveScore + "\n *See Jupyter Notebook to display the curve*";
    }
}
