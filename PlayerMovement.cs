using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] private float speed = 10;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;
    [SerializeField] private float turnSmoothTime = 0.1f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 4;
    [SerializeField] private float gravity = 25;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.4f;
    [SerializeField] private LayerMask groundMask;

    float turnSmoothVel;
    Vector3 velocity;
    bool isGrounded;


    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (velocity.y > 0 && isGrounded)
            velocity.y = -2f;

        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir * speed * Time.deltaTime);
        }

        if (isGrounded && Input.GetButton("Jump"))
            velocity.y = Mathf.Sqrt(jumpHeight * 2 * gravity);
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }
}
