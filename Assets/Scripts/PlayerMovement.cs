using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float MovX;
    public float JumpForce, Speed;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MovX = Input.GetAxisRaw("Horizontal");

        LeftOrRight(MovX);
        Animator.SetBool("isRunning", MovX != 0);

        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
        {
            Animator.SetTrigger("jump");
            Rigidbody2D.AddForce(Vector2.up * JumpForce);
            
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(MovX * Speed, Rigidbody2D.velocity.y);
    }

    /**
     * Checks if the player is moving left or right
     */
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

    /**
     * Checks if the player is on the ground
     */
    private bool IsOnGround()
    {
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.5f))
        {
            return true;
        }
        return false;
    }
}
