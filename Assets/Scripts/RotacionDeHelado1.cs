using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacionDeHelado1 : MonoBehaviour
{
    float velocidad;
    void Start()
    {
        velocidad = 5;
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 10f) * velocidad * Time.deltaTime);
    }
}
