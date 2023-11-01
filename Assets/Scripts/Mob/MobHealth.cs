using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponType = Weapon.AmmoType;

public class MobHealth : Damageable
{
    private MobWeaknessManager _weaknessManager;
    private void Start()
    {
        _weaknessManager = GetComponent<MobWeaknessManager>();
    }

    public override void ApplyDamage(in DamageSource damageSource)
    {
        base.ApplyDamage(damageSource);
        _weaknessManager.UpdateWeakness(this, damageSource.Type);
    }
}
