using UnityEngine;

public class FloatingBehaviour : IBehaviour, IHittable
{
    private Rigidbody _rigidbody;
    private RaycastHit _currentHit;

    private float _floatForce;

    private float _offsetAboveWater;
    private float _long;
    private float _offsetX;
    private float _amplitude;
    private float _offsetY;

    private float _timer;

    public FloatingBehaviour(Rigidbody rigidbody, float floatForce, float offsetAboveWater, float width, float offsetX, float amplitude, float offsetY)
    {
        _rigidbody = rigidbody;
        _floatForce = floatForce;
        _offsetAboveWater = offsetAboveWater;
        _long = width;
        _offsetX = offsetX;
        _amplitude = amplitude;
        _offsetY = offsetY;
    }

    public void Execute()
    {
        Floating(_currentHit);
    }

    public void Recive(RaycastHit hit)
    {
        _currentHit = hit;
    }

    private void Floating(RaycastHit hit)
    {
        _timer += Time.deltaTime;

        Vector3 direction = (hit.point + Vector3.up * _offsetAboveWater) - _rigidbody.position;
        Vector3 floating = (Vector3.up * (Mathf.Sin(_timer * _long + _offsetX) * _amplitude + _offsetY));

        _rigidbody.velocity = direction * _floatForce + floating;
    }
}