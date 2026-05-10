using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingScore : MonoBehaviour
{
    void Start() {
        Destroy(gameObject, 1.5f); 
    }
    void Update() {
        transform.Translate(Vector3.up * Time.deltaTime); 
    }
}
