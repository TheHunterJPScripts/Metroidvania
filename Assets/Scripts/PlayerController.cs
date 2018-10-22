

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

    private Rigidbody2D rb;

    [Header("Velocidad:")]
    [Tooltip("Velocidad que se aplicará.")]
    public float speed;
    private float moveInput;

    [Header("Detección de suelo:")]
    [Tooltip("Posición en la que se mirara si hay suelo.")]
    public Transform feetPos;
    [Tooltip("Radio de detección de suelo.")]
    public float checkRadius;
    [Tooltip("Layers que se considerarán como suelo.")]
    public LayerMask whatIsGround;
    private bool isGrounded;

    [Header("Salto:")]
    [Tooltip("Fuerza de salto.")]
    public float jumpForce;
    private float jumpTimeCounter;
    [Tooltip("Duración maxima del salto antes de empezar a caer.")]
    public float jumpTime;
    private bool isJumping;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Movimento lateral.
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
    private void Update()
    {
        // Sanity Check.
        if (feetPos == null)
        {
            EditorApplication.isPlaying = false;
            throw new System.Exception("[PlayerController] Error: No se ha asignado ningún 'Tranform' a la variable 'feetPos'");
        }


        // Modifica la dirección donde apunta la imagen del personaje
        // según hacia donde se mueva.
        if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0,0,0);
        } else if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }

        // Detecta si el personaje está en el suelo.
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        // Detecta si se ha presionado el botón de saltar.
        bool spaceDown = Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1");
        bool space = Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1");
        bool spaceUp = Input.GetKeyUp(KeyCode.Space) || Input.GetButtonUp("Fire1");

        // Si se acaba de presionar el botón de saltar.
        if(isGrounded && spaceDown && isJumping == false)
        {
            // Salta
            rb.velocity = Vector2.up * jumpForce;
            jumpTimeCounter = jumpTime;
            isJumping = true;
        }
        // Si el botón se esta dejando presionado.
        if(space)
        {
            // Si aún le queda tiempo de salto.
            if(jumpTimeCounter > 0 && isJumping == true)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            } else
            {
                isJumping = false;
            }
        }
        // Si el botón se ha soltado.
        if(spaceUp)
        {
            isJumping = false;
        }
    }
}
