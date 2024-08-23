using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Asigna el VideoPlayer aquí
    public RawImage rawImage;       // Asigna el RawImage aquí
    public GameObject videoCanvas;  // Asigna el GameObject que contiene solo la RawImage y el VideoPlayer

    private bool isPlayingVideo = false;
    private string lastCheckpointKey = "LastCheckpoint";

    void Start()
    {

        videoCanvas.SetActive(false);
        videoPlayer.loopPointReached += OnVideoEnd;
        videoPlayer.errorReceived += OnVideoError;

        // Configura el VideoPlayer para renderizar en la RawImage
        videoPlayer.targetTexture = new RenderTexture(Screen.width, Screen.height, 0);
        rawImage.texture = videoPlayer.targetTexture;

        // Ajustar el RawImage para cubrir toda la pantalla
        RectTransform rectTransform = rawImage.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(1, 1);
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }

    void Update()
    {
        if (isPlayingVideo && Input.GetKeyDown(KeyCode.Escape))
        {
            EndVideo();
        }
    }

    public void PlayVideo()
    {
        isPlayingVideo = true;
        videoCanvas.SetActive(true);
        videoPlayer.Play();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        EndVideo();
    }

    private void OnVideoError(VideoPlayer vp, string message)
    {
        Debug.LogError("VideoPlayer Error: " + message);
        EndVideo();
    }

    private void EndVideo()
    {
        isPlayingVideo = false;
        videoCanvas.SetActive(false);
        videoPlayer.Stop();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
