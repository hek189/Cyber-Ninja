using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Controls inputActions;
    private float movX;
    private Rigidbody2D body;

    public float jumpForce, speed;
    public AudioClip jumpSound;
    public AudioClip deathSound;

    void Awake()
    {
        inputActions = new Controls();
        inputActions.Player.Move.performed += context => movX = context.ReadValue<float>();
        inputActions.Player.Move.canceled += context => movX = 0;
        inputActions.Player.Jump.performed += context => Jump();
        inputActions.Player.Debug.performed += _ => Die();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LeftOrRight(movX);
        animator.SetBool("isRunning", movX != 0);
        animator.SetBool("isJumping", !IsOnGround());
        animator.SetFloat("yDirection", body.velocity.y);
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(movX * speed, body.velocity.y);
    }

    private void Jump()
    {
        if (IsOnGround())
        {
            body.AddForce(Vector2.up * jumpForce);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }
    }

    private void LeftOrRight(float AxisX)
    {
        if (AxisX < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (AxisX > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    public bool IsOnGround()
    {
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.5f))
        {
            return true;
        }
        return false;
    }

    private void Die()
    {
        animator.SetTrigger("die");
        
        Camera.main.GetComponent<AudioSource>().PlayOneShot(deathSound);
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }
}
