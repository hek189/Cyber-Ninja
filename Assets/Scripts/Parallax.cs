using UnityEngine;

public class Parallax : MonoBehaviour
{
    // CAMARA MULTIPLANO
    private float length, startpos;
    public Camera camara;
    public float parallaxEffect = 1;
    // Start is called before the first frame update
    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (camara.transform.position.x * (1 - parallaxEffect));
        float dist = (camara.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}
