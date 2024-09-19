using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RutaEnemigo : MonoBehaviour
{
    GameObject target;
    public int rangoEnemigo;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float agroSpeed;
    [SerializeField] private AudioClip sonidoStun;   // Sonido para cuando el enemigo se stunnea
    [SerializeField] private AudioClip sonidoEstatica;  // Sonido constante de estática
    [SerializeField] private VideoController videoController; // Referencia al VideoController

    [SerializeField] private Camera playerCamera; // Asignar la cámara del jugador en el inspector

    private int currentWaypoint;
    private NavMeshAgent navMeshAgent;
    private bool isStunned = false;
    private AudioSource audioSource;  // AudioSource para reproducir los sonidos

    [HideInInspector] public bool final = false;

    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        navMeshAgent.angularSpeed = 360;
        navMeshAgent.autoBraking = false;
        audioSource = GetComponent<AudioSource>();

        // Configura el AudioSource
        audioSource.loop = true;
        audioSource.clip = sonidoEstatica;
        audioSource.volume = 0.5f;  // Ajusta según sea necesario
        audioSource.spatialBlend = 1.0f;  // Hace el sonido completamente 3D
        audioSource.minDistance = 1.0f;  // Ajusta según sea necesario
        audioSource.maxDistance = 15.0f;  // Reduce para limitar la audibilidad del sonido de estática

        audioSource.Play();
    }

    void Update()
    {
        if (final)
        {
            navMeshAgent.speed = agroSpeed + 2;
            navMeshAgent.SetDestination(target.transform.position);
        }

        if (!isStunned)
        {
            ComportamientoEnemigo();
        }
    }

    public int GetRangoEnemigo()
    {
        return rangoEnemigo;
    }

    void ComportamientoEnemigo()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToPlayer <= rangoEnemigo)
        {
            if (distanceToPlayer < 3.0f) // Ajusta la distancia según sea necesario
            {
                // Reproduce el video y oscurece la cámara
                videoController.PlayVideo();
                StartCoroutine(FadeToBlack());
            }
            else
            {
                AgroMode();
            }
        }
        else
        {
            PatrolMode();
        }
    }

    void AgroMode()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isAgro", true);
        navMeshAgent.speed = agroSpeed;
        navMeshAgent.SetDestination(target.transform.position);
    }

    void PatrolMode()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isAgro", false);
            navMeshAgent.speed = moveSpeed;
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0;
            }

            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }
    }

    public void StunEnemy(float duration)
    {
        if (!isStunned)
        {
            audioSource.PlayOneShot(sonidoStun, 1.0f);
            StartCoroutine(Stun(duration));
        }
    }

    IEnumerator Stun(float duration)
    {
        isStunned = true;
        anim.SetTrigger("isStuned");
        anim.SetBool("isAgro", false);
        anim.SetBool("isWalking", false);
        navMeshAgent.isStopped = true;
        audioSource.PlayOneShot(sonidoStun);
        yield return new WaitForSeconds(duration);
        navMeshAgent.isStopped = false;
        isStunned = false;
    }

    // Corrutina para oscurecer la cámara
    IEnumerator FadeToBlack()
    {
        // Crear una imagen negra que cubra la cámara
        GameObject blackOverlay = new GameObject("BlackOverlay");
        blackOverlay.transform.SetParent(playerCamera.transform);
        blackOverlay.transform.localPosition = Vector3.zero;
        blackOverlay.transform.localRotation = Quaternion.identity;

        // Crear un quad frente a la cámara
        MeshRenderer renderer = blackOverlay.AddComponent<MeshRenderer>();
        MeshFilter filter = blackOverlay.AddComponent<MeshFilter>();
        filter.mesh = CreateQuadMesh();

        // Posicionarlo justo frente a la cámara
        blackOverlay.transform.localPosition = new Vector3(0, 0, 0.1f);

        // Material negro
        Material blackMaterial = new Material(Shader.Find("Unlit/Color"));
        blackMaterial.color = new Color(0, 0, 0, 0); // Comienza transparente
        renderer.material = blackMaterial;

        float fadeDuration = 1.0f; // Duración del fade
        float fadeProgress = 0.0f;

        // Fade progresivo a negro
        while (fadeProgress < fadeDuration)
        {
            fadeProgress += Time.deltaTime;
            blackMaterial.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, fadeProgress / fadeDuration));
            yield return null;
        }

        // Mantener el negro un poco antes de reiniciar
        yield return new WaitForSeconds(2.0f);
        Destroy(blackOverlay);  // Destruir el overlay al finalizar
    }

    // Método para crear un quad
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
