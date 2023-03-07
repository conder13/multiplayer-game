using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Netcode;
using UnityEngine;

public class ThirdPersonMovement : NetworkBehaviour
{
    public CharacterController controller;
    public Transform cam;

    public float speed = 6;
    public float jumpHeight = 3;
    Vector3 velocity;
    bool isGrounded;


    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        //jump
        if(!IsLocalPlayer) return;

        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}


