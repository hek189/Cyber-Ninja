using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerAttack : MonoBehaviour 
{
    private Controls inputActions;
    private Animator animator;

    public float attackCooldown;
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
        
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        Collider2D[] enemiesArray = Physics2D.OverlapCircleAll(hitbox.position, range, enemies);
        //TO DO
        //Collider2D[] enemiessArray = Physics2D.OverlapCapsuleAll(hitbox.position, range, enemies);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(attackSound);
        foreach(Collider2D enemy in enemiesArray)
        {
            Debug.Log("me fumo un alonso");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (hitbox != null)
        {
            Gizmos.DrawWireSphere(hitbox.position, range);
        }
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
