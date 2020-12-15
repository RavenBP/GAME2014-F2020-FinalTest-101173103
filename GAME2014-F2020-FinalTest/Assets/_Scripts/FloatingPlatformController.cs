// FloatingPlatformController.cs
// Created by: Raven Powless - 101173103
// Last Edited: 15/12/20
// Description: Controls Behaviour for Floating/Shrinking Platforms

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformController : MonoBehaviour
{
    
    [SerializeField]
    GameObject Platform; // GameObject (Platform) that will be scaled


    [SerializeField]
    AudioClip[] audioClips; // Array of Audio Clips to play various sounds

    private AudioSource audioSource;
    private BoxCollider2D boxCollider2D;
    private bool triggered = false;
    private bool shrunk = false;
    private float amount = 0.01f; // Amount at which the scale will change
    private float scale = 1.0f; // Original size

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered == true) // Platform has been touched by Player
        {
            // Scale platform down
            transform.localScale = new Vector3(scale -= amount, scale -= amount, 1.0f);

            if (scale <= 0.0f)
            {
                triggered = false;
                shrunk = true;
                audioSource.clip = audioClips[1];
                audioSource.Play();
                Debug.Log("Platform reached smallest value");
            }
        }
        else if (shrunk == true) // Platform has shrunk to smallest size
        {
            // Scale platform to original size
            transform.localScale = new Vector3(scale += amount, scale += amount, 1.0f);

            if (scale >= 1.0f)
            {
                shrunk = false;
                Debug.Log("Platform reached largest value");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            triggered = true;
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
    }
}
