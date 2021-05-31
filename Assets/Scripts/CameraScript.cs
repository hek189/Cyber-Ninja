using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    //public Transform background;

    public Vector3 offset;

    private void LateUpdate()
    {
        transform.position = target.position + offset;
        //background.position = transform.position;

    }
}
