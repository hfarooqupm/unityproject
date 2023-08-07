using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensor2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.name == "Bola2")
            c.GetComponent<MoverBola2>().meta = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
