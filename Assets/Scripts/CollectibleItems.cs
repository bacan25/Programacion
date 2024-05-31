using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItems : MonoBehaviour
{
    //Para especificar de que objeto se verifica el booleano
    [SerializeField] CollectibleItems keyScript;
    [SerializeField] CollectibleItems doorScript;
    [SerializeField] CollectibleItems crossScript;


    [SerializeField] CheckpointsController check3;
    [SerializeField] CheckpointsController check2;

    //triggers
    [SerializeField] GameObject key;
    [SerializeField] GameObject crossbar;

    //Objestos interactuables (Puertas)
    [SerializeField] GameObject doorsExt;
    [SerializeField] GameObject doorsInt;
    
    //Boooooleanos
    [HideInInspector] public bool _hasKey;
    [HideInInspector] public bool _hasFlash;

    
    public bool _hasCrossbar;
    private bool _extDoorOpened;
    private bool _intDoorOpened;
    


    void Start()
    {
        //Borra Playerprefs
        //PlayerPrefs.DeleteAll();

        //Verifica guardados
        _hasKey = PlayerPrefs.GetInt("HasKey", 0) == 1;
        _hasCrossbar = PlayerPrefs.GetInt("HasCrossbar", 0) == 1;
        _hasFlash = PlayerPrefs.GetInt("HasFlashlight", 0) == 1;
        _extDoorOpened = PlayerPrefs.GetInt("DoorExtOpen", 0) == 1;
        _intDoorOpened = PlayerPrefs.GetInt("DoorIntOpen", 0) == 1;

        if (_hasKey)
        {
            key.SetActive(false);
        }

        if(_hasCrossbar)
        {
            crossbar.SetActive(false);
        }

        if(_extDoorOpened)
        {
            doorsExt.SetActive(false);
        }
        if(_intDoorOpened)
        {
            doorsInt.SetActive(false);
        }
        
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        //Verifica que objeto tocó
        if (col.CompareTag("Player"))
        {
            switch (gameObject.name)
            {
                case "Key":
                    if(check3.checkpoint2Pressed || check2.checkpoint2Pressed)
                    {
                       print("aja ve");
                        PickKey();
                    }
                    break;
                case "Crossbar":
                    PickCrossbar(); 
                    break;
                case "DoorExt":
                    if(keyScript._hasKey || crossScript._hasKey)
                    {
                        OpenExtDoor();
                    }
                    break;
                case "DoorInt":
                    if(doorScript._hasCrossbar || crossScript._hasCrossbar)
                    {
                        OpenIntDoor();
                    }
                    break;
            }
        }

    }

    //La flash tremenda mkada (Si es posible orgaizar mejor :)
    public void PickFlash()
    {
        _hasFlash = true;
        PlayerPrefs.SetInt("HasFlashlight", _hasFlash ? 1 : 0);
        _hasFlash = true;
        print("Flsahlight entro al chat");
    }

    void PickKey()
    {
        _hasKey = true;
        PlayerPrefs.SetInt("HasKey", _hasKey ? 1 : 0);
        key.SetActive(false);
        _hasKey = true;
        print("key entro al chat");
    }

    void PickCrossbar()
    {
        _hasCrossbar = true;
        PlayerPrefs.SetInt("HasCrossbar", _hasCrossbar ? 1 : 0);
        crossbar.SetActive(false);
        _hasCrossbar = true;
        print("crossbar entro al chat");
    }

    void OpenExtDoor()
    {
        doorsExt.SetActive(false);
        _extDoorOpened = true;
        PlayerPrefs.SetInt("DoorExtOpen", _extDoorOpened ? 1 : 0);

    }

    void OpenIntDoor()
    {
        doorsInt.SetActive(false);
        _intDoorOpened = true;
        PlayerPrefs.SetInt("DoorIntOpen", _intDoorOpened ? 1 : 0);
    }

   
}
