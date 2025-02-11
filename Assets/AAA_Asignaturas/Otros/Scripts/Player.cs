using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifesSO : MonoBehaviour
{
    private float velocidadMovimientoActual;
    private float velocidadPorDefecto;

    public float VelocidadMovimientoActual { get => velocidadMovimientoActual; set => velocidadMovimientoActual = value; }
    public float VelocidadPorDefecto { get => velocidadPorDefecto; set => velocidadPorDefecto = value; }
}
