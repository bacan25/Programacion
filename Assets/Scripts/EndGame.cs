using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    int currentScene;
    bool hasBoy = false;
    bool hasBattery = false;


    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player") && hasBoy && hasBattery)
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);

        } else if(col.CompareTag("Bateria")){
            hasBattery = true;
            print(hasBattery);
        } else if(col.CompareTag("Niño")){
            hasBoy = true;
            print(hasBoy);

        }
    }
}
