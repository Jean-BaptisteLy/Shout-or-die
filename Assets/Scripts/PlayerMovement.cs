using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Base qui permet de se déplacer au clavier
    public float moveSpeed;
    public float jumpForce; // 300 max
    public bool isJumping = false;
    public Rigidbody2D rb;
    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    // Microphone
    public float sensitivity = 100f;
    public float loudness = 0f;
    public float jumpLoudnessThreshold;
    public float runLoudnessThreshold;
    AudioSource _audio;

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
        //Debug.Log ("loudness : "+loudness);
        //Debug.Log ("jumpLoudnessThreshold : "+jumpLoudnessThreshold);
        if (loudness >= jumpLoudnessThreshold) {
            Debug.Log("---------- Je saute ! ----------");
            Debug.Log ("loudness : "+loudness);
            Debug.Log ("jumpLoudnessThreshold : "+jumpLoudnessThreshold);
            //rb.AddForce( Vector3.up * jumpForce);
            isJumping = true;
            horizontalMovement = moveSpeed * loudness * Time.deltaTime;
        }
        else if (loudness >= runLoudnessThreshold) {
            horizontalMovement = moveSpeed * loudness * Time.deltaTime;
        }
        else {  // Déplacement au clavier
            Debug.Log("---------- Clavier ! ----------");
            horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            if (Input.GetButtonDown("Jump"))
            {
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