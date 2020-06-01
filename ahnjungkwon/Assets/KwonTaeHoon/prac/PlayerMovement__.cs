using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement__ : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;


    [SerializeField]
    private float moveSpeed = 8f;
    [SerializeField]
    private float backwardMoveSpeed = 4f;
    [SerializeField]
    private float turnSpeed = 150f;
    [SerializeField]
    private float jumpForce = 6f;

    private Vector3 movement = Vector3.zero;
    private float gravity = -9.8f;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0, vertical);

        animator.SetFloat("HorizontalVelocity", horizontal);
        animator.SetFloat("VerticalVelocity", vertical);
        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime);

        if (vertical != 0)
        {
            float moveSpeedToUse = vertical > 0 ? moveSpeed : backwardMoveSpeed;
            characterController.SimpleMove(transform.forward * moveSpeedToUse * vertical);
        }

        if (!characterController.isGrounded) // 캐릭터가 땅에서 떨어져있다면
        {
            movement.y += gravity * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            movement.y += jumpForce;
        }

    }
}
