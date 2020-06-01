using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController__ : MonoBehaviour
{
    [SerializeField]
    private float sensitivity = 1f;

    private CinemachineComposer composer;
    
    private void Start()
    {
        composer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineComposer>();
    }

    void Update()
    {
        float vertical = Input.GetAxis("Mouse Y") * sensitivity;
        float horizontal = Input.GetAxis("Mouse X") * sensitivity;

        composer.m_TrackedObjectOffset.y += vertical;
        composer.m_TrackedObjectOffset.y = Mathf.Clamp(composer.m_TrackedObjectOffset.y, -3, 3);

        composer.m_TrackedObjectOffset.x += horizontal;
        composer.m_TrackedObjectOffset.x = Mathf.Clamp(composer.m_TrackedObjectOffset.x, -3, 3);
    }
}
