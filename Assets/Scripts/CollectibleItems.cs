using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItems : MonoBehaviour
{
    //Para especificar de que objeto se verifica el booleano
    /* [SerializeField] CollectibleItems keyScript;
    [SerializeField] CollectibleItems doorScript;
    [SerializeField] CollectibleItems crossScript;
 */

    CheckpointsController check3;
    CheckpointsController check2;

    //Level manager
    private LevelManager levelM;

    //triggers
    /* [SerializeField] GameObject key;
    [SerializeField] GameObject crossbar; */

    //Objestos interactuables (Puertas)
    [SerializeField] GameObject doorsExt;
    [SerializeField] GameObject doorsInt;
    


    void Start()
    {
        //Acceso Singleton
        levelM = LevelManager.Instance;
        
        //Borra Playerprefs
        //PlayerPrefs.DeleteAll();

        //Verifica guardados
        /* levelM._hasKey = PlayerPrefs.GetInt("HasKey", 0) == 1;
        levelM._hasCrossbar = PlayerPrefs.GetInt("HasCrossbar", 0) == 1;
        levelM._hasFlash = PlayerPrefs.GetInt("HasFlashlight", 0) == 1; */
        levelM._extDoorOpened = PlayerPrefs.GetInt("DoorExtOpen", 0) == 1;
        levelM._intDoorOpened = PlayerPrefs.GetInt("DoorIntOpen", 0) == 1;

       /*  if (levelM._hasKey)
        {
            key.SetActive(false);
        }

        if(levelM._hasCrossbar)
        {
            crossbar.SetActive(false);
        } */

        if(levelM._extDoorOpened)
        {
            doorsExt.SetActive(false);
        }
        if(levelM._intDoorOpened)
        {
            doorsInt.SetActive(false);
        }
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        
        
        switch (this.gameObject.name)
        {
            case "DoorExt":
                if(col.gameObject.name == "Key")
                {
                    OpenExtDoor();
                }   
                break;

            case "DoorInt":
                if(col.gameObject.name == "Crossbar")
                {
                    OpenIntDoor(); 
                }
                break;
        }
        

    }

    //La flash tremenda mkada (Si es posible orgaizar mejor :)
    /* public void PickFlash()
    {
        levelM._hasFlash = true;
        PlayerPrefs.SetInt("HasFlashlight", levelM._hasFlash ? 1 : 0);
        levelM._hasFlash = true;
        print("Flsahlight entro al chat");
    } */

    /* void PickKey()
    {
        levelM._hasKey = true;
        PlayerPrefs.SetInt("HasKey", levelM._hasKey ? 1 : 0);
        key.SetActive(false);
        levelM._hasKey = true;
        print("key entro al chat");
    } */

    /* void PickCrossbar()
    {
        levelM._hasCrossbar = true;
        PlayerPrefs.SetInt("HasCrossbar", levelM._hasCrossbar ? 1 : 0);
        crossbar.SetActive(false);
        levelM._hasCrossbar = true;
        print("crossbar entro al chat");
    } */

    void OpenExtDoor()
    {
        doorsExt.SetActive(false);
        levelM._extDoorOpened = true;
        PlayerPrefs.SetInt("DoorExtOpen", levelM._extDoorOpened ? 1 : 0);

    }

    void OpenIntDoor()
    {
        doorsInt.SetActive(false);
        levelM._intDoorOpened = true;
        PlayerPrefs.SetInt("DoorIntOpen", levelM._intDoorOpened ? 1 : 0);
    }

   
}
