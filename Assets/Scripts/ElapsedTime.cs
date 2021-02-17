using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElapsedTime : MonoBehaviour
{
    private TMPro.TextMeshProUGUI text;
    public float elapsedTime = 0f;
    // Start is called before the first frame update
    void Start(){
        text = GetComponent<TMPro.TextMeshProUGUI>();
        
    }

    // Update is called once per frame
    void Update(){
        elapsedTime += Time.deltaTime;
        text.text = "Elapsed time: " + Mathf.Round(elapsedTime);
        
    }
}
