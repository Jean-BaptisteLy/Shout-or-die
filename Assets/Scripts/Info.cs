using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour {
    private static Info _instance;

    public static Info Instance { get { return _instance; } }


    private void Awake(){
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
}