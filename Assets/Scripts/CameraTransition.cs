using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTransition : MonoBehaviour
{
    [SerializeField] 
    private CinemachineVirtualCamera _currentCamera;

    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        _currentCamera.Priority--;
        _currentCamera = target;
        _currentCamera.Priority++;
    }
}
