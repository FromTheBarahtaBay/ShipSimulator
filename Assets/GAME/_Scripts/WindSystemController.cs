using UnityEngine;

public class WindSystemController
{
    private Transform _flag;

    private float _secondsToChangeWindDirection;
    private float _speedForChangeWindDirection;

    private float _timer;

    private Vector3 _previousWindDirection;
    private Vector3 _newWindDirection;

    public WindSystemController(Transform flag, float secondsToChangeWindDirection, float speedForChangeWindDirection)
    {
        _flag = flag;
        _secondsToChangeWindDirection = secondsToChangeWindDirection;
        _speedForChangeWindDirection = speedForChangeWindDirection;

        _previousWindDirection = _newWindDirection = _flag.forward;
    }

    public Vector3 CurrentWindDirection { get; private set; }

    public void Blow()
    {
        _timer += Time.deltaTime;

        CurrentWindDirection = Vector3.Lerp(_previousWindDirection, _newWindDirection, _timer / _speedForChangeWindDirection);

        _flag.rotation = Quaternion.LookRotation(CurrentWindDirection);

        if (_timer < _secondsToChangeWindDirection)
            return;

        _timer = 0;

        ChangeWindDirection();
    }

    private void ChangeWindDirection()
    {
        _previousWindDirection = _newWindDirection;
        Vector3 randomDirection = Random.insideUnitCircle.normalized;
        _newWindDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
    }
}