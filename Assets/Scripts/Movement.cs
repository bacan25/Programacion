using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed = 5.0f;
    public float runningSpeed = 10.0f;
    public float stamina = 100.0f;
    public float staminaDecreasePerSecond = 10.0f;
    public float staminaRecoveryPerSecond = 5.0f;
    public Slider staminaBar;

    public AudioClip walkingSound;
    public AudioClip runningSound;
    private AudioSource movementAudioSource;  // AudioSource para sonidos de movimiento

    private float currentStamina;
    private CharacterController controller;
    private bool isRunning;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentStamina = stamina;
        staminaBar.value = CalculateStaminaPercent();

        // Crear AudioSource para movimiento si no existe
        if (movementAudioSource == null)
        {
            movementAudioSource = gameObject.AddComponent<AudioSource>();
            movementAudioSource.playOnAwake = false;
            movementAudioSource.loop = false;  // Asegúrate de que no esté en loop
        }
    }

    void Update()
    {
        MovePlayer();
        UpdateStamina();
    }

    void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        float finalSpeed = isRunning ? runningSpeed : speed;
        controller.Move(transform.TransformDirection(movement) * finalSpeed * Time.deltaTime);

        // Controlar la reproducción del sonido
        if (movement.magnitude > 0)
        {
            AudioClip clipToPlay = isRunning ? runningSound : walkingSound;
            if (movementAudioSource.clip != clipToPlay || !movementAudioSource.isPlaying)
            {
                movementAudioSource.clip = clipToPlay;
                movementAudioSource.Play();
            }
        }
        else if (movementAudioSource.isPlaying)
        {
            movementAudioSource.Stop();
        }
    }

    void UpdateStamina()
    {
        if (isRunning)
        {
            currentStamina -= staminaDecreasePerSecond * Time.deltaTime;
        }
        else
        {
            currentStamina += staminaRecoveryPerSecond * Time.deltaTime;
        }
        currentStamina = Mathf.Clamp(currentStamina, 0, stamina);
        staminaBar.value = CalculateStaminaPercent();
    }

    float CalculateStaminaPercent()
    {
        return currentStamina / stamina;
    }
}
