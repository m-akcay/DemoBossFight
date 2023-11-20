using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponType = Weapon.AmmoType;

public class DWeakenedCube : DamageableObject
{
    public event Action<WeaponType> OnHit;

    private DWeakenedCubeWeaknessManager _weaknessManager;
    private void Start()
    {
        _weaknessManager = GetComponent<DWeakenedCubeWeaknessManager>();
    }

    public override void ApplyDamage(in DamageSource damageSource)
    {
        base.ApplyDamage(damageSource);
        OnHit?.Invoke(damageSource.Type);
        _weaknessManager.UpdateWeakness(this, damageSource.Type);
    }
}
