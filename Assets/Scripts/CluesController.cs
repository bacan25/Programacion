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

    //Booleanos
    private bool clue0Pressed = false;
    private bool clue1Pressed = false;
    private bool clue2Pressed = false;
    private bool clue3Pressed = false;
    private bool clue4Pressed = false;
    private bool clue5Pressed = false;
    private bool clue6Pressed = false;
    private bool clue7Pressed = false;
    private bool clue8Pressed = false;

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
        //Borra Playerprefs
        //PlayerPrefs.DeleteAll();

        //Verifica los guardados
        clue0Pressed = PlayerPrefs.GetInt("Clue0Pressed", 0) == 1;
        clue1Pressed = PlayerPrefs.GetInt("Clue1Pressed", 0) == 1;
        clue2Pressed = PlayerPrefs.GetInt("Clue2Pressed", 0) == 1;
        clue3Pressed = PlayerPrefs.GetInt("Clue3Pressed", 0) == 1;
        clue4Pressed = PlayerPrefs.GetInt("Clue4Pressed", 0) == 1;
        clue5Pressed = PlayerPrefs.GetInt("Clue5Pressed", 0) == 1;
        clue6Pressed = PlayerPrefs.GetInt("Clue6Pressed", 0) == 1;
        clue7Pressed = PlayerPrefs.GetInt("Clue7Pressed", 0) == 1;
        clue8Pressed = PlayerPrefs.GetInt("Clue8Pressed", 0) == 1;

        if (clue0Pressed)
        {
            clue0.SetActive(false); 
        }

        if (clue1Pressed)
        {
            clue1.SetActive(false); 
        }

        if (clue2Pressed)
        {
            clue2.SetActive(false); 
        }

        if (clue3Pressed)
        {
            clue3.SetActive(false); 
        }

        if (clue4Pressed)
        {
            clue4.SetActive(false); 
        }

        if (clue5Pressed)
        {
            clue5.SetActive(false); 
        }

        if (clue6Pressed)
        {
            clue6.SetActive(false); 
        }

        if (clue7Pressed)
        {
            clue7.SetActive(false); 
        }

        if (clue8Pressed)
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
        clue0Pressed = true;
        PlayerPrefs.SetInt("Clue0Pressed", clue0Pressed ? 1 : 0);
        clue0Canva.SetActive(true);

        clue0.SetActive(false);

        Destroy(clue0Canva, 12f);
    }

    void Clue1()
    {
        clue1Pressed = true;
        PlayerPrefs.SetInt("Clue1Pressed", clue1Pressed ? 1 : 0);

        clue1Canva.SetActive(true);

        clue1.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue1Canva, 12f);

    }

    void Clue2()
    {
        clue2Pressed = true;
        PlayerPrefs.SetInt("Clue2Pressed", clue2Pressed ? 1 : 0);

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
        clue3Pressed = true;
        PlayerPrefs.SetInt("Clue3Pressed", clue3Pressed ? 1 : 0);

        clue3Canva.SetActive(true);

        clue3.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue3Canva, 12f);
    }
    
    void Clue4()
    {
        clue4Pressed = true;
        PlayerPrefs.SetInt("Clue4Pressed", clue4Pressed ? 1 : 0);

        clue4Canva.SetActive(true);

        clue4.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue4Canva, 12f);
    }

    void Clue5()
    {
        clue5Pressed = true;
        PlayerPrefs.SetInt("Clue5Pressed", clue5Pressed ? 1 : 0);

        clue5Canva.SetActive(true);

        clue5.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue5Canva, 12f);
    }

    void Clue6()
    {
        clue6Pressed = true;
        PlayerPrefs.SetInt("Clue6Pressed", clue6Pressed ? 1 : 0);

        clue6Canva.SetActive(true);

        clue6.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue6Canva, 12f);
    }

    void Clue7()
    {
        clue7Pressed = true;
        PlayerPrefs.SetInt("Clue7Pressed", clue7Pressed ? 1 : 0);

        clue7Canva.SetActive(true);

        clue7.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue7Canva, 12f);
    }

    void Clue8()
    {
        clue8Pressed = true;
        PlayerPrefs.SetInt("Clue8Pressed", clue8Pressed ? 1 : 0);

        clue8Canva.SetActive(true);

        clue8.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue8Canva, 12f);
    }
   
}
