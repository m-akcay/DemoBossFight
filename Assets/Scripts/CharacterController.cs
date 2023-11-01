using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private const float SPEED_MULTIPLIER = 10;
    [SerializeField][Range(1, 10)] private float _speed;
    private Vector3 _prevPos;
    private Transform _transform;
    private Weapon _weapon;

    private void Start()
    {
        _transform = transform;
        _prevPos = _transform.position;
        _weapon = GetComponentInChildren<Weapon>();
    }

    private void FixedUpdate()
    {
        var velocity = _transform.position - _prevPos;
        velocity /= Time.fixedDeltaTime;
        _prevPos = _transform.position;
    }

    public void Move(in Vector3 movementVector)
    {
        _transform.position += movementVector * _speed * SPEED_MULTIPLIER * Time.deltaTime;
    }

    public void LookAt(in Vector3 point)
    {
        var targetPoint = point;
        targetPoint.y = _transform.position.y;
        _transform.LookAt(targetPoint);
    }

    public void Shoot()
    {
        _weapon.Shoot();
    }

    public void StopShooting()
    {
        _weapon.StopShooting();
    }
}
