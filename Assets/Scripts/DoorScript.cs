using UnityEngine;

public class DoorScript : MonoBehaviour
{
    private Animator animator;
    private AudioSource source;
    private bool isClosed = true;
    public AudioClip doorSound;

    private void Start()
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isClosed)
        {
            isClosed = false;
            source.PlayOneShot(doorSound);
            animator.SetBool("isOpen", true);
        }
    }
}
