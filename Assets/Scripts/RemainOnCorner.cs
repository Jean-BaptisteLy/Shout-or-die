using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainOnCorner : MonoBehaviour
{
    private RectTransform rt;
    public Rigidbody2D rb;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI () {
        rt.localPosition = new Vector3(rb.position.x -374.1f, rb.position.y -179.2f, rt.localPosition.z);
        // GUI.Box (new Rect (Screen.width - 100,0,100,50), "Top-right");    
    }
}
