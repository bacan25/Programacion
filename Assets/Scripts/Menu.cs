using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Menu : MonoBehaviour
{
    int currentScene;
    public GameObject continueButton; // Asigna este botón en el inspector
    public GameObject pauseMenuUI;
    public GameObject settingsMenuUI;
    public Slider volumeSlider;
    public TMP_Dropdown resolutionDropdown; // Usar TMP_Dropdown
    public Toggle fullscreenToggle;

    private bool isPaused = false;
    private Resolution[] resolutions;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        // Verifica si hay datos guardados
        if (continueButton != null)
        {
            if (PlayerPrefs.HasKey("PlayerPosX"))
            {
                continueButton.SetActive(true); // Muestra el botón de continuar
            }
            else
            {
                continueButton.SetActive(false); // Oculta el botón de continuar si no hay datos guardados
            }
        }

        // Inicializa el menú de pausa y ajustes
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        
        if (settingsMenuUI != null)
        {
            settingsMenuUI.SetActive(false);
        }

        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume); // Asegúrate de que el evento esté suscrito
        }

        // Configurar resoluciones disponibles
        if (resolutionDropdown != null)
        {
            resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();
            int currentResolutionIndex = 0;

            for (int i = 0; i < resolutions.Length; i++)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height;
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                }
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = Screen.fullScreen;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void StartGame()
    {
        // Borra todos los datos guardados al iniciar un nuevo juego
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
        Debug.Log("Hablamos pa");
        Application.Quit();
    }

    public void NextScene()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

    // Funcionalidad del menú de pausa
    public void Resume()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        isPaused = false;
    }

    void Pause()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void OpenSettings()
    {
        if (settingsMenuUI != null)
        {
            settingsMenuUI.SetActive(true);
        }
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    public void CloseSettings()
    {
        if (settingsMenuUI != null)
        {
            settingsMenuUI.SetActive(false);
        }
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        Debug.Log("Volume set to: " + volume); // Añade un mensaje de depuración para verificar
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutions != null && resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
