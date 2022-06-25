using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTransitionManager : MonoBehaviour
{
    [SerializeField] 
    private CinemachineVirtualCamera _currentCamera;

    private void Start()
    {
        _currentCamera.Priority++;
    }

    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        _currentCamera.Priority--;
        _currentCamera = target;
        _currentCamera.Priority++;
    }
}
