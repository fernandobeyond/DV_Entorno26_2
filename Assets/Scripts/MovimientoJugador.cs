using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour {
    // Parametros de inicio
    public string nombreJugador = "Fernando";
    [SerializeField] private int salud;

    // Parametros de movimiento
    [Header("Movimiento")]
    public float velocidad = 5f;
    public float velocidadCorrer = 9f;
    public float fuerzaSalto = 7f;

    // Camara
    [Header("Cámara")]
    public Transform camaraJugador;
    public float sensibilidadMouse = 0.15f;
    public float sensibilidadMando = 2f;

    // Rigidbody del jugador
    private Rigidbody rb;
    private bool estaEnSuelo = true;
    private float rotacionCamaraX = 0f;

    // Sistema de Input Actions generado automáticamente
    private JugadorControles controles;
    private Vector2 inputMovimiento;
    private Vector2 inputMirar;

    // UI Manager
    [Header("UI Manager")]
    public UIManager uiManager;
    private int monedasTotales = 0;
    private int vidasRestantes = 3;

    private void Awake() {
        controles = new JugadorControles();
    }

    private void OnEnable() {
        controles.Player.Enable();
        controles.Player.Jump.performed += OnSaltar;
    }

    private void OnDisable() {
        controles.Player.Jump.performed -= OnSaltar;
        controles.Player.Disable();
    }

    void Start() {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        NotificarCambioUI();
    }

    void Update() {
        // Leer valores — funciona con teclado Y mando automáticamente
        inputMovimiento = controles.Player.Move.ReadValue<Vector2>();
        inputMirar = controles.Player.Look.ReadValue<Vector2>();
        bool corriendo = controles.Player.Sprint.IsPressed();

        MoverJugador(corriendo);
        RotarCamara();
    }

    private void MoverJugador(bool corriendo) {
        Vector3 direccion = (transform.right * inputMovimiento.x
                           + transform.forward * inputMovimiento.y).normalized;

        float vel = corriendo ? velocidadCorrer : velocidad;
        transform.Translate(direccion * vel * Time.deltaTime, Space.World);
    }

    private void RotarCamara() {
        if (inputMirar == Vector2.zero) return;

        float sensibilidad = Gamepad.current != null
                             ? sensibilidadMando : sensibilidadMouse;

        float rotX = inputMirar.x * sensibilidad;
        transform.Rotate(Vector3.up * rotX);

        float rotY = inputMirar.y * sensibilidad;
        rotacionCamaraX -= rotY;
        rotacionCamaraX = Mathf.Clamp(rotacionCamaraX, -80f, 80f);

        if (camaraJugador != null) {
            camaraJugador.localRotation = Quaternion.Euler(rotacionCamaraX, 0f, 0f);
        }
    }

    private void OnSaltar(InputAction.CallbackContext contexto) {
        if (estaEnSuelo) {
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
            Vector3 dir = collision.gameObject.transform.position - transform.position;
            rbImpacto.AddForce(dir.normalized * 2f, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Moneda")) {
            Debug.Log("Moneda recogida");
            monedasTotales++;
            NotificarCambioUI();
            Destroy(other.gameObject);
        }
    }


    private void NotificarCambioUI(){
            uiManager.ActualizarHUD(salud, 100, monedasTotales, vidasRestantes);
    }
}
