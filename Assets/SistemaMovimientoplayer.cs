using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaMovimientoplayer : SistemaPlayer
{
    protected override void Awake()
    {
        base.Awake();
        Main.SistemaMovimiento = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Main.ActivarStamina();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
