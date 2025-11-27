using UnityEngine;

public class PhysicRotator
{
    private Rigidbody _rigidbody;

    public PhysicRotator(Rigidbody rigitbody)
    {
        _rigidbody = rigitbody;
    }

    public void Rotate(Vector3 direction, float value)
    {
        _rigidbody.AddTorque(direction * value, ForceMode.Acceleration);
    }
}