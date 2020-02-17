using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlatformerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    [SerializeField]
    private Vector3 movement;
    [SerializeField]
    private float stepSpeed = 2.5f;
    [SerializeField]
    private float airManeuverSpeed = 0.25f;
    [SerializeField]
    private float jumpForce = 7.5f;

    private readonly int speedAnimId = Animator.StringToHash("Speed");
    private readonly int jumpAnimId = Animator.StringToHash("Jump");
    private readonly int fallAnimId = Animator.StringToHash("VerticalSpeed");
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        movement = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 currentMovement = new Vector3(x, 0.0f, z).normalized;
        animator.SetFloat(speedAnimId, currentMovement.sqrMagnitude);
        if (currentMovement.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(currentMovement, transform.up);
        }
        if (controller.isGrounded)
        {
            currentMovement *= stepSpeed;
            if (Input.GetButton("Jump"))
            {
                animator.SetTrigger(jumpAnimId);
                currentMovement.y = jumpForce;
            }
        }
        else
        {
            currentMovement = stepSpeed * new Vector3(
                movement.x + airManeuverSpeed * currentMovement.x,
                0,
                movement.z + airManeuverSpeed * currentMovement.z).normalized;
            currentMovement.y = movement.y;
        }
        movement = currentMovement;
        movement.y -= PhysicsHelper.GravityStrength * Time.deltaTime;
        animator.SetFloat(fallAnimId, movement.y);
        controller.Move(Time.deltaTime * movement);
    }
}
