using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector3 minValues, maxValues;

    private void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 bounds = new Vector3(Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x), Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y), Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z));
        transform.position = bounds;
    }

    private void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 bounds = new Vector3(Mathf.Clamp(targetPosition.x, minValues.x, maxValues.x), Mathf.Clamp(targetPosition.y, minValues.y, maxValues.y), Mathf.Clamp(targetPosition.z, minValues.z, maxValues.z));
        transform.position = bounds;
    }
}
