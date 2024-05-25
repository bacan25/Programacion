using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LinternaControl : MonoBehaviour
{
    public RutaEnemigo enemigoScript;
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

    private void Update()
    {
        //Borra Playerprefs
        if (Input.GetKey(KeyCode.Z)) 
        {
            PlayerPrefs.DeleteAll(); 
            Debug.Log("PlayerPrefs pa la mierda"); 
        }
        if (tieneLinterna)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ToggleLinterna();
            }

            if (Input.GetKey(KeyCode.R) && !linterna.enabled && bateria < 100 && !isRecargando)
            {
                StartRecargarLinterna();
            }

            if (Input.GetKeyUp(KeyCode.R) || bateria >= 100)
            {
                StopRecargarLinterna();
            }

            if (Input.GetMouseButtonDown(1) && bateria > 50)
            {
                StartCoroutine(UsarFlash());
            }
        }

        if (linterna.enabled && bateria > 0)
        {
            bateria -= Time.deltaTime * 5;
            barraDeBateria.value = bateria / 100.0f;
        }

        if (bateria <= 0)
        {
            bateria = 0;
            linterna.enabled = false;
        }

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
        audioSource.Stop();  // Detiene el sonido de recarga

        if (bateria > 0 && linternaAntesDeRecargar)
        {
            linterna.enabled = true;
        }
    }

    IEnumerator UsarFlash()
    {
        linterna.enabled = true;
        float originalIntensity = linterna.intensity;
        linterna.intensity = 2.0f;
        bateria -= 50;
        barraDeBateria.value = bateria / 100.0f;

        // Verificar la distancia al enemigo
        float distanceToEnemy = Vector3.Distance(transform.position, enemigoScript.transform.position);
        if (distanceToEnemy <= enemigoScript.GetRangoEnemigo())  // Usando el método getter
        {
            enemigoScript.StunEnemy(8);  // Stun al enemigo por 8 segundos
        }

        for (int i = 0; i < 3; i++)
        {
            linterna.enabled = false;
            audioSource.PlayOneShot(sonidoToggleLinterna);  // Reproduce el sonido al apagar
            yield return new WaitForSeconds(0.1f);
            linterna.enabled = true;
            audioSource.PlayOneShot(sonidoToggleLinterna);  // Reproduce el sonido al encender
            yield return new WaitForSeconds(0.1f);
            linterna.enabled = false;
            audioSource.PlayOneShot(sonidoToggleLinterna);  // Reproduce el sonido al apagar
            yield return new WaitForSeconds(0.1f);
            linterna.enabled = true;
            audioSource.PlayOneShot(sonidoToggleLinterna);  // Reproduce el sonido al encender
            yield return new WaitForSeconds(0.1f);
        }

        linterna.intensity = originalIntensity;
        if (bateria <= 0)
        {
            linterna.enabled = false;
        }
    }


}
