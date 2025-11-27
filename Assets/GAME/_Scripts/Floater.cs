using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Floater : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _rollSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _speedForce;
    [SerializeField] private float _floatForce;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private Transform _ship;
    [SerializeField] private Transform _sail;
    [SerializeField] private Transform _wind;

    [SerializeField] private float _maxBodyTilt;

    [SerializeField] private float _offsetAboveWater;
    [SerializeField] private float _long;
    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _amplitude;

    private float _shipInput;
    private float _sailInput;

    private RaycastHit _currentHit;

    private bool _onWater;

    private float _timer;

    public float rotationSpeed = 80f; 
    private float _currentSailAngle = 0f; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.maxLinearVelocity = _maxSpeed;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        _onWater = Physics.Raycast(_rigidbody.position + Vector3.up * 15, Vector3.down, out _currentHit, 18f, _layerMask);

        if (_onWater)
        {
            _shipInput = Input.GetAxisRaw("Horizontal");

            float leftInput = (Input.GetKey(KeyCode.Q)) ? -1 : 0;
            float rightInput = (Input.GetKey(KeyCode.E)) ? 1 : 0;

            _sailInput = leftInput + rightInput;

            _currentSailAngle += _sailInput * rotationSpeed * Time.deltaTime;

            _currentSailAngle = Mathf.Clamp(_currentSailAngle, -90f, 90f);

            _sail.rotation = _ship.rotation * Quaternion.Euler(0f, _currentSailAngle, 0f);
        }
    }

    private void FixedUpdate()
    {
        if (_onWater)
        {
            _rigidbody.AddTorque(transform.up * _shipInput * _rotationSpeed, ForceMode.Acceleration);

            _rigidbody.AddTorque(Vector3.Cross(transform.up, _currentHit.normal) * _rollSpeed, ForceMode.Acceleration);

            _rigidbody.AddTorque(transform.forward * _shipInput * _maxBodyTilt, ForceMode.Acceleration);

            _rigidbody.AddForce(transform.forward * GetMovingForce(_speedForce), ForceMode.Acceleration);

            Vector3 direction = (_currentHit.point + Vector3.up * _offsetAboveWater) - transform.position;

            Vector3 floating = (Vector3.up * (Mathf.Sin(_timer * _long + _offsetX) * _amplitude + _offsetY));

            _rigidbody.velocity = direction * _floatForce + floating;
        }
    }

    private float GetMovingForce(float maxSpeedForce)
    {
        float value = maxSpeedForce *
              Vector3.Dot(_ship.forward, _sail.forward) *
              Vector3.Dot(_wind.forward, _sail.forward);

        return Mathf.Max(value, 0f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up * 15, Vector3.down * 18f);
    }
}