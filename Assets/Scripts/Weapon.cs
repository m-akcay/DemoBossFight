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
        BOMB
    }

    [SerializeField] protected AmmoSO _ammoSo;
    [SerializeField] protected VisualEffect _vfx;
    
    public AmmoType Ammo { get; protected set; }

    protected bool _vfxPlaying = false;
    protected int _vfxStartEvent;
    protected int _vfxStopEvent;
    protected bool _canShoot = true;
    [SerializeField] protected Transform _weaponTransform;
    protected Transform _transform;

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {
        _transform = transform;
    }

    public virtual void Shoot()
    {
    }

    protected IEnumerator ShootingICD(float secs)
    {
        _canShoot = false;
        yield return new WaitForSeconds(secs);
        _canShoot = true;
    }

    public void StopShooting()
    {
        _vfx.SendEvent(_vfxStopEvent);
        _vfxPlaying = false;
    }

}
