using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaStaminaPlayer : SistemaPlayer
{
    protected override void Awake()
    {
        base.Awake();
        Main.SistemaStamina = this;
    }
}
