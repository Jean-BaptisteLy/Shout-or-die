using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration : MonoBehaviour
{
    TMPro.TextMeshProUGUI text;
    Timer timer;

    // Start is called before the first frame update
    void Start(){
        text = GameObject.Find("Canvas").GetComponentInChildren<TMPro.TextMeshProUGUI>();
        timer = text.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update(){
        
    }
}
