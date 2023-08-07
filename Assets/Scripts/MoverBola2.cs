using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverBola2 : MonoBehaviour
{
    public bool meta = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.deviceOrientation == DeviceOrientation.FaceUp) && (meta==false))
        {
            Vector3 mov = new Vector3(Input.acceleration.x * 10, 0f, Input.acceleration.y * 10);
            GetComponent<Rigidbody>().AddForce(mov, ForceMode.Force);
        }
        if (meta)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().velocity=Vector3.zero;
            
        }
    }
}
