using UnityEngine;

public class PenaltyObject : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Subtract 5 seconds
            ScoreManager.Instance.timeRemaining -= 5f;

            // Visual feedback (Optional: Play a "buzz" sound)
            Debug.Log("Hit penalty object! -5 seconds");

            Destroy(gameObject);
        }
    }
}