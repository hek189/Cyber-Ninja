using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour


{
    private Animator animator;
    private Vector2 direction;
    private bool canAttack;
    private float nextAttackTimer = 0f;
    /****************************/
    public GameObject player;
    public AudioClip deathSound;
    public float attackRate;
    public AudioClip attackSound;
    public Transform hitbox;
    public float range;
    public LayerMask playerLayer;
    public float distanceFromPlayer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        canAttack = (Time.time >= nextAttackTimer);
        if (canAttack && Mathf.Abs(direction.x) < distanceFromPlayer)
        {
            Attack();
        }

    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        Collider2D[] enemiesArray = Physics2D.OverlapCircleAll(hitbox.position, range, playerLayer);
        GetComponent<AudioSource>().PlayOneShot(attackSound);
        foreach (Collider2D enemy in enemiesArray)
        {
            enemy.GetComponent<PlayerBehaviour>().Die();
        }
        nextAttackTimer = Time.time + 1f / attackRate;
    }

    public void Die()
    {
        animator.SetTrigger("die");
        GetComponent<AudioSource>().PlayOneShot(deathSound);
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void OnDrawGizmosSelected()
    {
        if (hitbox != null)
        {
            Gizmos.DrawWireSphere(hitbox.position, range);
        }
    }

}
