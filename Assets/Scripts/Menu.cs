using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    int currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
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
