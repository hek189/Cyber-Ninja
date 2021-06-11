using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //direction = player.transform.position - transform.position;
        animator.SetBool("isMoving", speed != 0);
    }

    private void FixedUpdate()
    {
        if (IsFacingRight())
        {
            body.velocity = new Vector2(speed, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(-speed, body.velocity.y);
        }
    }
    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(body.velocity.x)), transform.localScale.y);
    }
}
