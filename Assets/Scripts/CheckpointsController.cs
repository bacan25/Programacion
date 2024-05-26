using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointsController : MonoBehaviour
{
    //Checkpoints (triggers)
    [SerializeField] GameObject check1;
    [SerializeField] GameObject check2;
    //[SerializeField] GameObject check3;
    //[SerializeField] GameObject check4;

    private bool checkpoint1Pressed;
    private bool checkpoint2Pressed = false;
    private bool checkpoint3Pressed = false;
    private bool checkpoint4Pressed = false;

    [SerializeField] private GameObject player;

    //Primer checkpoint (Por la fogata)
    [SerializeField] GameObject linterna;
    [SerializeField] GameObject canvaLinterna;
    [SerializeField] GameObject enemyInt;
    [SerializeField] AudioSource campana;

    //Segundo checkpoint (Parte de atras)
    [SerializeField] GameObject enemyExt;
    
    void Start()
    {
        //Borra Playerprefs
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            player.transform.position = new Vector3(x, y, z);
        }

        checkpoint1Pressed = PlayerPrefs.GetInt("Checkpoint1Pressed", 0) == 1;
        checkpoint2Pressed = PlayerPrefs.GetInt("Checkpoint2Pressed", 0) == 1;

        linterna.SetActive(PlayerPrefs.GetInt("LinternaActive", 0) == 1);
        canvaLinterna.SetActive(PlayerPrefs.GetInt("CanvaLinternaActive", 0) == 1);

        enemyInt.SetActive(PlayerPrefs.GetInt("EnemyIntActive", 0) == 1);
        enemyExt.SetActive(PlayerPrefs.GetInt("EnemyExtActive", 0) == 1);


        if (checkpoint1Pressed)
        {
            check1.SetActive(false);
        }
        if (checkpoint2Pressed)
        {
            check2.SetActive(false);
        }
    }

    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (gameObject.name)
            {
                case "Checkpoint#1":
                    Checkpoint1();
                    break;
                case "Checkpoint#2":
                    Checkpoint2(); 
                    break;
                case "Checkpoint#3":
                    Checkpoint3();
                    break;
                case "Checkpoint#4":
                    Checkpoint4();
                    break;
            }
        }

    }

    void Checkpoint1()
    {
        checkpoint1Pressed = true;
        PlayerPrefs.SetInt("Checkpoint1Pressed", checkpoint1Pressed ? 1 : 0);

        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);

        linterna.SetActive(true);
        PlayerPrefs.SetInt("LinternaActive", 1);

        canvaLinterna.SetActive(true);
        PlayerPrefs.SetInt("CanvaLinternaActive", 1);

        enemyInt.SetActive(true);
        PlayerPrefs.SetInt("EnemyIntActive", 1);

        check1.SetActive(false);

        campana.Play();

        PlayerPrefs.Save();

    }
    
    void Checkpoint2()
    {
        checkpoint2Pressed = true;
        PlayerPrefs.SetInt("Checkpoint2Pressed", checkpoint2Pressed ? 1 : 0);

        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);


        enemyInt.SetActive(false);
        PlayerPrefs.SetInt("EnemyIntActive", 0);

        enemyExt.SetActive(true);
        PlayerPrefs.SetInt("EnemyExtActive", 1);

        check2.SetActive(false);

        PlayerPrefs.Save();
    }
    
    void Checkpoint3()
    {
        print("Holi3");
    }
    
    void Checkpoint4()
    {
        print("Holi4");
    }
}
