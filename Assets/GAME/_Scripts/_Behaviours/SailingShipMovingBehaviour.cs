using UnityEngine;

public class SailingShipMovingBehaviour : IBehaviour
{
    private Rigidbody _rigidbody;
    private float _speedForce;

    private Transform _ship;
    private Transform _sail;
    private Transform _wind;

    public SailingShipMovingBehaviour(Rigidbody rigidbody, float speedForce, Transform ship, Transform sail, Transform wind)
    {
        _rigidbody = rigidbody;
        _speedForce = speedForce;

        _ship = ship;
        _sail = sail;
        _wind = wind;
    }

    public void Execute()
    {
        _rigidbody.AddForce(_rigidbody.transform.forward * GetMovingForce(_speedForce), ForceMode.Acceleration);
    }

    private float GetMovingForce(float maxSpeedForce)
    {
        float value = maxSpeedForce *
              Vector3.Dot(_ship.forward, _sail.forward) *
              Vector3.Dot(_wind.forward, _sail.forward);

        return Mathf.Max(value, 0f);
    }
}