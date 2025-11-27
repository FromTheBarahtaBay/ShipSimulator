using UnityEngine;

public class AlignWithNormalBehaviour : IHittable, IBehaviour
{
    private RaycastHit _currentHit;
    private PhysicRotator _physicRotator;
    private Rigidbody _rigidbody;
    private float _rollSpeed;

    public AlignWithNormalBehaviour(Rigidbody rigidbody, PhysicRotator physicRotator, float rollSpeed)
    {
        _rigidbody = rigidbody;
        _rollSpeed = rollSpeed;
        _physicRotator = physicRotator;
    }

    public void Execute()
    {
        _physicRotator.Rotate(Vector3.Cross(_rigidbody.transform.up, _currentHit.normal), _rollSpeed);
    }

    public void Recive(RaycastHit hit)
    {
        _currentHit = hit;
    }
}