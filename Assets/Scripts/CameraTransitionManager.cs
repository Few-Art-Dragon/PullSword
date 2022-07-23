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
        //_currentCamera.Priority++;
    }

    public void UpdateCamera(CinemachineVirtualCamera target)
    {
        CheckNameCamera(target);
        _currentCamera.Priority--;
        _currentCamera = target;
        _currentCamera.Priority++;
    }
    private void CheckNameCamera(CinemachineVirtualCamera target)
    {
        if (target.name == "CameraGame")
        {
            GameManager.SetGameEvent.Invoke();
        }
        else if (target.name == "CameraMainMenu")
        {
            GameManager.SetMainMenuEvent.Invoke();
        }
        else if (target.name == "CameraCollection")
        {
            GameManager.SetCollectionEvent.Invoke();
        }

    }
}
