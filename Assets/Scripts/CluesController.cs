using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluesController : MonoBehaviour
{
    //Mensaje en el UI
    [SerializeField] GameObject clue1Canva;
    [SerializeField] GameObject clue2Canva;
    [SerializeField] GameObject clue3Canva;
    [SerializeField] GameObject clue4Canva;
    [SerializeField] GameObject clue5Canva;
    [SerializeField] GameObject clue6Canva;
    [SerializeField] GameObject clue0Canva;
    [SerializeField] GameObject clue7Canva;
    [SerializeField] GameObject clue8Canva;

    //Level manager
    private LevelManager levelM;

    [SerializeField] private GameObject player;

    [SerializeField] GameObject clue0;
    //Puerta iglesia
    [SerializeField] GameObject clue1;

    //Segundo Checkpoint (Atras)
    [SerializeField] GameObject clue2;

    //Coche
    [SerializeField] GameObject clue3;

    //llave
    [SerializeField] GameObject clue4;

    //checkpoint 3 cobertizo
    [SerializeField] GameObject clue5;

    //Final
    [SerializeField] GameObject clue6;

    [SerializeField] GameObject clue7;

    [SerializeField] GameObject clue8;

   

    void Start()
    {
        //Acceso Singleton
        levelM = LevelManager.Instance;
        
        //Borra Playerprefs
        //PlayerPrefs.DeleteAll();

        //Verifica los guardados
        levelM.clue0Pressed = PlayerPrefs.GetInt("Clue0Pressed", 0) == 1;
        levelM.clue1Pressed = PlayerPrefs.GetInt("Clue1Pressed", 0) == 1;
        levelM.clue2Pressed = PlayerPrefs.GetInt("Clue2Pressed", 0) == 1;
        levelM.clue3Pressed = PlayerPrefs.GetInt("Clue3Pressed", 0) == 1;
        levelM.clue4Pressed = PlayerPrefs.GetInt("Clue4Pressed", 0) == 1;
        levelM.clue5Pressed = PlayerPrefs.GetInt("Clue5Pressed", 0) == 1;
        levelM.clue6Pressed = PlayerPrefs.GetInt("Clue6Pressed", 0) == 1;
        levelM.clue7Pressed = PlayerPrefs.GetInt("Clue7Pressed", 0) == 1;
        levelM.clue8Pressed = PlayerPrefs.GetInt("Clue8Pressed", 0) == 1;

        if (levelM.clue0Pressed)
        {
            clue0.SetActive(false); 
        }

        if (levelM.clue1Pressed)
        {
            clue1.SetActive(false); 
        }

        if (levelM.clue2Pressed)
        {
            clue2.SetActive(false); 
        }

        if (levelM.clue3Pressed)
        {
            clue3.SetActive(false); 
        }

        if (levelM.clue4Pressed)
        {
            clue4.SetActive(false); 
        }

        if (levelM.clue5Pressed)
        {
            clue5.SetActive(false); 
        }

        if (levelM.clue6Pressed)
        {
            clue6.SetActive(false); 
        }

        if (levelM.clue7Pressed)
        {
            clue7.SetActive(false); 
        }

        if (levelM.clue8Pressed)
        {
            clue8.SetActive(false); 
        }
    }

    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider col)
    {
        //verifica que pista tocó
        if (col.CompareTag("Player"))
        {
            switch (gameObject.name)
            {
                case "Clue#0":
                    Clue0();
                    break;
                case "Clue#1":
                    Clue1();
                    break;
                case "Clue#2":
                    Clue2();
                    break;
                case "Clue#3":
                    Clue3();
                    break;
                case "Clue#4":
                    Clue4();
                    break;
                case "Clue#5":
                    Clue5();
                    break;
                case "Clue#6":
                    Clue6();
                    break;
                case "Clue#7":
                    Clue7();
                    break;
                case "Clue#8":
                    Clue8();
                    break;
            }
        }
    }

    void Clue0()
    {
        levelM.clue0Pressed = true;
        PlayerPrefs.SetInt("Clue0Pressed", levelM.clue0Pressed ? 1 : 0);
        clue0Canva.SetActive(true);

        clue0.SetActive(false);

        Destroy(clue0Canva, 12f);
    }

    void Clue1()
    {
        levelM.clue1Pressed = true;
        PlayerPrefs.SetInt("Clue1Pressed", levelM.clue1Pressed ? 1 : 0);

        clue1Canva.SetActive(true);

        clue1.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue1Canva, 12f);

    }

    void Clue2()
    {
        levelM.clue2Pressed = true;
        PlayerPrefs.SetInt("Clue2Pressed", levelM.clue2Pressed ? 1 : 0);

        clue2Canva.SetActive(true);

        clue2.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue2Canva, 12f);
        
        if(clue1 != null){
            Destroy(clue1);
        }
        
    }
    
    void Clue3()
    {
        levelM.clue3Pressed = true;
        PlayerPrefs.SetInt("Clue3Pressed", levelM.clue3Pressed ? 1 : 0);

        clue3Canva.SetActive(true);

        clue3.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue3Canva, 12f);
    }
    
    void Clue4()
    {
        levelM.clue4Pressed = true;
        PlayerPrefs.SetInt("Clue4Pressed", levelM.clue4Pressed ? 1 : 0);

        clue4Canva.SetActive(true);

        clue4.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue4Canva, 12f);
    }

    void Clue5()
    {
        levelM.clue5Pressed = true;
        PlayerPrefs.SetInt("Clue5Pressed", levelM.clue5Pressed ? 1 : 0);

        clue5Canva.SetActive(true);

        clue5.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue5Canva, 12f);
    }

    void Clue6()
    {
        levelM.clue6Pressed = true;
        PlayerPrefs.SetInt("Clue6Pressed", levelM.clue6Pressed ? 1 : 0);

        clue6Canva.SetActive(true);

        clue6.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue6Canva, 12f);
    }

    void Clue7()
    {
        levelM.clue7Pressed = true;
        PlayerPrefs.SetInt("Clue7Pressed", levelM.clue7Pressed ? 1 : 0);

        clue7Canva.SetActive(true);

        clue7.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue7Canva, 12f);
    }

    void Clue8()
    {
        levelM.clue8Pressed = true;
        PlayerPrefs.SetInt("Clue8Pressed", levelM.clue8Pressed ? 1 : 0);

        clue8Canva.SetActive(true);

        clue8.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue8Canva, 12f);
    }
   
}
