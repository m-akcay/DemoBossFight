using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmmoType = Weapon.AmmoType;

public class DamageableObject : MonoBehaviour
{
    [Serializable]
    public struct Weakness
    {
        public AmmoType weaponType;
        public float multiplier;
    }

    public event Action<DamageableUiData> OnHpChange;

    [SerializeField] private float _totalHp;
    [SerializeField] private float _remainingHp;
    [SerializeField] protected Weakness[] _weaknesses;
    protected Dictionary<AmmoType, float> _weaknessMap;
    protected Dictionary<AmmoType, float> _weapopnDotRemainingTimes;
    protected DamageableDefenseReductionHandler _defenseReductionHandler;
    [SerializeField] private DamageLog _damageLog;

    public Dictionary<AmmoType, float> WeaknessMap => _weaknessMap;
    protected bool isAlive => _remainingHp > 0;
    protected float RemainingHp
    { 
        get { return _remainingHp; }
        set
        {

            _remainingHp = value > 0 ? value : 0;
            _uiContainer.CurrentHp = _remainingHp;
            OnHpChange?.Invoke(_uiContainer);
        }
    }
    
    protected DamageableUiData _uiContainer;

    public DamageLog DamageInfo => _damageLog;

    public virtual void Init(in DWeakenedCubeSo SO)
    {
        _weaknessMap = new();
        _weapopnDotRemainingTimes = new();
        
        foreach (var weakness in SO.Weaknesses)
        {
            _weaknessMap.Add(weakness.weaponType, weakness.multiplier);
        }

        _totalHp = SO.TotalHp;
        _remainingHp = _totalHp;
        _defenseReductionHandler = GetComponent<DamageableDefenseReductionHandler>();
        _uiContainer = new DamageableUiData(SO.Name, _totalHp, _remainingHp);
        _damageLog = GetComponent<DamageLog>();
        _damageLog.Init(_weaknessMap);

        GameManager.Instance.AddDamageable(this);
    }

    public virtual void ApplyDamage(in DamageSource damageSource)
    {
        if (!isAlive)
        {
            return;
        }

        var ammoType = damageSource.Type;
        float weaknessMultiplier = _weaknessMap[ammoType];
        float totalDamage = CalculateDirectDamage(damageSource.OnHitDamage, weaknessMultiplier);
        RemainingHp -= totalDamage;
        _damageLog.Log(ammoType, totalDamage);
        if (damageSource.HasDot)
        {
            ApplyDot(damageSource.Dot, damageSource.DotDurationSeconds, ammoType);
        }

        if (damageSource.HasDr)
        {
            _defenseReductionHandler.UpdateDefenseReduction(damageSource);
        }
    }

    protected float CalculateDirectDamage(float onHitDamage, float weaknessMultiplier)
    {
        float damageAfterWeakness = onHitDamage * weaknessMultiplier;
        float damageTaken = damageAfterWeakness * _defenseReductionHandler.DefenseReductionMultiplier;
        return damageTaken;
    }

    protected void ApplyDot(float amount, float duration, AmmoType ammoType)
    {
        if (_weapopnDotRemainingTimes.ContainsKey(ammoType))
        {
            _weapopnDotRemainingTimes[ammoType] = duration;
            return;
        }

        _weapopnDotRemainingTimes.Add(ammoType, duration);
        StartCoroutine(DamageOverTime(amount, ammoType));
    }

    protected IEnumerator DamageOverTime(float amount, AmmoType ammoType)
    {
        // TryGetValue saves 1 read to dict
        while (_weapopnDotRemainingTimes.TryGetValue(ammoType, out float remainingTime) && remainingTime > 0.0f)
        {
            if (!isAlive)
            {
                break;
            }
            float damage = amount * _weaknessMap[ammoType] * _defenseReductionHandler.DefenseReductionMultiplier;
            RemainingHp -= damage;
            _weapopnDotRemainingTimes[ammoType] = remainingTime - 1;
            _damageLog.LogDot(ammoType, damage);
            yield return new WaitForSeconds(1.0f);
        }
        _weapopnDotRemainingTimes.Remove(ammoType);
    }
  
}
