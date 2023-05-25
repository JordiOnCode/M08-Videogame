using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject botonLeft;
    [SerializeField] private GameObject botonRight;
    [SerializeField] private GameObject botonJump;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private Camera camara;
    [SerializeField] private float musicVolumePause;

    private bool juegoEnPausa;
    private float volumenOriginal;

    private void Start()
    {
        volumenOriginal = camara.GetComponent<AudioSource>().volume;
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        botonLeft.SetActive(false);
        botonRight.SetActive(false);
        botonJump.SetActive(false);
        menuPausa.SetActive(true);

        // Desactivar el volumen del audio de la c�mara
        camara.GetComponent<AudioSource>().volume = musicVolumePause;

        juegoEnPausa = true;
    }

    public void Renudar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        botonLeft.SetActive(true);
        botonRight.SetActive(true);
        botonJump.SetActive(true);
        menuPausa.SetActive(false);

        // Restaurar el volumen original del audio de la c�mara si el juego estaba en pausa
        if (juegoEnPausa)
        {
            camara.GetComponent<AudioSource>().volume = volumenOriginal;
            juegoEnPausa = false;
        }
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        botonPausa.SetActive(true);
        botonLeft.SetActive(true);
        botonRight.SetActive(true);
        botonJump.SetActive(true);
        menuPausa.SetActive(false);

        // Restaurar el volumen original del audio de la c�mara si el juego estaba en pausa
        if (juegoEnPausa)
        {
            camara.GetComponent<AudioSource>().volume = volumenOriginal;
            juegoEnPausa = false;
        }

    }
    public void Cerrar()
    {
        Debug.Log("Cerrando Juego...");
        Application.Quit();
    }

}
