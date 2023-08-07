using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrujulaController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Input.location.Start();
        Input.compass.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,-Input.compass.trueHeading,0);
    }
}
