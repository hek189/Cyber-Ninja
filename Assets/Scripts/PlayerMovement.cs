using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D Rigidbody2D;
    private float MovX;
    public float JumpForce, Speed;
    //private bool IsOnGround;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovX = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
        {
            Rigidbody2D.AddForce(Vector2.up * JumpForce);
        }
    }

    private void Jump()
    {

    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(MovX * Speed, Rigidbody2D.velocity.y);
    }

    private bool IsOnGround()
    {
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
        {
            return true;
        }
        return false;
    }


}
