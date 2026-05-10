using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCube : MonoBehaviour
{
    public GameObject popupTextPrefab; // A small UI prefab that says "+10"

    // This is called when your bullet hits the cube's collider
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("I was hit by: " + collision.gameObject.name); // THIS LINE HELPS YOU SEE THE TRUTH
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HandleHit();
        }
    }

    public void HandleHit()
    {
        // 1. Tell manager to add points
        ScoreManager.Instance.AddScore(10);

        // 2. Spawn the +10 message at the cube's position
        if (popupTextPrefab != null)
        {
            Instantiate(popupTextPrefab, transform.position, Quaternion.identity);
        }

        // 3. Destroy the cube
        Destroy(gameObject);
    }
}