using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour
{
    public enum AmmoType
    {
        FLAME,
        FROST,
        BULLET
    }

    public virtual event Action<AmmoType, float> OnAmmoChange;

    [SerializeField] protected AmmoSO _ammoSo;
    [SerializeField] protected VisualEffect _vfx;
    [SerializeField] private GameObject _capsule;
    // TODO --> add capsule color getter
    public AmmoType Ammo { get; protected set; }
    public GameObject Capsule => _capsule;
    public Color CapsuleColor => _ammoSo.CapsuleColor;

    protected bool _vfxPlaying = false;
    protected int _vfxStartEvent;
    protected int _vfxStopEvent;
    protected bool _canShoot = true;
    protected bool _shooting = false;
    [SerializeField] protected Transform _weaponTransform;
    protected Transform _transform;
    [SerializeField] protected int _totalAmmo;
    [SerializeField] protected int _remainingAmmo;
    [SerializeField] protected float _shootingIcd;
    [SerializeField] protected float _reloadIcd;
    [SerializeField] protected int _ammoReductionAmount;
    [SerializeField] protected bool _reloading;
    [SerializeField] protected bool _reloadIdle = false;

    protected virtual float RemainingAmmoRatio => (float)_remainingAmmo / _totalAmmo;


    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        _transform = transform;

        _totalAmmo = _ammoSo.TotalAmmo;
        _shootingIcd = _ammoSo.ShootingIcd;
        _reloadIcd = _ammoSo.ReloadIcd;
        _ammoReductionAmount = _ammoSo.AmmoReductionAmount;

        _remainingAmmo = _totalAmmo;
    }

    public virtual void Shoot()
    {
        if (!_canShoot)
        {
            return;
        }


        if (_remainingAmmo >= _ammoReductionAmount && !_reloadIdle)
        {
            _remainingAmmo -= _ammoReductionAmount;
            OnAmmoChange?.Invoke(Ammo, RemainingAmmoRatio);
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
        else if (!_reloadIdle)
        {
            StartCoroutine(ReloadIdle());
            StopShooting();
        }
    }

    protected IEnumerator ShootingICD()
    {
        _canShoot = false;
        yield return new WaitForSeconds(_shootingIcd);
        _canShoot = true;
    }

    protected IEnumerator ReloadIdle()
    {
        _reloadIdle = true;
        yield return new WaitForSeconds(1.0f);
        _reloadIdle = false;
    }

    public void StopShooting()
    {
        if (_reloading)
        {
            return;
        }

        _vfx.SendEvent(_vfxStopEvent);
        _vfxPlaying = false;
        if (_remainingAmmo < _totalAmmo)
        {
            _reloading = true;
            StartCoroutine(Reload());
        }
    }


    protected virtual IEnumerator Reload()
    {
        while (_reloading)
        {
            yield return new WaitForSeconds(_reloadIcd);
            _remainingAmmo++;
            OnAmmoChange?.Invoke(Ammo, RemainingAmmoRatio);
            if (_remainingAmmo == _totalAmmo)
            {
                break;
            }
        }

        _reloading = false;
    }

    // call from child class
    protected void InvokeOnAmmoChange()
    {
        OnAmmoChange?.Invoke(Ammo, RemainingAmmoRatio);
    }
}
