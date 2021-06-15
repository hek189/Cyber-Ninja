using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    private Animator animator;
    private Controls inputActions;
    private float movX;
    private Rigidbody2D body;
    private bool alreadyFallen = false;
    private AudioSource audioSource;
    /***********************************************/
    public float jumpForce, speed;
    public AudioClip jumpSound, deathSound, victorySound, fallToDeathSound;
    public float FallToDeathDistanceY = -10;

    void Awake()
    {
        inputActions = new Controls();
        inputActions.Player.Move.performed += context => movX = context.ReadValue<float>();
        inputActions.Player.Move.canceled += context => movX = 0;
        inputActions.Player.Jump.performed += context => Jump();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }



    // Update is called once per frame
    void Update()
    {
        LeftOrRight(movX);
        animator.SetBool("isRunning", movX != 0);
        animator.SetBool("isJumping", !IsOnGround());
        animator.SetFloat("yDirection", body.velocity.y);
        if (body.position.y < FallToDeathDistanceY && !alreadyFallen)
        {
            alreadyFallen = true;
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
            audioSource.PlayOneShot(jumpSound);
        }
    }

    private void LeftOrRight(float AxisX)
    {
        if (AxisX < 0.0f)
        {
            transform.localScale = new Vector2(-1.0f, 1.0f);
        }
        else if (AxisX > 0.0f)
        {
            transform.localScale = new Vector2(1.0f, 1.0f);

        }
    }

    public bool IsOnGround()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.5f))
        {
            return true;
        }
        return false;
    }

    public void Die()
    {
        DisableMovement();
        PlayerPrefs.SetInt("nDeaths", PlayerPrefs.GetInt("nDeaths") + 1);
        animator.SetBool("isDead", true);
        Camera.main.GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(deathSound);
        Invoke("Respawn", fallToDeathSound.length);
    }

    private void FallToDeath()
    {
        DisableMovement();
        PlayerPrefs.SetInt("nDeaths", PlayerPrefs.GetInt("nDeaths") + 1);
        Camera.main.GetComponent<AudioSource>().Stop();
        audioSource.PlayOneShot(fallToDeathSound);
        Invoke("Respawn", fallToDeathSound.length);

    }

    public void Win()
    {
        DisableMovement();
        animator.SetTrigger("victory");
        Camera.main.GetComponent<AudioSource>().Stop();
        audioSource.PlayOneShot(victorySound);
        Invoke("NextLevel", victorySound.length);
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    private void DisableMovement()
    {
        enabled = false;
        inputActions.Player.Disable();
        GetComponent<PlayerAttack>().DisableInput();
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