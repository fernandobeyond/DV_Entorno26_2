using UnityEngine;

public class PropiedadesJugador : MonoBehaviour {
    [SerializeField] private int vida = 100;
    [SerializeField] private float velocidad = 5.5f;
    public string nombreJugador = "Fernando";

    void Start() {
        Debug.Log("Bienvenido, " + nombreJugador + " al videojuego.");
    }

    void Update() {
        // Debug.Log("Velocidad: " + velocidad + " - Vida = " + vida);
        ControlarEstado();
    }

    private void ControlarEstado() {
        // Evaluar la vida del jugador y mostrar un mensaje si es baja
        if (vida <= 0) {
            Debug.Log("El jugador ha muerto.");
        }
        else if (vida < 20) {
            Debug.Log("Cuidado, tu vida es baja!");
        }

        // Evaluar la velocidad del jugador y mostrar un mensaje si es alta
        if (velocidad > 10f) {
            Debug.Log("Velocidad máxima alcanzada.");
            velocidad = 10f;
        }
    }
}