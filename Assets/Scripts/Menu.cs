using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    int currentScene;
    public GameObject continueButton; // Asigna este botón en el inspector

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        // Verifica si hay datos guardados
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            continueButton.SetActive(true); // Muestra el botón de continuar
        }
        else
        {
            continueButton.SetActive(false); // Oculta el botón de continuar si no hay datos guardados
        }
    }

    public void StartGame()
    {
        // Borra todos los datos guardados al iniciar un nuevo juego
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        // Carga la escena guardada
        if (PlayerPrefs.HasKey("LastCheckpoint"))
        {
            string checkpointScene = PlayerPrefs.GetString("LastCheckpoint");
            SceneManager.LoadScene(checkpointScene);
        }
    }

    public void Exit()
    {
        Debug.Log("Hablamos pa");
        Application.Quit();
    }
   
    public void NextScene()
    {
        SceneManager.LoadScene(currentScene + 1);
    }
}
