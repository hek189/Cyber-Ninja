using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
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
    public float deathTimerOffset = 0.5f;
    public float FallToDeathDistanceY = -10;

    void Awake()
    {
        inputActions = new Controls();
        inputActions.Player.Move.performed += context => movX = context.ReadValue<float>();
        inputActions.Player.Move.canceled += context => movX = 0;
        inputActions.Player.Jump.performed += context => Jump();
        inputActions.Player.Dash.performed += context => Dash();
        inputActions.Player.Debug.performed += _ => Die();
    }

    void Start()
    {
        leaderboardManager = GetComponent<LeaderboardManager>();
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
        if (body.position.y < FallToDeathDistanceY)
        {
            FallToDeath();
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

    public void Die()
    {
        PlayerPrefs.SetInt("nDeaths", PlayerPrefs.GetInt("nDeaths") + 1);
        animator.SetTrigger("die");
        Camera.main.GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(deathSound);
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + deathTimerOffset);
    }

    private void FallToDeath()
    {
        PlayerPrefs.SetInt("nDeaths", PlayerPrefs.GetInt("nDeaths") + 1);
        Camera.main.GetComponent<AudioSource>().Stop();
        Destroy(gameObject, deathTimerOffset * 2);
    }

    public void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); ;
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
