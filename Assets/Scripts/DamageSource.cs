using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmmoType = Weapon.AmmoType;

public class DamageSource : MonoBehaviour
{
    private const int MOB_LAYER = 10;
    public AmmoType Type { get; protected set; }
    public float OnHitDamage { get; protected set; }
    public float Dot { get; protected set; }
    public float DotDurationSeconds { get; protected set; }
    public bool HasDot => Dot > 0;
    protected Transform _transform = null;

    protected void Awake()
    {
        _transform = transform;
    }

    public virtual void Init(in Transform inTransform, in AmmoSO ammoSO)
    {
        Type = ammoSO.AmmoType;
        OnHitDamage = ammoSO.OnHitDamage;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == MOB_LAYER)
        {
            other.GetComponent<Damageable>().ApplyDamage(this);
        }
    }
}
