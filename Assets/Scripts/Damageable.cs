using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmmoType = Weapon.AmmoType;

public class Damageable : MonoBehaviour
{
    [System.Serializable]
    public struct Weakness
    {
        public AmmoType weaponType;
        public float multiplier;
    }

    [SerializeField] protected float _totalHp;
    [SerializeField] protected float _remainingHp;
    [SerializeField] protected Weakness[] _weaknesses;
    [SerializeField] protected float _damageReduction = 0;
    [SerializeField] protected float _damageReductionMultiplier = 1;
    protected Dictionary<AmmoType, float> _weaknessMap;
    protected Dictionary<AmmoType, float> _weaknessDotRemainingTimes;

    public Dictionary<AmmoType, float> WeaknessMap => _weaknessMap;

    public virtual void Init(in MobSO SO)
    {
        _weaknessMap = new();
        _weaknessDotRemainingTimes = new();
        foreach (var weakness in SO.Weaknesses)
        {
            _weaknessMap.Add(weakness.weaponType, weakness.multiplier);
        }

        _totalHp = SO.TotalHp;
        _remainingHp = _totalHp;
    }

    public virtual void ApplyDamage(in DamageSource damageSource)
    {
        float weaknessMultiplier = _weaknessMap[damageSource.Type];
        float totalDamage = CalculateDirectDamage(damageSource.OnHitDamage, weaknessMultiplier);
        _remainingHp -= totalDamage;
        if (damageSource.HasDot)
        {
            ApplyDot(damageSource.Dot, damageSource.DotDurationSeconds, damageSource.Type);
        }
    }

    protected float CalculateDirectDamage(float onHitDamage, float weaknessMultiplier)
    {
        float damageAfterWeakness = onHitDamage * weaknessMultiplier;
        float damageTaken = damageAfterWeakness * _damageReductionMultiplier;
        return damageTaken;
    }

    protected void ApplyDot(float amount, float duration, AmmoType ammoType)
    {
        if (_weaknessDotRemainingTimes.ContainsKey(ammoType))
        {
            _weaknessDotRemainingTimes[ammoType] = duration;
            return;
        }

        _weaknessDotRemainingTimes.Add(ammoType, duration);
        StartCoroutine(DamageOverTime(amount, ammoType));
    }

    protected IEnumerator DamageOverTime(float amount, AmmoType ammoType)
    {
        // TryGetValue saves 1 read to dict
        while (_weaknessDotRemainingTimes.TryGetValue(ammoType, out float remainingTime) && remainingTime > 0.0f)
        {
            float damage = amount * _weaknessMap[ammoType] * _damageReductionMultiplier;
            _remainingHp -= damage;
            _weaknessDotRemainingTimes[ammoType] = remainingTime - 1;
            yield return new WaitForSeconds(1.0f);
        }
        _weaknessDotRemainingTimes.Remove(ammoType);
    }

    public void UpdateDamageReduction(float value)
    {
        _damageReduction = value;
        _damageReductionMultiplier = 1 - value;
    }
}
