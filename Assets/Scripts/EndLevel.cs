using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        var p = other.gameObject.GetComponent<PlayerBehaviour>();
        if(p!=null)
        {
            p.Win();
        }
    }
}
