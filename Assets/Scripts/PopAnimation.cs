using UnityEngine;

public class PopUpAnimation : MonoBehaviour
{
    // Force the final size to be 0.2 on all axes
    private Vector3 targetScale = new Vector3(0.2f, 0.2f, 0.2f);

    void Start()
    {
        // Start completely invisible/tiny
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        // Smoothly grow to exactly 0.2
        if (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * 5f);

            // Snap to target if it gets very close to prevent jitter
            if (Vector3.Distance(transform.localScale, targetScale) < 0.01f)
            {
                transform.localScale = targetScale;
            }
        }
    }
}