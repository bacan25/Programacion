using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeartbeatController : MonoBehaviour
{
    public Transform[] enemies; // Asignar desde el Inspector
    public AudioSource heartbeatAudio; // Asignar desde el Inspector
    public float maxDistance = 50.0f; // Distancia máxima en la que el sonido es audible
    public float minPitch = 0.5f; // Mínimo pitch del audio
    public float maxPitch = 2.0f; // Máximo pitch del audio
    public float minVolume = 0.2f; // Volumen mínimo del audio
    public float maxVolume = 1.0f; // Volumen máximo del audio

    void Start()
    {
        // Filtrar enemigos activos al inicio
        var activeEnemies = enemies.Where(enemy => enemy.gameObject.activeInHierarchy).ToArray();
        
        // Si no hay enemigos activos, asegurarse de que el audio no esté reproduciéndose
        if (activeEnemies.Length == 0 && heartbeatAudio.isPlaying)
        {
            heartbeatAudio.Stop();
        }
    }

    void Update()
    {
        if (enemies.Length == 0) return;

        // Filtrar enemigos activos
        var activeEnemies = enemies.Where(enemy => enemy.gameObject.activeInHierarchy).ToArray();
        if (activeEnemies.Length == 0)
        {
            if (heartbeatAudio.isPlaying)
                heartbeatAudio.Stop();
            return;
        }

        // Calcular la distancia mínima entre el jugador y cualquier enemigo activo
        float closestDistance = activeEnemies.Min(enemy => Vector3.Distance(transform.position, enemy.position));

        // Ajustar el volumen y pitch del sonido basado en la proximidad
        if (closestDistance <= maxDistance)
        {
            if (!heartbeatAudio.isPlaying)
                heartbeatAudio.Play();

            heartbeatAudio.pitch = Mathf.Lerp(minPitch, maxPitch, 1 - (closestDistance / maxDistance));
            heartbeatAudio.volume = Mathf.Lerp(minVolume, maxVolume, 1 - (closestDistance / maxDistance));
        }
        else
        {
            if (heartbeatAudio.isPlaying)
                heartbeatAudio.Stop();
        }
    }
}
