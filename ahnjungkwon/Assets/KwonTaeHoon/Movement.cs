using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] // 다른 클래스에선 건들 수 없지만 Inspector에선 수정 가능
    private float velocity = 5f;

    private Vector3 moveDirection = Vector3.zero;
    public bool isRunning = false;
    public bool isJumping = false;

    private CharacterController characterController; // 캐릭터 컨트롤러 변수생성

    private float jumpForce = 6f;
    public float runSpeed = 2f;

    // 중력가속도 지정
    private float gravity = -9.8f;

    // Script 객체가 생성될 때 호출되는 메서드
    private void Awake()
    {
        characterController = GetComponent<CharacterController>(); // 캐릭터 컴포넌트 가져오기 
    }

    // Update is called once per frame
    void Update()
    {
        if (!characterController.isGrounded) // 캐릭터가 땅에서 떨어져있다면
        {
            moveDirection.y += gravity * Time.deltaTime;
        }
        characterController.Move(runSpeed * moveDirection * velocity * Time.deltaTime); // 매 프레임마다 이동시키는 메소드 호출
    }

    public void Move(Vector3 direction)
    {
        moveDirection = new Vector3(direction.x, moveDirection.y, direction.z); // y엔 중력값 추가
    }

    public void Jump()
    {
        if (characterController.isGrounded)
        {
            moveDirection.y += jumpForce;
            isJumping = true;
        }
        
    }
}
