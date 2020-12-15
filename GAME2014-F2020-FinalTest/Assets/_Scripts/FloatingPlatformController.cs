using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformController : MonoBehaviour
{
    [SerializeField]
    GameObject Platform;

    [SerializeField]
    AudioClip[] audioClips;

    private AudioSource audioSource;
    private BoxCollider2D boxCollider2D;
    private bool triggered = false;
    private bool shrunk = false;
    private float amount = 0.01f;
    private float scale = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered == true)
        {
            transform.localScale = new Vector3(scale -= amount, scale -= amount, 1.0f);

            if (scale <= 0.0f)
            {
                triggered = false;
                shrunk = true;
                Debug.Log("Platform reached smallest value");
                audioSource.clip = audioClips[1];
                audioSource.Play();
            }
        }
        else if (shrunk == true)
        {
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
