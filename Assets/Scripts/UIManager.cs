using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Referencias de UI")]
    public Image barraSalud;
    public TextMeshProUGUI textoMonedas;
    public TextMeshProUGUI textoVidas;

    // Método para actualizar la barra y el color
    public void ActualizarHUD(int saludActual, int saludMax, int monedas, int vidas)
    {
        // 1. Lógica de la barra (valor entre 0 y 1)
        float porcentaje = (float)saludActual / saludMax;
        barraSalud.fillAmount = porcentaje;

        // 2. Actualización de textos
        textoMonedas.text = "" + monedas;
        textoVidas.text = "" + vidas;
    }
}
