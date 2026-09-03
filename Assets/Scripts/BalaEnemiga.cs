using UnityEngine;

public class BalaEnemiga : MonoBehaviour {
    public float velocidad = 15f;
    public float tiempoVida = 3f;
    public int danioInfligido = 10;

    void Start() {
        Destroy(gameObject, tiempoVida); // Se autodestruye para no saturar memoria
    }

    void Update() {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player"))
        {
            // Busca el script en el jugador y ejecuta el daño
            MovimientoJugador jugador = other.GetComponent<MovimientoJugador>();
            if (jugador != null)
            {
                jugador.RecibirDanio(danioInfligido);
            }
            Destroy(gameObject); // La bala desaparece al impactar
        }
    }
}
