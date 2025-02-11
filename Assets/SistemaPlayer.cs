using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaPlayer : MonoBehaviour
{
    private Jugador main;

    public Jugador Main { get => main;}

    protected virtual void Awake()
    {
        main = transform.root.GetComponent<Jugador>();
    }
}
