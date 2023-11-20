using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmmoType = Weapon.AmmoType;

public class DamageSource : MonoBehaviour
{
    protected const int MOB_LAYER = 10;
    public AmmoType Type { get; protected set; }
    public float OnHitDamage { get; protected set; }
    public float Dot { get; protected set; }
    public float DotDurationSeconds { get; protected set; }
    public float DefenseReduction { get; protected set; }
    public float DefenseReductionDuration { get; protected set; }
    public bool HasDot => Dot > 0;
    public bool HasDr => DefenseReduction > 0;
    protected Transform _transform = null;

    protected virtual void Awake()
    {
        _transform = transform;
    }

    public virtual void Init(in Transform inTransform, in AmmoSO ammoSO)
    {
        Type = ammoSO.AmmoType;
        OnHitDamage = ammoSO.OnHitDamage;
        Dot = ammoSO.Dot;
        DotDurationSeconds = ammoSO.DotDurationSeconds;
        DefenseReduction = ammoSO.DefenseReduction;
        DefenseReductionDuration = ammoSO.DefenseReductionDuration;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == MOB_LAYER)
        {
            other.GetComponent<DamageableObject>().ApplyDamage(this);
        }
    }
}
