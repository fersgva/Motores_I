using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    protected PlayerLifesSO main;
    protected virtual void Awake()
    {
        main = transform.root.GetComponent<PlayerLifesSO>();
    }
}
