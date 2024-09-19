using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

public class HeartbeatController : MonoBehaviour
{
    public Transform[] enemies; // Assign all enemies from the Inspector
    public AudioSource heartbeatAudio; // Assign the heartbeat audio from the Inspector
    public float maxDistance = 50.0f; // Maximum distance at which the sound is audible
    public float minPitch = 0.5f; // Minimum audio pitch
    public float maxPitch = 2.0f; // Maximum audio pitch
    public float minVolume = 0.2f; // Minimum audio volume
    public float maxVolume = 1.0f; // Maximum audio volume

    public float deathDistance = 5.0f; // Distance at which the player's camera fades to black

    private Camera mainCamera; // Reference to the player's main camera
    private Renderer cameraRenderer; // Renderer for the black screen effect
    private bool isFading = false;

    void Start()
    {
        // Get the main camera within the XR Rig
        mainCamera = GetComponentInChildren<Camera>();

        // Add a black overlay to simulate the fade-to-black effect
        CreateBlackOverlay();

        // Filter active enemies at the start
        var activeEnemies = enemies.Where(enemy => enemy.gameObject.activeInHierarchy).ToArray();

        // Stop the heartbeat audio if no enemies are active
        if (activeEnemies.Length == 0 && heartbeatAudio.isPlaying)
        {
            heartbeatAudio.Stop();
        }
    }

    void Update()
    {
        if (enemies.Length == 0) return;

        // Filter active enemies
        var activeEnemies = enemies.Where(enemy => enemy.gameObject.activeInHierarchy).ToArray();
        if (activeEnemies.Length == 0)
        {
            if (heartbeatAudio.isPlaying)
                heartbeatAudio.Stop();
            return;
        }

        // Calculate the closest distance between the player and any active enemy
        float closestDistance = activeEnemies.Min(enemy => Vector3.Distance(transform.position, enemy.position));

        // Adjust heartbeat sound based on proximity
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

        // Trigger death scene if the enemy is too close
        if (closestDistance <= deathDistance && !isFading)
        {
            StartCoroutine(FadeToBlack());
        }
    }

    // Create a black overlay for the fade effect
    void CreateBlackOverlay()
    {
        GameObject blackOverlay = new GameObject("BlackOverlay");
        blackOverlay.transform.SetParent(mainCamera.transform);
        blackOverlay.transform.localPosition = Vector3.zero;
        blackOverlay.transform.localRotation = Quaternion.identity;

        // Create a full-screen black quad in front of the camera
        MeshRenderer renderer = blackOverlay.AddComponent<MeshRenderer>();
        MeshFilter filter = blackOverlay.AddComponent<MeshFilter>();
        filter.mesh = CreateQuadMesh();

        // Position it just in front of the camera
        blackOverlay.transform.localPosition = new Vector3(0, 0, 0.1f);

        // Create the black material for the quad
        Material blackMaterial = new Material(Shader.Find("Unlit/Color"));
        blackMaterial.color = Color.black;
        renderer.material = blackMaterial;

        // Disable it initially
        blackOverlay.SetActive(false);
        cameraRenderer = renderer;
    }

    // Coroutine to fade the camera view to black
    IEnumerator FadeToBlack()
    {
        isFading = true;
        cameraRenderer.gameObject.SetActive(true);
        Color blackColor = cameraRenderer.material.color;
        float fadeDuration = 1.0f; // Time to fully fade
        float fadeProgress = 0.0f;

        while (fadeProgress < fadeDuration)
        {
            fadeProgress += Time.deltaTime;
            blackColor.a = Mathf.Lerp(0, 1, fadeProgress / fadeDuration);
            cameraRenderer.material.color = blackColor;
            yield return null;
        }

        // Once faded to black, trigger any additional game over or death logic here
        Debug.Log("Player is dead!");

        // Optionally, reset after fade (depending on game design)
        yield return new WaitForSeconds(2.0f);
        isFading = false;
        cameraRenderer.material.color = new Color(0, 0, 0, 0); // Reset alpha to fully transparent
        cameraRenderer.gameObject.SetActive(false);
    }

    // Create a simple quad mesh
    Mesh CreateQuadMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = new Vector3[] {
            new Vector3(-1, -1, 0),
            new Vector3(1, -1, 0),
            new Vector3(-1, 1, 0),
            new Vector3(1, 1, 0)
        };
        mesh.uv = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };
        mesh.triangles = new int[] { 0, 2, 1, 2, 3, 1 };
        return mesh;
    }
}
