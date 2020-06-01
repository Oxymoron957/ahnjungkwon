using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardController : MonoBehaviour
{
    // 캐릭터가 나아가야할 방향을 설정하는 스크립트
    private Movement movement;
    private Animator animator;
    private Vector3 direction;

    private float rotationX = 0f;
    private float rotationY = 0f;
    private float rotationSpeed = 80f;

    private float mouseSensitive = 3f;

    Vector3 m_Movement;
    Rigidbody m_Rigidbody;
    Quaternion m_Rotation = Quaternion.identity;
    public float turnSpeed = 20f;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        movement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.isRunning = false;
        movement.isJumping = false;
        movement.runSpeed = 1f;
        float mouseInputX = Input.GetAxis("Mouse X");
        float mouseInputY = Input.GetAxis("Mouse Y");

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        m_Movement.Set(horizontalInput, 0f, verticalInput);

        direction = new Vector3(horizontalInput, 0, verticalInput);
        m_Movement.Normalize();

        rotationX += mouseSensitive * mouseInputX * rotationSpeed * Time.deltaTime; // 회전값 산출 및 증가
        rotationY += mouseSensitive * mouseInputY * rotationSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, rotationX, 0); // 옆으로 회전 -> y축
        

        direction = transform.TransformDirection(direction); // local과 world의 z축 차이를 계산해서 실제 움직여야하는 방향벡터 계산

        if (Input.GetKeyDown(KeyCode.Space))
        {
            movement.Jump();
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement.isRunning = true;
            movement.runSpeed = 2f;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetTrigger("Attack");
        }

        animator.SetFloat("HorizontalVelocity", horizontalInput);
        animator.SetFloat("VerticalVelocity", verticalInput);
        animator.SetBool("isRunning", movement.isRunning);
        animator.SetBool("isJumping", movement.isJumping);

        movement.Move(direction);
    }

    private void FixedUpdate()
    {
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

    }

    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement);
        m_Rigidbody.MoveRotation(m_Rotation);

    }
}
