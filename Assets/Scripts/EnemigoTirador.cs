using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemigoTirador : MonoBehaviour
{
    [Header("Referencias")]
    public Transform objetivo;
    public GameObject balaPrefab;
    public Transform puntoDisparo;

    [Header("Parámetros")]
    public float distanciaParaAtacar = 12f;
    public float cadenciaFuego = 1.5f;

    private NavMeshAgent agente;
    private float cronometro;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        if (objetivo == null) objetivo = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (objetivo == null) return;

        float distancia = Vector3.Distance(transform.position, objetivo.position);

        if (distancia > distanciaParaAtacar)
        {
            // Avanza
            agente.isStopped = false;
            agente.SetDestination(objetivo.position);
        }
        else
        {
            // Se detiene y dispara
            agente.isStopped = true;

            // Mira al jugador bloqueando el eje Y para no rotar hacia el suelo
            Vector3 direccion = new Vector3(objetivo.position.x, transform.position.y, objetivo.position.z);
            transform.LookAt(direccion);

            ManejarDisparo();
        }
    }

    void ManejarDisparo()
    {
        cronometro += Time.deltaTime;
        if (cronometro >= cadenciaFuego)
        {
            if (balaPrefab != null && puntoDisparo != null)
            {
                Instantiate(balaPrefab, puntoDisparo.position, puntoDisparo.rotation);
            }
            cronometro = 0;
        }
    }
}
