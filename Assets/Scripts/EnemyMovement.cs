using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, ITurnable
{
    Rigidbody2D body;
    public GameObject player;
    public float speed = 3;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = player.transform.position - transform.position;
        LeftOrRight(direction.x);
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(speed, body.velocity.y);
    }

    public void LeftOrRight(float AxisX)
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

    private bool IsHittingWall()
    {
        if (Physics2D.Raycast(transform.position, Vector3.left, 0.5f))
        {
            return true;
        }
        return false;
    }
}
