using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour


{
    private Animator animator;
    private Vector2 direction;
    private bool canAttack;
    private float nextAttackTimer;
    private Collider2D[] playerArray;
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
        playerArray = Physics2D.OverlapCircleAll(hitbox.position, range, playerLayer);
        if(canAttack && playerArray.Length >0)
        {
            Attack();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        GetComponent<AudioSource>().PlayOneShot(attackSound);
        foreach (Collider2D player in playerArray)
        {
            player.GetComponent<PlayerBehaviour>().Die();
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
