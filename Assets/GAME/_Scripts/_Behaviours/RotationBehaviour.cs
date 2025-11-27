using UnityEngine;

public class RotationBehaviour : IBehaviour, IShipUpdatable
{
    private Rigidbody _rigidbody;
    private PhysicRotator _physicRotator;
    private float _shipRotationSpeed;
    private float _currentInput;

    public RotationBehaviour(Rigidbody rigidbody, PhysicRotator physicRotator, float shipRotationSpeed)
    {
        _rigidbody = rigidbody;
        _physicRotator = physicRotator;
        _shipRotationSpeed = shipRotationSpeed;
    }

    public void Tick(float value)
    {
        _currentInput = value;
    }

    public void Execute()
    {
        _physicRotator.Rotate(_rigidbody.transform.up, _currentInput * _shipRotationSpeed);
    }
}