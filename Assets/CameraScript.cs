using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10);
    public float mouseInfluence = 0.5f;
    public float maxDistance = 5f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        if (target == null || cam == null) return;

        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 targetPosition = target.position + offset;
        Vector3 directionToMouse = mousePos - target.position;
        Vector3 lerpedPosition = Vector3.Lerp(targetPosition, mousePos, mouseInfluence);

        Vector3 finalPosition = Vector3.Lerp(targetPosition, lerpedPosition, smoothSpeed);

        transform.position = finalPosition;
    }
}