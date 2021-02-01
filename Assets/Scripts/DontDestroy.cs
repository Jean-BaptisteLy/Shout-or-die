using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{   
     //GameObject player;
    // Start is called before the first frame update
    void Awake(){
        //player = GameObject.Find("Player");
        //if (player != null){
            DontDestroyOnLoad(this.gameObject);
        //}
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
