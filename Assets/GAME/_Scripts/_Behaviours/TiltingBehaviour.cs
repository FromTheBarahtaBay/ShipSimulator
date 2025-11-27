using UnityEngine;

public class TiltingBehaviour : IShipUpdatable, IBehaviour
{
    private Rigidbody _rigidbody;
    private PhysicRotator _physicRotator;
    private float _maxBodyTilt;
    private float _currentInput;

    public TiltingBehaviour(Rigidbody rigidbody, PhysicRotator physicRotator, float maxBodyTilt)
    {
        _rigidbody = rigidbody;
        _physicRotator = physicRotator;
        _maxBodyTilt = maxBodyTilt;
    }

    public void Tick(float value)
    {
        _currentInput = value;
    }

    public void Execute()
    {
        _physicRotator.Rotate(_rigidbody.transform.forward, _currentInput * _maxBodyTilt);
    }
}