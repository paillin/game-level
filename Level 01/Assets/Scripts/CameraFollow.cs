using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
   
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    public float smoothSpeed = 0.125f;


    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);

        transform.position = smoothPosition;

        transform.LookAt(target);

     
    }

}
