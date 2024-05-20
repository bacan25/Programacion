using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RutaEnemigo : MonoBehaviour
{
    GameObject target;
    [SerializeField] private int rangoEnemigo;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float agroSpeed;
    [SerializeField] private AudioClip sonidoStun;   // Sonido para cuando el enemigo se stunnea
    [SerializeField] private AudioClip sonidoEstatica;  // Sonido constante de estática

    private int currentWaypoint;
    private NavMeshAgent navMeshAgent;
    private bool isStunned = false;
    private AudioSource audioSource;  // AudioSource para reproducir los sonidos

    void Start()
    {
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
            AgroMode();
        }
        else
        {
            PatrolMode();
        }
    }

    void AgroMode()
    {
        navMeshAgent.speed = agroSpeed;
        navMeshAgent.SetDestination(target.transform.position);
    }

    void PatrolMode()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
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
            audioSource.PlayOneShot(sonidoStun, 1.0f);  // Incrementa el segundo parámetro para aumentar el volumen del sonido de stun
            StartCoroutine(Stun(duration));
        }
    }

    IEnumerator Stun(float duration)
    {
        isStunned = true;
        navMeshAgent.isStopped = true;
        audioSource.PlayOneShot(sonidoStun);  // Reproducir el sonido de stun
        yield return new WaitForSeconds(duration);
        navMeshAgent.isStopped = false;
        isStunned = false;
    }
}
