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
    /* public CollectibleItems key;
    public CollectibleItems crossb; */
    [SerializeField] GameObject linterna;
    [SerializeField] GameObject bateria;
    [SerializeField] GameObject mike;

    //Pistas
    public GameObject clue3;
    public GameObject clue4;

    //Level manager
    private LevelManager levelM;

    [SerializeField] private GameObject player;

    //Primer checkpoint (Por la fogata)
    [SerializeField] GameObject canvaLinterna;
    [SerializeField] GameObject enemyInt;
    [SerializeField] GameObject lucesCoche;
    [SerializeField] AudioSource campana;

    //Segundo checkpoint (Parte de atras)
    [SerializeField] GameObject enemyExt;

    //Final
    public RutaEnemigo finalEnemy;
    [SerializeField] GameObject endGame;
    [SerializeField] GameObject triggerFinal;

    void Awake()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void Start()
    {
        //Acceso Singleton
        levelM = LevelManager.Instance;

        if(PlayerPrefs.HasKey("LucesActive"))
        {
            lucesCoche.SetActive(PlayerPrefs.GetInt("LucesActive", 0) == 1);
        }

        levelM.checkpoint1Pressed = PlayerPrefs.GetInt("Checkpoint1Pressed", 0) == 1;
        levelM.checkpoint2Pressed = PlayerPrefs.GetInt("Checkpoint2Pressed", 0) == 1;
        levelM.checkpoint3Pressed = PlayerPrefs.GetInt("Checkpoint3Pressed", 0) == 1;
        levelM.checkpoint4Pressed = PlayerPrefs.GetInt("Checkpoint4Pressed", 0) == 1;

        //linterna.SetActive(PlayerPrefs.GetInt("LinternaActive", 0) == 1);
        canvaLinterna.SetActive(PlayerPrefs.GetInt("CanvaLinternaActive", 0) == 1);
        clue4.SetActive(PlayerPrefs.GetInt("Clue4", 0) == 1);

        enemyInt.SetActive(PlayerPrefs.GetInt("EnemyIntActive", 0) == 1);
        enemyExt.SetActive(PlayerPrefs.GetInt("EnemyExtActive", 0) == 1);

        if (levelM.checkpoint1Pressed)
        {
            check1.SetActive(false);
        }
        if (levelM.checkpoint2Pressed)
        {
            check2.SetActive(false);
        }
        if (levelM.checkpoint3Pressed)
        {
            check3.SetActive(false);
        }
        if (levelM.checkpoint4Pressed)
        {
            check4.SetActive(false);
        }
    }

    void Update()
    {
       
    }


    public void Checkpoint1()
    {
        levelM.checkpoint1Pressed = true;
        clue3.SetActive(true);
        //key.PickFlash();

        PlayerPrefs.SetInt("Checkpoint1Pressed", levelM.checkpoint1Pressed ? 1 : 0);

        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x + 1);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
        PlayerPrefs.SetString("LastCheckpoint", SceneManager.GetActiveScene().name);

        //linterna.SetActive(true);
        PlayerPrefs.SetInt("LinternaActive", 1);

        lucesCoche.SetActive(false);
        PlayerPrefs.SetInt("LucesActive", 0);

        canvaLinterna.SetActive(true);
        PlayerPrefs.SetInt("CanvaLinternaActive", 1);

        enemyInt.SetActive(true);
        PlayerPrefs.SetInt("EnemyIntActive", 1);

        check1.SetActive(false);

        campana.Play();

        PlayerPrefs.Save();
    }
    
    public void Checkpoint2()
    {
        levelM.checkpoint2Pressed = true;
        clue4.SetActive(true);
        PlayerPrefs.SetInt("Clue4", 1);

        PlayerPrefs.SetInt("Checkpoint2Pressed", levelM.checkpoint2Pressed ? 1 : 0);

        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y + 5);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
        PlayerPrefs.SetString("LastCheckpoint", SceneManager.GetActiveScene().name);

        enemyInt.SetActive(false);
        PlayerPrefs.SetInt("EnemyIntActive", 0);

        enemyExt.SetActive(true);
        PlayerPrefs.SetInt("EnemyExtActive", 1);

        check2.SetActive(false);

        PlayerPrefs.Save();
    }
    
    public void Checkpoint3()
    {
        levelM.checkpoint3Pressed = true;
        PlayerPrefs.SetInt("Checkpoint3Pressed", levelM.checkpoint3Pressed ? 1 : 0);

        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y + 1);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
        PlayerPrefs.SetString("LastCheckpoint", SceneManager.GetActiveScene().name);

        enemyInt.SetActive(true);
        PlayerPrefs.SetInt("EnemyIntActive", 1);

        enemyExt.SetActive(false);
        PlayerPrefs.SetInt("EnemyExtActive", 0);

        check3.SetActive(false);

        PlayerPrefs.Save();
    }
    
    public void Checkpoint4()
    {
        levelM.checkpoint4Pressed = true;
        PlayerPrefs.SetInt("Checkpoint4Pressed", levelM.checkpoint4Pressed ? 1 : 0);

        PlayerPrefs.SetFloat("PlayerPosX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", player.transform.position.y + 2);
        PlayerPrefs.SetFloat("PlayerPosZ", player.transform.position.z);
        PlayerPrefs.SetString("LastCheckpoint", SceneManager.GetActiveScene().name);

        check4.SetActive(false);

        PlayerPrefs.Save();
    }

    public void Final()
    {
        endGame.SetActive(true);
        //linterna.SetActive(false);
        bateria.SetActive(true);
        mike.SetActive(true);
        canvaLinterna.SetActive(false);
        finalEnemy.final = true;
        finalEnemy.rangoEnemigo = 5000;
        print("Final activado");
        triggerFinal.SetActive(false);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PlayerPrefs.HasKey("PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPosX");
            float y = PlayerPrefs.GetFloat("PlayerPosY");
            float z = PlayerPrefs.GetFloat("PlayerPosZ");
            player.transform.position = new Vector3(x, y, z);
        }
    }    
}
