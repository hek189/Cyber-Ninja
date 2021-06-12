using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private AudioSource source;
    public AudioClip doorSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        source.PlayOneShot(doorSound);
        animator.SetBool("isOpen", true);
    }
}
