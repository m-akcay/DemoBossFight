using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artillery : Weapon
{
    private BulletSO _bulletData = null;
    [SerializeField] private GameObject _explosionSphere;

    protected override void Awake()
    {
        Ammo = AmmoType.BULLET;
    }

    protected override void Start()
    {
        base.Start();
        _bulletData = _ammoSo as BulletSO;
    }

    private void ShootWithoutIdle()
    {
        if (!_canShoot)
        {
            return;
        }


        if (_remainingAmmo >= _ammoReductionAmount && !_reloadIdle)
        {
            _remainingAmmo -= _ammoReductionAmount;
            InvokeOnAmmoChange();
            _shooting = true;

            if (_remainingAmmo > 0)
            {
                _reloading = false;
            }
            else
            {
                StartCoroutine(Reload());
            }

            StartCoroutine(ShootingICD());
        }
    }

    public override void Shoot()
    {
        ShootWithoutIdle();

        if (!_shooting)
        {
            return;
        }

        _vfx.Play();
        StartCoroutine(FiringVFX(0.05f));
        var bullet = BulletHitboxPool.Instance.Get();
        if (bullet != null)
        {
            bullet.Init(_weaponTransform, _bulletData);
        }

        _shooting = false;
    }

    private IEnumerator FiringVFX(float seconds)
    {
        _explosionSphere.SetActive(true);
        yield return new WaitForSeconds(seconds);
        _explosionSphere.SetActive(false);
    }

 
}
