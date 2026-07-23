using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour {
    // Parametros de movimiento
    public float velocidad = 5f;
    // Parametro de salto
    [SerializeField] public float fuerzaSalto = 5f;
    private Rigidbody rb;
    private bool estaEnSuelo = true;

    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        Vector2 inputMovimiento = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) inputMovimiento.y += 1f;
        if (Keyboard.current.sKey.isPressed) inputMovimiento.y -= 1f;
        if (Keyboard.current.aKey.isPressed) inputMovimiento.x -= 1f;
        if (Keyboard.current.dKey.isPressed) inputMovimiento.x += 1f;

        Vector3 movimiento = (transform.right * inputMovimiento.x + transform.forward * inputMovimiento.y).normalized;

        transform.Translate(movimiento * velocidad * Time.deltaTime, Space.World);

        if (Keyboard.current.spaceKey.wasPressedThisFrame && estaEnSuelo) {
            rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
            estaEnSuelo = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Suelo")) {
            estaEnSuelo = true;
        }

        Rigidbody rbImpacto = collision.gameObject.GetComponent<Rigidbody>();

        if (rbImpacto != null) {
            Vector3 direccionImpacto = collision.gameObject.transform.position - transform.position;
            rbImpacto.AddForce(direccionImpacto.normalized * 2f, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Moneda")) {
            Debug.Log("Moneda recogida");
            Destroy(other.gameObject);
        }
    }


}
