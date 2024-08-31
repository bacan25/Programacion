using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clues : MonoBehaviour
{
    public CluesController controller;
    private LevelManager levelM;

    void Start()
    {
        levelM = LevelManager.Instance;
    }

    private void OnTriggerEnter(Collider col)
    {
        //verifica que pista tocó
        if (col.CompareTag("Player"))
        {
            switch (gameObject.name)
            {
                case "Clue#0":
                    controller.Clue0();
                    break;
                case "Clue#1":
                    controller.Clue1();
                    break;
                case "Clue#2":
                    controller.Clue2();
                    break;
                case "Clue#3":
                    controller.Clue3();
                    break;
                case "Clue#4":
                    controller.Clue4();
                    break;
                case "Clue#5":
                    controller.Clue5();
                    break;
                case "Clue#6":
                    controller.Clue6();
                    break;
                case "Clue#7":
                    controller.Clue7();
                    break;
                case "Clue#8":
                    controller.Clue8();
                    break;
                case "Clue#9":
                    controller.Clue9();
                    break;
            }
        }
    }
}
