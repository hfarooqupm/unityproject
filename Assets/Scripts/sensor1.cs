using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensor1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.name == "Bola1")
            c.GetComponent<MoverBola1>().meta = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
