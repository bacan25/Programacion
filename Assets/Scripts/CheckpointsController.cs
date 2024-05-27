using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckpointsController : MonoBehaviour
{
    //Checkpoints (triggers)
    [SerializeField] GameObject check1;
    [SerializeField] GameObject check2;
    [SerializeField] GameObject check3;
    [SerializeField] GameObject check4;

    //Objetos
    public CollectibleItems key;
    public CollectibleItems crossb;
    [SerializeField] GameObject linterna;

    //Booooleanos
    private bool checkpoint1Pressed = false;
    [HideInInspector] public bool checkpoint2Pressed = false;
    private bool checkpoint3Pressed = false;
    private bool checkpoint4Pressed = false;

    [SerializeField] private GameObject player;

    //Primer checkpoint (Por la fogata)
    [SerializeField] GameObject canvaLinterna;
    [SerializeField] GameObject enemyInt;
    [SerializeField] AudioSource campana;

    //Segundo checkpoint (Parte de atras)
    [SerializeField] GameObject enemyExt;

    //Final
    public RutaEnemigo finalEnemy;
    [SerializeField] GameObject endGame;

    void Start()
    {
        //Verificar guardados
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            player.transform.position = new Vector3(x, y, z);
        }

        checkpoint1Pressed = PlayerPrefs.GetInt("Checkpoint1Pressed", 0) == 1;
        checkpoint2Pressed = PlayerPrefs.GetInt("Checkpoint2Pressed", 0) == 1;
        checkpoint3Pressed = PlayerPrefs.GetInt("Checkpoint3Pressed", 0) == 1;
        checkpoint4Pressed = PlayerPrefs.GetInt("Checkpoint4Pressed", 0) == 1;

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
        if (checkpoint3Pressed)
        {
            check3.SetActive(false);
        }
        if (checkpoint4Pressed)
        {
            check4.SetActive(false);
        }
    }

    void Update()
    {
        // Aquí podrías agregar lógica si es necesario
    }

    private void OnTriggerEnter(Collider col)
    {
        //Comprueba que Trigger toco el pana
        if (col.CompareTag("Player"))
        {
            switch (gameObject.name)
            {
                case "Checkpoint#1":
                    Checkpoint1();
                    break;
                case "Checkpoint#2":
                    if (key._hasFlash)
                    {
                        Checkpoint2();
                    }
                    break;
                case "Checkpoint#3":
                    Checkpoint3();
                    break;
                case "Checkpoint#4":
                    Checkpoint4();
                    break;
                case "InicioFinal":
                    Final();
                    break;
            }
        }
    }

    void Checkpoint1()
    {
        checkpoint1Pressed = true;
        
        key.PickFlash();

        PlayerPrefs.SetInt("Checkpoint1Pressed", checkpoint1Pressed ? 1 : 0);

        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
        PlayerPrefs.SetString("LastCheckpoint", SceneManager.GetActiveScene().name);

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
        PlayerPrefs.SetString("LastCheckpoint", SceneManager.GetActiveScene().name);

        enemyInt.SetActive(false);
        PlayerPrefs.SetInt("EnemyIntActive", 0);

        enemyExt.SetActive(true);
        PlayerPrefs.SetInt("EnemyExtActive", 1);

        check2.SetActive(false);

        PlayerPrefs.Save();
    }
    
    void Checkpoint3()
    {
        checkpoint3Pressed = true;
        PlayerPrefs.SetInt("Checkpoint3Pressed", checkpoint3Pressed ? 1 : 0);

        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
        PlayerPrefs.SetString("LastCheckpoint", SceneManager.GetActiveScene().name);

        enemyInt.SetActive(true);
        PlayerPrefs.SetInt("EnemyIntActive", 1);

        enemyExt.SetActive(false);
        PlayerPrefs.SetInt("EnemyExtActive", 0);

        check3.SetActive(false);

        PlayerPrefs.Save();
    }
    
    void Checkpoint4()
    {
        checkpoint4Pressed = true;
        PlayerPrefs.SetInt("Checkpoint4Pressed", checkpoint4Pressed ? 1 : 0);

        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
        PlayerPrefs.SetString("LastCheckpoint", SceneManager.GetActiveScene().name);

        check4.SetActive(false);

        PlayerPrefs.Save();
    }

    void Final()
    {
        endGame.SetActive(true);
        linterna.SetActive(false);
        canvaLinterna.SetActive(false);
        finalEnemy.final = true;
        finalEnemy.rangoEnemigo = 5000;
        print("Final activado");
    }
}
