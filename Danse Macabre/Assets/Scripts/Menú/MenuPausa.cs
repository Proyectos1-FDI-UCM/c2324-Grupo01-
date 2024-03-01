using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject menuPausa;
    public void Pausa() 
    {
        Time.timeScale = 0f;   //Para detener el juego
        menuPausa.SetActive(true);
    }
    public void Reanudar() 
    { 
        Time.timeScale = 1f;    //Para arrancar el juego
        menuPausa.SetActive(false);
    }
    public void Reiniciar() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);   //Reiniciar la escena que est?
    }
    public void Quit() 
    {
        SceneManager.LoadScene(0);
    }
}
