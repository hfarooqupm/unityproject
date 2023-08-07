using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiroscopoController : MonoBehaviour
{
    // Quads para las 6 caras del cubo
    private GameObject[] quads = new GameObject[6];

    // Texturas para cada cara, se corresponden con +X, +Y, +Z, -X, -Y, -Z
    // con los colores apropiados verde rojo y azul
    public Texture[] labels;
    // Start is called before the first frame update
    void Start()
    {
        // Activa el giroscopio si lo lleva el dispositivo
        if (SystemInfo.supportsGyroscope)
        Input.gyro.enabled = true;

        // Se sitúa la cámara en el origen y se la da un color de fondo sólido
        GetComponent<Camera>().backgroundColor = new Color(0.19f, 0.30f, 0.47f);
        GetComponent<Camera>().transform.position = new Vector3(0, 0, 0);
        GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;

        // Se crean los 6 quads para las caras del cubo
        GameObject quad = GameObject.CreatePrimitive(PrimitiveType.Quad);
        quads[0] = createQuad(quad, new Vector3(1, 0, 0), new Vector3(0, 90, 0), "plus x",
        new Color(0.90f, 0.10f, 0.10f, 1), labels[0]);
        quads[1] = createQuad(quad, new Vector3(0, 1, 0), new Vector3(-90, 0, 0), "plus y",
        new Color(0.10f, 0.90f, 0.10f, 1), labels[1]);
        quads[2] = createQuad(quad, new Vector3(0, 0, 1), new Vector3(0, 0, 0), "plus z",
        new Color(0.10f, 0.10f, 0.90f, 1), labels[2]);
        quads[3] = createQuad(quad, new Vector3(-1, 0, 0), new Vector3(0, -90, 0), "neg x",
        new Color(0.90f, 0.50f, 0.50f, 1), labels[3]);
        quads[4] = createQuad(quad, new Vector3(0, -1, 0), new Vector3(90, 0, 0), "neg y",
        new Color(0.50f, 0.90f, 0.50f, 1), labels[4]);
        quads[5] = createQuad(quad, new Vector3(0, 0, -1), new Vector3(0, 180, 0), "neg z",
        new Color(0.50f, 0.50f, 0.90f, 1), labels[5]);
        GameObject.Destroy(quad);
    }

    // Crea un quad para un lado del cubo con color y textura
    GameObject createQuad(GameObject quad, Vector3 pos, Vector3 rot, string name, Color col, Texture t){
        Quaternion quat = Quaternion.Euler(rot);
        GameObject GO = Instantiate(quad, pos, quat);
        GO.name = name;
        GO.GetComponent<Renderer>().material.color = col;
        GO.GetComponent<Renderer>().material.mainTexture = t;
        GO.transform.localScale += new Vector3(0.25f, 0.25f, 0.25f);
        return GO;
    }

    // Update is called once per frame
    // El giróscopo se orienta con mano derecha y Unity con mano izquierda. Hay que convertirlo
    void Update()
    {
        //Aplica la inclinación directamente a un objeto
        transform.rotation = GyroToUnity(Input.gyro.attitude);
    }
    private static Quaternion GyroToUnity(Quaternion q){
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }
}
