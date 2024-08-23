using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCheck : MonoBehaviour
{
    public CheckpointsController controller;

    private LevelManager levelM;

    void Start()
    {
        levelM = LevelManager.Instance;
    }

    private void OnTriggerEnter(Collider col)
    {
        //Comprueba que Trigger toco el pana
        if(col.CompareTag("Player"))
        {
            switch (this.name)
            {
                case "Checkpoint#1":
                    controller.Checkpoint1();
                    break;
                case "Checkpoint#2":
                    if (levelM._hasFlash)
                    {
                        controller.Checkpoint2();
                    }
                    break;
                case "Checkpoint#3":
                    controller.Checkpoint3();
                    break;
                case "Checkpoint#4":
                    controller.Checkpoint4();
                    break;
                case "InicioFinal":
                    controller.Final();
                    break;
            }
        }
        
        
    }
}
