using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GPSController : MonoBehaviour
{
    string message = "Inicializando GPS ...";
    float thisLat;
    float thisLong;
    float startingLat;
    float startingLong;
    float destinationLatitude = 31.184603f;
    float destinationLongitude = 29.907371f;
    public static float heading = 0;
    public GameObject compassNeedle;		// Flecha

    public AudioClip destino;
    private AudioSource sonido;
    

    void OnGUI(){
        GUI.skin.label.fontSize = 60;
        GUI.Label(new Rect(30,30,1000,1000), message);
    }

    IEnumerator StartGPS(){
        message = "Iniciando";
        if(!Input.location.isEnabledByUser){
            message = "Servicio GPS No Disponible";
            yield break;
        }

        // Start service before querying location
        Input.location.Start(25,0); // margen de error, displacement(How often it needs to update)

        // Wait until service initializes
        int maxWait = 5;
        while(Input.location.status == LocationServiceStatus.Initializing && maxWait >0){
            yield return new WaitForSeconds(1);
            maxWait--;
        }
        if(maxWait < 1){
            message = "Timed out";
            yield break;
        }

        // Connection has failed 
        if(Input.location.status == LocationServiceStatus.Failed){
            message = "No es posible detectar la localizacion";
            yield break;
        }else{
            Input.compass.enabled = true;
            message = "Location: " + Input.location.lastData.latitude + " " +
                                    Input.location.lastData.longitude + " " +
                                    Input.location.lastData.altitude + " " + Input.compass.trueHeading;
            startingLat = Input.location.lastData.latitude;
            startingLong = Input.location.lastData.longitude;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
     StartCoroutine(StartGPS());
     sonido = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        thisLat = Input.location.lastData.latitude;
        thisLong = Input.location.lastData.longitude;
        float distanceToDestination = Haversine(thisLat, thisLong, destinationLatitude, destinationLongitude);
        message = "\nDistancia hasta el destino: " + distanceToDestination;

        float bearing = angleFromCoordinate(thisLat, thisLong, destinationLatitude, destinationLongitude);
         
        compassNeedle.transform.localRotation = Quaternion.Slerp(compassNeedle.transform.localRotation, Quaternion.Euler(0, 0, Input.compass.magneticHeading + bearing), 100f);

        if(distanceToDestination <= 10.00f){
            sonido.PlayOneShot(destino); // reproduce sonido si estamos a menos de 10 metros del destino
        }
		
		// las siguientes condiciones sirven para agrandar el gameObject flecha.

        if(distanceToDestination == 100.0f){
            compassNeedle.transform.localScale += new Vector3(1, 1, 1);

        }
        if(distanceToDestination == 70.0f){
            compassNeedle.transform.localScale += new Vector3(1, 1, 1);
        }

        if(distanceToDestination == 50.0f){ 
            compassNeedle.transform.localScale += new Vector3(1, 1, 1);
        }

        if(distanceToDestination == 20.0f){
            compassNeedle.transform.localScale += new Vector3(1, 1, 1);
        }

    }

    float Haversine(float lat1, float long1, float lat2, float long2){

    	// lat1 y long1 coordenadas del punto inicial
    	// lat2 and long2 coordenadas del punto de destino
    	// retornamos distancia entre localizaciones
    	float earthRad = 6371000;
    	float lRad1 = lat1 * Mathf.Deg2Rad;
    	float lRad2 = lat2 * Mathf.Deg2Rad;
    	float dLat = (lat2 - lat1) * Mathf.Deg2Rad;
    	float dLong = (long2 - long1) * Mathf.Deg2Rad;
    	float a = Mathf.Sin(dLat / 2.0f) * Mathf.Sin(dLat / 2.0f) + Mathf.Cos(lRad1) * Mathf.Cos(lRad2) * Mathf.Sin(dLong / 2.0f) * Mathf.Sin(dLong / 2.0f);
    	float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));

    	return earthRad * c; // en metros 
    }



	// funcion que calcula el angulo entre las coordenadas del punto inicial y las coordenadas del punto de destino
     private float angleFromCoordinate(float lat1, float long1, float lat2, float long2) {
         lat1 *= Mathf.Deg2Rad;
         lat2 *= Mathf.Deg2Rad;
         long1 *= Mathf.Deg2Rad;
         long2 *= Mathf.Deg2Rad;
 
         float dLon = (long2 - long1);
         float y = Mathf.Sin(dLon) * Mathf.Cos(lat2); 
         float x = (Mathf.Cos(lat1) * Mathf.Sin(lat2)) - (Mathf.Sin(lat1) * Mathf.Cos(lat2) * Mathf.Cos(dLon));
         float brng = Mathf.Atan2(y, x); 
         brng = Mathf.Rad2Deg* brng; 
         brng = (brng + 360) % 360; 
         brng = 360 - brng;
         return brng;
     }
}
