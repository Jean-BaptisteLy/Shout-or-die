using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    PlayerStats ps;

    void Start(){
        ps = GameObject.Find("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision){
    	if (collision.CompareTag("Player")){
            ps.addCoin();
    		Destroy(gameObject);
    	}
    }
}
