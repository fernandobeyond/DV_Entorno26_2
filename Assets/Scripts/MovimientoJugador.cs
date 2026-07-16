using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour {
    public float velocidad = 5f;

    void Update() {
        Vector2 inputMovimiento = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) inputMovimiento.y += 1f;
        if (Keyboard.current.sKey.isPressed) inputMovimiento.y -= 1f;
        if (Keyboard.current.aKey.isPressed) inputMovimiento.x -= 1f;
        if (Keyboard.current.dKey.isPressed) inputMovimiento.x += 1f;

        Vector3 movimiento = (transform.right * inputMovimiento.x + transform.forward * inputMovimiento.y).normalized;

        transform.Translate(movimiento * velocidad * Time.deltaTime, Space.World);
    }
}
