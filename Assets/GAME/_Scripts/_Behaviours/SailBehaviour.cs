using UnityEngine;

public class SailBehaviour : IBehaviour, ISailUpdatable
{
    private float _currentSailAngle = 0f;
    private float _sailRotationSpeed;

    private Transform _ship;
    private Transform _sail;

    public SailBehaviour(float sailRotationSpeed, Transform ship, Transform sail)
    {
        _sailRotationSpeed = sailRotationSpeed;
        _ship = ship;
        _sail = sail;
    }

    public void Execute()
    {

    }

    public void Tick(float value)
    {
        _currentSailAngle += value * _sailRotationSpeed * Time.deltaTime;
        _currentSailAngle = Mathf.Clamp(_currentSailAngle, -90f, 90f);

        _sail.rotation = _ship.rotation * Quaternion.Euler(0f, _currentSailAngle, 0f);
    }
}