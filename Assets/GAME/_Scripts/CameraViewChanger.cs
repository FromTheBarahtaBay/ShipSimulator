using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraViewChanger : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> _cameras = new List<CinemachineVirtualCamera>();

    private Queue<CinemachineVirtualCamera> _camerasQueue = new Queue<CinemachineVirtualCamera>();

    private void Awake()
    {
        foreach (var camera in _cameras)
            _camerasQueue.Enqueue(camera);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CinemachineVirtualCamera currentCamera = _camerasQueue.Dequeue();

            currentCamera.gameObject.SetActive(true);

            foreach (var camera in _camerasQueue)
                camera.gameObject.SetActive(false);

            _camerasQueue.Enqueue(currentCamera);
        }
    }
}