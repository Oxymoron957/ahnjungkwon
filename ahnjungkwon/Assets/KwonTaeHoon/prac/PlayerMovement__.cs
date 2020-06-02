using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class PlayerMovement__ : MonoBehaviour
{
    Vector3 m_Movement;
    Animator m_Animator;
    
    Quaternion m_Rotation = Quaternion.identity;
    Rigidbody m_Rigidbody;

    [SerializeField]
    private float moveSpeed = 8f;
    [SerializeField]
    private float jumpForce = 5f;
    [SerializeField]
    private float turnSpeed = 20f;
    [SerializeField]
    private float speed = 0.5f;

    private bool isJumping = false;
    

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("isWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        isJumping = false;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            isJumping = true;
        }

        m_Animator.SetBool("isJumping", isJumping);
    }


    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * moveSpeed);
        m_Rigidbody.MoveRotation(m_Rotation);

    }

    private void Jump()
    {
        m_Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
*/

public class PlayerMovement__ : MonoBehaviour
{
    private Animator animator;
    private CharacterController characterController;

    [SerializeField]
    private float moveSpeed = 8f;
    [SerializeField]
    private float turnSpeed = 150f;
    [SerializeField]
    private float jumpForce = 6f;

    private float gravity = 9.8f;
    private bool isJumping = false;

    Vector3 movement = Vector3.zero;

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

        animator.SetFloat("HorizontalVelocity", horizontal);
        animator.SetFloat("VerticalVelocity", vertical);

        transform.Rotate(Vector3.up, horizontal * turnSpeed * Time.deltaTime); 

        isJumping = false;
        if (characterController.isGrounded)
        {
            movement = new Vector3(horizontal, 0, vertical);
            movement = transform.TransformDirection(movement);
            movement *= moveSpeed;

            if (Input.GetButton("Jump"))
            {
                movement.y = jumpForce;
                isJumping = true;
                animator.SetBool("isJumping", isJumping);
            }

        }
        movement.y -= gravity * Time.deltaTime;
        characterController.Move(movement * Time.deltaTime);

    }
}

