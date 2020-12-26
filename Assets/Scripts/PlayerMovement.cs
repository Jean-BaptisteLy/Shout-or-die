using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    // Base qui permet de se déplacer au clavier
    public float moveSpeed;
    public float jumpForceKeyboard = 300.0f; // 300 max    
    public bool isJumping = false;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    public float jumpForce;

    // Microphone
    public float jumpForceMicrophone = 10.0f; // 50 max
    public float sensitivity = 100f;
    public float loudness = 0f;
    public float jumpLoudnessThreshold;
    public float runLoudnessThreshold;
    AudioSource _audio;

    // Tiles
    public Tile tile;
    public Tilemap map;
    private Vector3Int previous;

    void Start()
    {
        // Déplacement au microphone
        foreach (var device in Microphone.devices)
        {
            Debug.Log("Name: " + device);
            //Debug.Log("Microphone.GetDeviceCaps : "+device.GetDeviceCaps);
        }

        //Debug.Log(Microphone.GetDeviceCaps);
        rb = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _audio.clip = Microphone.Start(null, true, 10, 44100);
        //_audio.clip = Microphone.Start("Microphone Array (Realtek High Definition Audio)", true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;
        while (!(Microphone.GetPosition(null) > 0)) {

        }

        _audio.Play();
    }

    void Update()
    {
        // Déplacement au microphone
        //Debug.Log("GetAverageVolume : "+GetAverageVolume());
        loudness = GetAverageVolume() * sensitivity;
        jumpForce = jumpForceMicrophone;
        //Debug.Log ("loudness : "+loudness);
        //Debug.Log ("jumpLoudnessThreshold : "+jumpLoudnessThreshold);
        if (loudness >= jumpLoudnessThreshold) {
            Debug.Log("---------- Je saute ! ----------");
            Debug.Log ("loudness : "+loudness);
            Debug.Log ("jumpLoudnessThreshold : "+jumpLoudnessThreshold);
            //rb.AddForce( Vector3.up * jumpForce);
            jumpForce = jumpForceMicrophone;
            isJumping = true;
            horizontalMovement = moveSpeed * Time.deltaTime;
        }
        else if (loudness >= runLoudnessThreshold) {
            horizontalMovement = moveSpeed * Time.deltaTime;
        }
        else {  // Déplacement au clavier
            Vector3Int currentCell = map.WorldToCell(transform.position);
            currentCell.x += 1;
            Debug.Log("---------- Clavier ! ----------");
            horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
                //map.SetTile(currentCell, tile);
                jumpForce = jumpForceKeyboard;
                isJumping = true;
            }
        }
        MovePlayer(horizontalMovement);
        /*
        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }
        */
    }

    void LateUpdate()
    {
        /*
        Vector3Int currentCell = map.WorldToCell(transform.position);
        currentCell.x += 1;
        if(currentCell != previous)
        {
            // set the new tile
            map.SetTile(currentCell, tile);
 
            // erase previous
            map.SetTile(previous, null);
 
            // save the new position for next frame
            previous = currentCell;
        }
        */
    }

    void FixedUpdate()
    {
        // Déplacement au clavier
        //MovePlayer(horizontalMovement);
    }

    // Déplacement au clavier
    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
    
        if (isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    // Déplacement au microphone
    float GetAverageVolume() {

        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0);

        foreach (float s in data) {
            a += Mathf.Abs(s);
        }

        return (a/256f);
    }
}