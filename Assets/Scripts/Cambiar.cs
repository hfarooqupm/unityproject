using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambiar : MonoBehaviour
{
    // Start is called before the first frame update
    public void salir()
    {
        Application.Quit();
        Debug.Log("Cerrada la Aplicacion");

    }
    public void CambiarEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }
    public void CambiarEscena(int escena)
    {
        SceneManager.LoadScene(escena);
    }
}
