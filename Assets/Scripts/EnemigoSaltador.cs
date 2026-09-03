using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class EnemigoSaltador : MonoBehaviour
{
    public Transform objetivo;
    public int danioPorContacto = 15;

    private NavMeshAgent agente;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        if (objetivo == null) objetivo = GameObject.FindGameObjectWithTag("Player").transform;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if (objetivo != null && agente.isActiveAndEnabled && agente.isOnNavMesh)
        {
            agente.isStopped = false; // Nos aseguramos de que no esté detenido
            agente.SetDestination(objetivo.position);
        }
    }

    // Detección física cuando golpea al jugador
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            MovimientoJugador jugador = collision.gameObject.GetComponent<MovimientoJugador>();
            if (jugador != null)
            {
                jugador.RecibirDanio(danioPorContacto);
            }

            // Para empujarlo hacia atrás, le damos un impulso directo a la velocidad del NavMeshAgent.
            if (agente.isActiveAndEnabled && agente.isOnNavMesh)
            {
                Vector3 empuje = (transform.position - collision.transform.position).normalized;
                agente.velocity = empuje * 4f;
            }
        }
    }
}
