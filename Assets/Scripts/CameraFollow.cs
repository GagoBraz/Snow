using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    [SerializeField]
    float smoothSpeed = 0.125f;

    [SerializeField]
    Vector3 offset = new Vector3(0, 0, 0);

    private void FixedUpdate()
    {
        Vector3 desiredPosition = Target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
