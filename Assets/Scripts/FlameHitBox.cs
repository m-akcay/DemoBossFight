using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmmoType = Weapon.AmmoType;

public class FlameHitBox : DamageSource
{
    [SerializeField] private Vector3 _displacement;
    [SerializeField] private float _displacementMagnitude;
    [SerializeField] private float _totalDisplacement;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _scaleFactor;

    public void Init(in Transform inTransform, in FlameSO flameSO)
    {
        base.Init(inTransform, flameSO);

        var fwd = inTransform.forward;
        var pos = inTransform.position;

        float speed = flameSO.Speed;

        _totalDisplacement = 0;
        _scaleFactor = speed * 0.01f;
        _maxDistance = flameSO.MaxDistance;
        _displacement = speed * Time.deltaTime * fwd;
        _displacementMagnitude = _displacement.magnitude;
        Dot = flameSO.Dot;
        DotDurationSeconds = flameSO.DotLengthSeconds;

        _transform.localScale = Vector3.one;
        _transform.position = pos;
        _transform.LookAt(pos + fwd);
    }

    private void Update()
    {
        if (_totalDisplacement > _maxDistance)
        {
            FlameHitboxPool.Instance.Return(this);
        }

        _totalDisplacement += _displacementMagnitude;
        _transform.position += _displacement;

        float scale = _totalDisplacement * _scaleFactor;
        _transform.localScale = new Vector3(scale, scale, scale);
    }
}
