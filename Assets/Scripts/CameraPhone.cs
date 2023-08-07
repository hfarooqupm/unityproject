using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraPhone : MonoBehaviour
{
    // Indica si hay una cámara disponible
    private bool camAvailable;
    // Obtiene la imagen de la cámara del dispositivo
    private WebCamTexture backCam;
    // Elemento de la UI para mostrar la imagen
    public RawImage background;
    // Componente para adaptar la imagen a la resolución de la pantalla
    public AspectRatioFitter fit;

    // Start is called before the first frame update
    void Start()
    {
        // No hay cámara hasta que llegue al final
        camAvailable = false;
        // Se guardan todas las cámaras del dispositivo
        WebCamDevice[] devices = WebCamTexture.devices;
        // Si no hay cámaras, regresa
        if (devices.Length == 0)
        {
        Debug.Log("No se han detectado cámaras."); 
        return;
        }
        // Busca una cámara que no sea frontal
        for (int i = 0; i < devices.Length; i++)
        {
            if (!devices[i].isFrontFacing)
            {
            backCam = new WebCamTexture(devices[i].name, Screen.width,Screen.height);
            }
        }
        // Si no hay cámara trasera, regresa
        if (backCam == null)
        {
        Debug.Log("No encuentra la cámara trasera.");
        return;
        }

        // Ha encontrado una cámara trasera, la inicia 
        // y la asigna a la textura
        backCam.Play();
        background.texture = backCam;
        camAvailable = true;
        
    }

    // Update is called once per frame
    void Update()
    {
            // Si no hay cámara disponible, regresa
        if (!camAvailable)
        return;
        
        // Calcula el ratio y lo asigna a Aspect Ratio Fitter
        float ratio = (float)backCam.width / (float)backCam.height;
        fit.aspectRatio = ratio;
        // Orienta la cámara para que se vea correctamente
        float scaleY = backCam.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scaleY, 1f);
        int orient = -backCam.videoRotationAngle;
        background.rectTransform.localEulerAngles = new Vector3(0,0,orient);
        
    }
}
