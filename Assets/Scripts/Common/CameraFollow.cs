using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0.125f;
    private Vector3 _targetToCamera;

    private void Start()
    {
        _transform = transform;
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _targetToCamera = _transform.position - _target.position;
    }

    void LateUpdate()
    {
        if (_target != null)
        {
            var desiredPosition = _target.position + _targetToCamera;
            var smoothedPosition = Vector3.Lerp(_transform.position, desiredPosition, _smoothSpeed);
            _transform.position = smoothedPosition;
        }
    }
}