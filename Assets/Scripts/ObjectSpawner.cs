using System.Collections;
using System.Collections.Generic; // Important for the List
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject cylinderPrefab;
    public AudioClip popSound;

    // We store the cubes here so we can tell them to float later
    private List<FloatingBehaviour> cubeList = new List<FloatingBehaviour>();

    public int numberOfCubes = 10;
    public int numberOfCylinders = 5;
    public Vector3 spawnAreaSize = new Vector3(1.5f, 0.1f, 0.8f);

    void Start()
    {
        StartCoroutine(SpawnSequence());
    }

    IEnumerator SpawnSequence()
    {
        // 1. Spawn Cubes slowly (Stationary on table)
        for (int i = 0; i < numberOfCubes; i++)
        {
            GameObject cube = SpawnSingleObject(cubePrefab, true, 0.05f);

            // Get your script and keep it in our list
            FloatingBehaviour behavior = cube.GetComponent<FloatingBehaviour>();
            if (behavior != null) cubeList.Add(behavior);

            yield return new WaitForSeconds(0.8f);
        }

        // 2. WAIT: This is your time to grab the VR Gun!
        yield return new WaitForSeconds(2.0f);

        // 3. TRIGGER FLOATING: Tell all stored cubes to start moving
        foreach (FloatingBehaviour cubeScript in cubeList)
        {
            if (cubeScript != null) cubeScript.StartFloating();
        }

        // 4. Spawn Cylinders (Hazards float immediately)
        for (int i = 0; i < numberOfCylinders; i++)
        {
            GameObject cylinder = SpawnSingleObject(cylinderPrefab, false, 0.4f);
            FloatingBehaviour cylScript = cylinder.GetComponent<FloatingBehaviour>();
            if (cylScript != null) cylScript.StartFloating();

            yield return new WaitForSeconds(1.2f);
        }
    }

    GameObject SpawnSingleObject(GameObject prefab, bool isCube, float spawnHeight)
    {
        Vector3 spawnPoint = GetValidSpawnPoint(spawnHeight);
        GameObject obj = Instantiate(prefab, spawnPoint, Quaternion.identity);
        obj.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        // Make sure your script is on the object
        if (obj.GetComponent<FloatingBehaviour>() == null)
            obj.AddComponent<FloatingBehaviour>();

        if (isCube)
            obj.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.7f, 1f);

        // Plays the pop sound at the specific spot the cube appeared
        AudioSource.PlayClipAtPoint(popSound, spawnPoint);

        return obj;
    }

    Vector3 GetValidSpawnPoint(float yOffset)
    {
        Vector3 spawnPoint = Vector3.zero;
        bool validPointFound = false;
        int attempts = 0;

        while (!validPointFound && attempts < 20)
        {
            attempts++;
            spawnPoint = transform.position + new Vector3(
                Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
                yOffset,
                Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
            );

            Collider[] colliders = Physics.OverlapSphere(spawnPoint, 0.25f);
            if (colliders.Length == 0) validPointFound = true;
        }
        return spawnPoint;
    }



    // This helps you see the spawn area in the Scene view (it draws a red box)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawCube(transform.position, spawnAreaSize);
    }
}