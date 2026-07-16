using UnityEngine;
using UnityEngine.InputSystem;

public class ControlCamara : MonoBehaviour {
    public float sensibilidad = 5f;
    private float rotacionX = 0f;
    public Transform cuerpoJugador;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * sensibilidad * Time.deltaTime;
        float mouseY = mouseDelta.y * sensibilidad * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
        cuerpoJugador.Rotate(Vector3.up * mouseX);
    }
}
