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

    public override void Shoot()
    {
        base.Shoot();

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

    //protected override IEnumerator Reload()
    //{
    //    const float maxReloadTime = 1;
    //    const float stepCount = 100;
    //    const float reloadStep = maxReloadTime / stepCount;
    //    float stepSize = _reloadIcd / stepCount;
    //    float totalReloadTime = 0;

    //    while (totalReloadTime < maxReloadTime)
    //    {
    //        totalReloadTime += reloadStep;
    //        OnAmmoChange?.Invoke(Ammo, totalReloadTime);
    //        yield return new WaitForSeconds(stepSize);
    //    }

    //    _remainingAmmo++;
    //    _reloading = false;
    //}

    //private IEnumerator ReloadVisual()
    //{
    //    const float maxReloadTime = 1;
    //    const float stepCount = 100;
    //    const float reloadStep = maxReloadTime / stepCount;
    //    float stepSize = _reloadIcd / stepCount;
    //    float totalReloadTime = 0;

    //    while (_reloading)
    //    {
    //        totalReloadTime += reloadStep;
    //        OnAmmoChange?.Invoke(Ammo, totalReloadTime);
    //        yield return new WaitForSeconds(stepSize);
    //    }
    //}
}
