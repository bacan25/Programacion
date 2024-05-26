using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CluesController : MonoBehaviour
{
    //Mensaje en el UI
    [SerializeField] GameObject clue1Canva;
    [SerializeField] GameObject clue2Canva;
    //[SerializeField] GameObject clue3Canva;
    //[SerializeField] GameObject clue4Canva;

    //Booleanos
    private bool clue1Pressed = false;
    private bool clue2Pressed = false;
    private bool clue3Pressed = false;
    private bool clue4Pressed = false;

    [SerializeField] private GameObject player;

    //Primer checkpoint (Por la fogata)
    [SerializeField] GameObject clue1;

    //Segundo Checkpoint (Atras)
    [SerializeField] GameObject clue2;

    //Tercer Checkpoint (Cobertizo)
    //[SerializeField] GameObject clue3;

    //Final
    //[SerializeField] GameObject clue4;

    void Start()
    {
        //Borra Playerprefs
        //PlayerPrefs.DeleteAll();

        //Verifica los guardados
        clue1Pressed = PlayerPrefs.GetInt("Clue1Pressed", 0) == 1;

        if (clue1Pressed)
        {
            clue1.SetActive(false); 
        }

        if (clue2Pressed)
        {
            clue2.SetActive(false); 
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
            }
        }
    }

    void Clue1()
    {
        clue1Pressed = true;
        PlayerPrefs.SetInt("Clue1Pressed", clue1Pressed ? 1 : 0);

        clue1Canva.SetActive(true);

        clue1.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue1Canva, 7f);

    }

    void Clue2()
    {
        clue2Pressed = true;
        PlayerPrefs.SetInt("Clue2Pressed", clue2Pressed ? 1 : 0);

        clue2Canva.SetActive(true);

        clue2.SetActive(false);

        PlayerPrefs.Save();

        Destroy(clue2Canva, 7f);
        
        if(clue1 != null){
            Destroy(clue1);
        }
        
    }
    
    void Clue3()
    {

    }
    
    void Clue4()
    {

    }
   
}
