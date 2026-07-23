using UnityEngine;

public class PropiedadesJugador : MonoBehaviour {
    [SerializeField] private int vida;
    [SerializeField] private float velocidad = 5.5f;
    public string nombreJugador = "Fernando";

    private void Awake() {
        Debug.Log("Awake: Inicializando propiedades de:" + nombreJugador);
    }

    void Start() {
        Vida = 2000;
        Debug.Log("Bienvenido, " + nombreJugador + " al videojuego.");
    }

    void Update() {
        VerificarEstado();
        ControlarEstado();
    }

    private void ControlarEstado() {
        // Evaluar la vida del jugador y mostrar un mensaje si es baja
        if (vida <= 0) {
            Debug.Log("El jugador ha muerto.");
            vida = 0;
        } else if (vida > 100) {
            vida = 100;
        } else if (vida < 20) {
            Debug.Log("Cuidado, tu vida es baja!");
        }

        // Evaluar la velocidad del jugador y mostrar un mensaje si es alta
        if (velocidad > 10f) {
            Debug.Log("Velocidad máxima alcanzada.");
            velocidad = 10f;
        }
    }

    private void VerificarEstado() {
        if (vida >= 20) {
            //Debug.Log("Velocidad: " + velocidad + " - Vida = " + vida);
        }
    }

    // Propiedad publica para acceder a la vida del jugador desde otros scripts
    public int Vida {
        get { return vida; }
        set {
            if (value < 0) {
                vida = 0;
            }
            else if (value > 100) {
                vida = 100;
            }
            else {
                vida = value;
            }
        }
    }
}