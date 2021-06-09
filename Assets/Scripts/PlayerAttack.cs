using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour 
{
    private Controls inputActions;
    private Animator animator;
    private float nextAttackTimer = 0f;
    private bool canAttack;

    public float attackRate;
    public AudioClip attackSound;
    public Transform hitbox;
    public float range;
    public LayerMask enemies;


    private void Awake()
    {
        inputActions = new Controls();
        inputActions.Player.Attack.performed += context => Attack();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        canAttack = (Time.time >= nextAttackTimer);
    }

    private void Attack()
    {
        if (gameObject.GetComponent<PlayerBehaviour>().IsOnGround() && canAttack)
        {
            animator.SetTrigger("attack");
            Collider2D[] enemiesArray = Physics2D.OverlapCircleAll(hitbox.position, range, enemies);
            GetComponent<AudioSource>().PlayOneShot(attackSound);
            foreach (Collider2D enemy in enemiesArray)
            {
                enemy.GetComponent<EnemyBehaviour>().Die();
            }
            nextAttackTimer = Time.time + 1f / attackRate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hitbox != null)
        {
            Gizmos.DrawWireSphere(hitbox.position, range);
        }
    }

    public void DisableInput()
    {
        inputActions.Player.Disable();
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
