using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    PlayerStats ps;

    // Start is called before the first frame update
    void Start(){
        ps = GameObject.Find ("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    	if (collision.CompareTag("Player")){
            ps.addCoin();
    		Destroy(gameObject);
    	}
    }
}
