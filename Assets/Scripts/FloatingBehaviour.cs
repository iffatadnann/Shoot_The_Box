using UnityEngine;

public class FloatingBehaviour : MonoBehaviour
{
    public bool isFloating = false;
    private float startY;
    private float fixedX;
    private float fixedZ;

    private float speed;
    private float floatStrength = 0.2f;

    void Start()
    {
        // Capture the spawn point (which should be just above the table)
        startY = transform.position.y;
        fixedX = transform.position.x;
        fixedZ = transform.position.z;

        speed = Random.Range(1.2f, 2.0f);
    }

    void Update()
    {
        if (!isFloating) return;

        // Using Mathf.Abs(Mathf.Sin) makes the value bounce between 0 and 1
        // This means the cube only moves UP from the table, never DOWN into it.
        float bounce = Mathf.Abs(Mathf.Sin(Time.time * speed));
        float newY = startY + (bounce * floatStrength);

        transform.position = new Vector3(fixedX, newY, fixedZ);

        transform.Rotate(Vector3.up * Time.deltaTime * 25f);
    }

    public void StartFloating()
    {
        fixedX = transform.position.x;
        fixedZ = transform.position.z;
        startY = transform.position.y;
        isFloating = true;
    }
}