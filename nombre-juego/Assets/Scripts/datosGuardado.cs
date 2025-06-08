using UnityEngine;

[System.Serializable]
public class datosGuardado
{
    public Vector3 posicionJugador;
    public int personajeActivo;
    public bool cooldownD;
    public bool cooldownJ;

    public int indiceNivel;       // Cambiado de string a int
    public float vidaActual;
}
