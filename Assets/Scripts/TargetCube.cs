using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCube : MonoBehaviour
{
    public GameObject popupTextPrefab;

    // 1. Just declare the variable here
    private AudioSource audioSource;

    void Start()
    {
        // 2. Initialize it when the game starts
        audioSource = GetComponent<AudioSource>();
      
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I was hit by: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HandleHit();
        }
    }

    public void HandleHit()
    {
        ScoreManager.Instance.AddScore(10);

        if (popupTextPrefab != null)
        {
            Instantiate(popupTextPrefab, transform.position, Quaternion.identity);
        }

        // 3. Play the sound here before the object is destroyed
        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
        }

        // NOTE: If you destroy the object immediately, the sound will cut off!
        // To fix that, we hide the cube and destroy it after 1 second.
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, 1.0f);
    }
}