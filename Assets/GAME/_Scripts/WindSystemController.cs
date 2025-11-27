using UnityEngine;

public class WindSystemController : MonoBehaviour
{
    [SerializeField] private Transform _flag;
    [SerializeField] private float _secondsToChangeWindDirection;
    [SerializeField] private float _speedForChangeWindDirection;

    private float _timer;

    private Vector3 _previousWindDirection;
    public Vector3 CurrentWindDirection { get; private set; }
    private Vector3 _newWindDirection;

    private void Start()
    {
        _previousWindDirection = _newWindDirection = _flag.forward;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        CurrentWindDirection = Vector3.Lerp(_previousWindDirection, _newWindDirection, _timer / _speedForChangeWindDirection);

        _flag.rotation = Quaternion.LookRotation(CurrentWindDirection, Vector3.up);

        if (_timer < _secondsToChangeWindDirection)
            return;

        _timer = 0;

        ChangeWundDirection();
    }

    private void ChangeWundDirection()
    {
        _previousWindDirection = _newWindDirection;
        Vector3 randomDirection = Random.insideUnitCircle.normalized;
        _newWindDirection = new Vector3(randomDirection.x, 0f, randomDirection.y);
    }
}