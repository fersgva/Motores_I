using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class BolaPlayer : MonoBehaviour
{
    [SerializeField] private float distanciaDeteccionSuelo;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float fuerza;

    private PlayerInput input;
    private Rigidbody rb;

    private float hInput, vInput;

    private Vector2 inputMovement;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        input.actions["Jump"].started += Jump;
        input.actions["Move"].performed += UpdateMovement;
        input.actions["Move"].canceled += UpdateMovement;

    }
    private void UpdateMovement(InputAction.CallbackContext ctx)
    {
        inputMovement = ctx.ReadValue<Vector2>();
    }


    private void Jump(InputAction.CallbackContext obj)
    {
        //Y se detecta algo bajo mis pies...
        if (Physics.Raycast(transform.position, Vector3.down, distanciaDeteccionSuelo))
        {
            Debug.DrawRay(transform.position, Vector3.down * distanciaDeteccionSuelo, Color.red, 3);
            rb.AddForce(Vector3.up.normalized * fuerzaSalto, ForceMode.Impulse);

        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(inputMovement.x, 0, inputMovement.y) * fuerza, ForceMode.Force);

    }

    private void OnDisable()
    {
        input.actions["Jump"].started -= Jump;
    }
}
