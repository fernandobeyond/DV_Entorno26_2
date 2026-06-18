using UnityEngine;

public class PropiedadesJugador : MonoBehaviour {
    [SerializeField] private int vida = 100;
    [SerializeField] private float velocidad = 5.5f;
    public string nombreJugador = "Fernando";

    void Start() {
        Debug.Log("Bienvenido, " + nombreJugador + " al videojuego.");
    }

    void Update() {
        Debug.Log("Velocidad: " + velocidad + " - Vida = " + vida);
    }
}
