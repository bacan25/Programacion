using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject videoCanvas;
    private bool isPlayingVideo = false;
    private string lastCheckpointKey = "LastCheckpoint";

    void Start()
    {
        videoCanvas.SetActive(false);
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void Update()
    {
        if (isPlayingVideo && Input.GetKeyDown(KeyCode.Escape)) // Skip video
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

    private void EndVideo()
    {
        isPlayingVideo = false;
        videoCanvas.SetActive(false);
        videoPlayer.Stop();
        
        if (PlayerPrefs.HasKey(lastCheckpointKey))
        {
            string checkpointScene = PlayerPrefs.GetString(lastCheckpointKey);
            SceneManager.LoadScene(checkpointScene);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
