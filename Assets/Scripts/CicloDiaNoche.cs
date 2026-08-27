using UnityEngine;

public class CicloDiaNoche : MonoBehaviour{
    [Header("Configuración del Ciclo")]
    [Tooltip("Cantidad de minutos reales que toma completar un día entero de 24 horas (360 grados).")]
    public float minutosPorDia = 1.0f;

    void Update() {
        if (minutosPorDia <= 0.001f) {
            minutosPorDia = 0.001f;
        }

        float gradosPorSegundo = 360f / (minutosPorDia * 60f);
        transform.Rotate(Vector3.right * gradosPorSegundo * Time.deltaTime);
    }
}
