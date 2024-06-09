using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    //Checkpoints presionados
    [HideInInspector] public bool checkpoint1Pressed = false;
    [HideInInspector] public bool checkpoint2Pressed = false;
    [HideInInspector] public bool checkpoint3Pressed = false;
    [HideInInspector] public bool checkpoint4Pressed = false;

    //Objetos recogidos
    [HideInInspector] public bool _hasCrossbar;
    [HideInInspector] public bool _extDoorOpened;
    [HideInInspector] public bool _intDoorOpened;
    [HideInInspector] public bool _hasFlash;
    [HideInInspector] public bool _hasKey;

    //Pistas recogidas
    [HideInInspector] public bool clue0Pressed = false;
    [HideInInspector] public bool clue1Pressed = false;
    [HideInInspector] public bool clue2Pressed = false;
    [HideInInspector] public bool clue3Pressed = false;
    [HideInInspector] public bool clue4Pressed = false;
    [HideInInspector] public bool clue5Pressed = false;
    [HideInInspector] public bool clue6Pressed = false;
    [HideInInspector] public bool clue7Pressed = false;
    [HideInInspector] public bool clue8Pressed = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
    }

}
