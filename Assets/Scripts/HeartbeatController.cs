using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartbeatController : MonoBehaviour
{
    public Transform enemy; // Asignar desde el Inspector
    public AudioSource heartbeatAudio; // Asignar desde el Inspector
    public float maxDistance = 50.0f; // Distancia máxima en la que el sonido es audible
    public float minPitch = 0.5f; // Mínimo pitch del audio
    public float maxPitch = 2.0f; // Máximo pitch del audio
    public float minVolume = 0.2f; // Volumen mínimo del audio
    public float maxVolume = 1.0f; // Volumen máximo del audio

    private float distance;

    void Update()
    {
        // Calcular la distancia entre el jugador y el enemigo
        distance = Vector3.Distance(transform.position, enemy.position);

        // Ajustar el volumen y pitch del sonido basado en la proximidad
        if (distance <= maxDistance)
        {
            if (!heartbeatAudio.isPlaying)
                heartbeatAudio.Play();

            heartbeatAudio.pitch = Mathf.Lerp(minPitch, maxPitch, 1 - (distance / maxDistance));
            heartbeatAudio.volume = Mathf.Lerp(minVolume, maxVolume, 1 - (distance / maxDistance));
        }
        else
        {
            if (heartbeatAudio.isPlaying)
                heartbeatAudio.Stop();
        }
    }
}
