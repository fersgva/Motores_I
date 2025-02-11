using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerDynamic : MonoBehaviour
{
    [SerializeField] private ClasesMotores.CanvasManagerr canvas;
    [SerializeField] private float fuerza;
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private float vidaInicial;
    [SerializeField] private float distanciaDeteccionSuelo;
    [SerializeField] private float distanciaDeteccionInteractuable;


    private float score;
    private Rigidbody rb;
    private float hInput, vInput;
    private float fuerzaSaltoMaxima;
    private float vidaActual;
    private Vector3 posicionInicial;
    private PlayerInput input;

    // Start is called before the first frame update
    void Start()
    {
        posicionInicial = transform.position;
        rb = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        input.actions["Jump"].started += Jump;
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


    // Update is called once per frame
    void Update()
    {
        hInput = Input.GetAxisRaw("Horizontal"); //-1, 0, 1
        vInput = Input.GetAxisRaw("Vertical"); //-1, 0, 1

 

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hit, distanciaDeteccionInteractuable))
            {
                Debug.DrawRay(transform.position, transform.position + Vector3.forward * 10, Color.red);
                if(hit.transform.TryGetComponent(out Panel panel))
                {
                    panel.Interactuar();
                }
            }
        }



    }
    //Ciclo de físicas: Es donde se gestiona la física.
    //Update VS Fixed: SON INDEPNEDIENTES. fijo: se ejecuta en un ratio constante.
    //físicas continuas, que necesitan cálculos a lo largo de un tiempo.
    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(hInput, 0, vInput).normalized * fuerza, ForceMode.Force);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out CuboOro cuboOro))
        {
            score++;
            canvas.ScoreText.text = "Score: " + score.ToString();
            Destroy(other.gameObject);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Cilindro"))
        {
            vidaActual -= 25;
            canvas.HealthBar.fillAmount = vidaActual / vidaInicial;
            Destroy(gameObject, 3f);
        }
        if(other.transform.TryGetComponent(out Techo techo))
        {
            transform.position = posicionInicial;
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.isKinematic = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }
    }
}
