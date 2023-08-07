using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotacion : MonoBehaviour
{
    float velocidad;
    void Start()
    {
        velocidad = 5;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 10f, 0f) * velocidad * Time.deltaTime);
    }
}
