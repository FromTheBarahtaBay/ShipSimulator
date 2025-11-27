using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraViewChanger
{
    private Queue<CinemachineVirtualCamera> _camerasQueue = new Queue<CinemachineVirtualCamera>();
    private Transform _target;

    public CameraViewChanger(List<CinemachineVirtualCamera> cameras, Transform target)
    {
        _target = target;

        foreach (var camera in cameras)
            _camerasQueue.Enqueue(camera);

        SetTarget();
    }

    public void ChangeView()
    {
        CinemachineVirtualCamera currentCamera = _camerasQueue.Dequeue();

        currentCamera.gameObject.SetActive(true);

        foreach (var camera in _camerasQueue)
            camera.gameObject.SetActive(false);

        _camerasQueue.Enqueue(currentCamera);
    }

    private void SetTarget()
    {
        foreach (var camera in _camerasQueue)
        {
            camera.Follow = _target;
            camera.LookAt = _target;
        }
    }
}