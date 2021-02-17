using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainOnCorner : MonoBehaviour
{
    private RectTransform rt;
    public Rigidbody2D rb;
    public GameObject player;

    public float xOffset;
    public float yOffset;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
        player = GameObject.Find("Player");
        rb = player.GetComponent<Rigidbody2D>();
        xOffset = rt.localPosition.x;
        yOffset = rt.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI () {
        rt.localPosition = new Vector3(rb.position.x + xOffset, rb.position.y +yOffset, rt.localPosition.z);  
    }
}
