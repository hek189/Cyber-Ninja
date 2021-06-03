using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        var p = other.gameObject.GetComponent<PlayerMovement>();
        if(p!=null)
        {
            p.Win();
        }
    }
}
