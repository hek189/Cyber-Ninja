using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour, ITurnable
{
    private Animator animator;
    private Controls inputActions;
    private float movX;
    private Rigidbody2D body;
    /***********************************************/
    public float jumpForce, speed;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    private LeaderboardManager leaderboardManager;
    public GameObject respawn;

    void Awake()
    {
        leaderboardManager = new LeaderboardManager();
        inputActions = new Controls();
        inputActions.Player.Move.performed += context => movX = context.ReadValue<float>();
        inputActions.Player.Move.canceled += context => movX = 0;
        inputActions.Player.Jump.performed += context => Jump();
        inputActions.Player.Dash.performed += context => Dash();
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
        if (body.position.y < -10)
        {
            Die();
        }
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
            GetComponent<AudioSource>().PlayOneShot(jumpSound);
        }
    }

    private void Dash()
    {
        if (!IsOnGround())
        {
            if (movX < 0)
            {
                body.AddForce(Vector2.left * jumpForce);
                GetComponent<AudioSource>().PlayOneShot(jumpSound);
            }
            else if (movX > 0)
            {
                body.AddForce(Vector2.right * jumpForce);
                GetComponent<AudioSource>().PlayOneShot(jumpSound);
            }
        }
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

    public bool IsOnGround()
    {
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.5f))
        {
            return true;
        }
        return false;
    }

    public void Die()
    {
        animator.SetTrigger("die");
        GetComponent<AudioSource>().PlayOneShot(deathSound);
        leaderboardManager.addDeath();
        Debug.Log(leaderboardManager.GetDeaths());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameObject.transform.position = respawn.transform.position;
    }

    public void Win()
    {
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
