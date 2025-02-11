using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    private SistemaStaminaPlayer sistemaStamina;
    private SistemaMovimientoplayer sistemaMovimiento;

    public SistemaStaminaPlayer SistemaStamina { get => sistemaStamina; set => sistemaStamina = value; }
    public SistemaMovimientoplayer SistemaMovimiento { get => sistemaMovimiento; set => sistemaMovimiento = value; }

    public void ActivarStamina()
    {
        sistemaStamina.enabled = true;
    }
}
