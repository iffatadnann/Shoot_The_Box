using UnityEngine;

public class PenaltyObject : MonoBehaviour
{
    public AudioClip cylinderHitSound;
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            AudioSource.PlayClipAtPoint(cylinderHitSound, transform.position);
            // Subtract 5 seconds
            ScoreManager.Instance.timeRemaining -= 5f;

            // Visual feedback (Optional: Play a "buzz" sound)
            Debug.Log("Hit penalty object! -5 seconds");

            Destroy(gameObject);
        }
    }
}