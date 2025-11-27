using UnityEngine;

public class Wave : MonoBehaviour
{
    private float _waveSpeed;
    private float _timeToDestroy;

    public void Initialize(float waveSpeed, float timeTODestroy)
    {
        _waveSpeed = waveSpeed;
        _timeToDestroy = timeTODestroy;

        GameObject.Destroy(transform.gameObject, _timeToDestroy);
    }

    private void Update()
    {
        transform.position += transform.forward * _waveSpeed * Time.deltaTime;
    }
}