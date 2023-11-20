using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmmoType = Weapon.AmmoType;

public class DamageableDefenseReductionHandler : MonoBehaviour
{
    private class DefenseReductionData
    {
        public float value;
        public float duration;
        public DefenseReductionData(float value, float duration)
        {
            this.value = value;
            this.duration = duration;
        }
    }

    public event Action<float> OnDefenseReductionChanged;

    [SerializeField] private float _defenseReduction;
    [SerializeField] private float _defenseReductionMultiplier;

    private Dictionary<AmmoType, DefenseReductionData> _defenseReductionMapping;

    public float DefenseReductionMultiplier => _defenseReductionMultiplier;

    private void Awake()
    {
        _defenseReductionMapping = new();
        _defenseReduction = 0;
        _defenseReductionMultiplier = 1;
    }

    public void UpdateDefenseReduction(in DamageSource damageSource)
    {
        var ammoType = damageSource.Type;
        if (_defenseReductionMapping.ContainsKey(ammoType))
        {
            _defenseReductionMapping[ammoType].duration = damageSource.DefenseReductionDuration;
            return;
        }

        float dr = damageSource.DefenseReduction;
        _defenseReductionMapping.Add(ammoType, new DefenseReductionData(dr, damageSource.DefenseReductionDuration));
        
        ApplyDefenseReduction(dr);
        StartCoroutine(DefenseReductionCoroutine(ammoType));
    }

    private void ApplyDefenseReduction(float value)
    {
        _defenseReduction += value;
        _defenseReductionMultiplier = 1 + _defenseReduction;
        OnDefenseReductionChanged?.Invoke(_defenseReduction);
    }

    private IEnumerator DefenseReductionCoroutine(AmmoType ammoType)
    {
        DefenseReductionData defenseReductionData;
        while (_defenseReductionMapping.TryGetValue(ammoType, out defenseReductionData) && defenseReductionData.duration > 0.0f)
        {
            yield return new WaitForSeconds(0.1f);
            defenseReductionData.duration -= 0.1f;
        }

        ApplyDefenseReduction(-defenseReductionData.value);
        _defenseReductionMapping.Remove(ammoType);
    }
}
