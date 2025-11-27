using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Floater : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private LayerMask _layerMask;
    private float _maxSpeed;

    private float _shipInput;
    private float _sailInput;

    private RaycastHit _currentHit;
    private bool _onWater;
    private InputSystem _inputSystem;
    private IBehaviour[] _behaviours;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Initialize(InputSystem inputSystem, SceneSettings settings, params IBehaviour [] behaviours)
    {
        _layerMask = settings.LayerMask;
        _maxSpeed = settings.MaxSpeed;

        _rigidbody.maxLinearVelocity = _maxSpeed;

        _inputSystem = inputSystem;

        _behaviours = behaviours;
    }

    private void Update()
    {
        if (_behaviours == null)
            return;

        _onWater = Physics.Raycast(transform.position + Vector3.up * 15, Vector3.down, out _currentHit, 18f, _layerMask);

        if (_onWater == false)
            return;

        _shipInput = _inputSystem.GetHorizontalInputAD();
        _sailInput = _inputSystem.GetHorizontalInputQE();

        foreach (var behaviour in _behaviours)
        {
            if (behaviour is IUpdatable)
            {
                if (behaviour is IShipUpdatable)
                    ((IShipUpdatable)behaviour).Tick(_shipInput);

                if (behaviour is ISailUpdatable)
                    ((ISailUpdatable)behaviour).Tick(_sailInput);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_onWater == false)
            return;

        foreach (var behaviour in _behaviours)
        {
            if (behaviour is IHittable)
            {
                ((IHittable)behaviour).Recive(_currentHit);
            }

            behaviour.Execute();
        }
    }
}