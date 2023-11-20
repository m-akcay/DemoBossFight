using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostThrower : Weapon
{
    private FlameSO _flameData = null;
    protected override void Awake()
    {
        Ammo = AmmoType.FROST;
    }

    protected override void Start()
    {
        base.Start();
        _flameData = _ammoSo as FlameSO;
        _vfxStartEvent = Shader.PropertyToID(_flameData.VfxStartEventName);
        _vfxStopEvent = Shader.PropertyToID(_flameData.VfxStopEventName);
    }

    public override void Shoot()
    {
        base.Shoot();

        if (!_shooting)
        {
            return;
        }

        var flame = FlameHitboxPool.Instance.Get();
        if (flame != null)
        {
            flame.Init(_weaponTransform, _flameData);
        }

        _shooting = false;

        if (!_vfxPlaying)
        {
            _vfx.Play();
            _vfx.SendEvent(_vfxStartEvent);
            _vfxPlaying = true;
        }

    }
}
