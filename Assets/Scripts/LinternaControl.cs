using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class LinternaControlXR : MonoBehaviour
{
    public RutaEnemigo[] enemigos; // Asignar todos los enemigos desde el Inspector
    public Light linterna;
    public bool tieneLinterna = false;
    public float bateria = 100.0f;
    public Slider barraDeBateria;
    public GameObject animacionRecarga;
    public RawImage imagenDeRecarga;

    public AudioSource audioSource;
    public AudioClip sonidoToggleLinterna;
    public AudioClip sonidoRecargando;
    public AudioClip sonidoFlash;

    private bool linternaAntesDeRecargar;
    private bool isRecargando = false;
    private XRController controller;  // Controlador XR
    private Vector3 previousAcceleration;
    private float shakeThreshold = 3.0f; // Umbral para detectar sacudidas

    void Start()
    {
        // Asignar el controlador XR
        controller = GetComponent<XRController>();
        previousAcceleration = Vector3.zero;
    }

    private void Update()
    {
        if (tieneLinterna)
        {
            // Control de linterna tradicional (teclado)
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleLinterna();
            }

            // Encender/Apagar linterna con controlador
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue) && primaryButtonValue)
            {
                ToggleLinterna();
            }

            // Recarga con tecla R
            if (Input.GetKey(KeyCode.R) && !linterna.enabled && bateria < 100 && !isRecargando)
            {
                StartRecargarLinterna();
            }

            if (Input.GetKeyUp(KeyCode.R) || bateria >= 100)
            {
                StopRecargarLinterna();
            }

            // Control de recarga al agitar el controlador
            DetectarSacudida();

            // Usar el flash con botón secundario del controlador
            if (controller.inputDevice.TryGetFeatureValue(CommonUsages.secondaryButton, out bool secondaryButtonValue) && secondaryButtonValue && bateria > 50)
            {
                StartCoroutine(UsarFlash());
            }

            // Usar el flash con el botón derecho del ratón en PC
            if (Input.GetMouseButtonDown(1) && bateria > 50)
            {
                StartCoroutine(UsarFlash());
            }
        }

        // Drenar batería cuando la linterna está encendida
        if (linterna.enabled && bateria > 0)
        {
            bateria -= Time.deltaTime * 2;
            barraDeBateria.value = bateria / 100.0f;
        }

        // Apagar linterna si se agota la batería
        if (bateria <= 0)
        {
            bateria = 0;
            linterna.enabled = false;
        }

        // Recargar batería
        if (isRecargando && bateria < 100)
        {
            bateria += Time.deltaTime * 20;
            barraDeBateria.value = bateria / 100.0f;
            imagenDeRecarga.transform.Rotate(0, 0, -360 * Time.deltaTime);
        }
    }

    void ToggleLinterna()
    {
        if (bateria > 0)
        {
            linterna.enabled = !linterna.enabled;
            audioSource.PlayOneShot(sonidoToggleLinterna);
        }
    }

    void StartRecargarLinterna()
    {
        linternaAntesDeRecargar = linterna.enabled;
        linterna.enabled = false;
        animacionRecarga.SetActive(true);
        imagenDeRecarga.gameObject.SetActive(true);
        isRecargando = true;
        audioSource.PlayOneShot(sonidoRecargando);
    }

    void StopRecargarLinterna()
    {
        animacionRecarga.SetActive(false);
        imagenDeRecarga.gameObject.SetActive(false);
        isRecargando = false;
        audioSource.Stop();

        if (bateria > 0 && linternaAntesDeRecargar)
        {
            linterna.enabled = true;
        }
    }

    void DetectarSacudida()
    {
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.deviceAcceleration, out Vector3 acceleration))
        {
            Vector3 deltaAcceleration = acceleration - previousAcceleration;

            if (deltaAcceleration.magnitude > shakeThreshold && bateria < 100 && !isRecargando)
            {
                StartRecargarLinterna();
            }
            else if (deltaAcceleration.magnitude <= shakeThreshold || bateria >= 100)
            {
                StopRecargarLinterna();
            }

            previousAcceleration = acceleration;
        }
    }

    IEnumerator UsarFlash()
    {
        linterna.enabled = true;
        float originalIntensity = linterna.intensity;
        linterna.intensity = 2.0f;
        bateria -= 40;
        barraDeBateria.value = bateria / 100.0f;

        // Verificar la distancia a todos los enemigos
        foreach (var enemigo in enemigos)
        {
            if (enemigo.gameObject.activeInHierarchy)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemigo.transform.position);
                if (distanceToEnemy <= enemigo.GetRangoEnemigo())
                {
                    enemigo.StunEnemy(2);  // Stun al enemigo por 8 segundos
                }
            }
        }

        for (int i = 0; i < 3; i++)
        {
            linterna.enabled = false;
            audioSource.PlayOneShot(sonidoToggleLinterna);
            yield return new WaitForSeconds(0.1f);
            linterna.enabled = true;
            audioSource.PlayOneShot(sonidoToggleLinterna);
            yield return new WaitForSeconds(0.1f);
            linterna.enabled = false;
            audioSource.PlayOneShot(sonidoToggleLinterna);
            yield return new WaitForSeconds(0.1f);
            linterna.enabled = true;
            audioSource.PlayOneShot(sonidoToggleLinterna);
            yield return new WaitForSeconds(0.1f);
        }

        linterna.intensity = originalIntensity;
        if (bateria <= 0)
        {
            linterna.enabled = false;
        }
    }
}
