using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using AmmoType = Weapon.AmmoType;

public class MobWeaknessManager : MonoBehaviour
{
    private const float MIN_WEAKNESS = 0.2f;
    private const float MAX_WEAKNESS = 1.0f;

    public event Action<float> OnWeaknessChanged;

    [SerializeField] private List<Damageable.Weakness> _weaknessMapDebug;

    private void Start()
    {
        _weaknessMapDebug = new();
    }

    public void UpdateWeakness(in Damageable damageable, in AmmoType ammoType)
    {
        var mapping = damageable.WeaknessMap;
        UpdateMapping(mapping, ammoType);
        float flameWeaknessRemapped = mapping[AmmoType.FLAME]
            .Remap(MIN_WEAKNESS, MAX_WEAKNESS, 0.0f, 1.0f);
        OnWeaknessChanged?.Invoke(flameWeaknessRemapped);
    }

    private void UpdateMapping(Dictionary<AmmoType, float> weaknessMap, in AmmoType ammoType)
    {
        if (ammoType == AmmoType.BULLET)
        {
            return;
        }

        _weaknessMapDebug.Clear();

        float currentWeakness = weaknessMap[ammoType];
        currentWeakness = Mathf.Clamp(currentWeakness - 0.005f, MIN_WEAKNESS, MAX_WEAKNESS);
        weaknessMap[ammoType] = currentWeakness;

        //debug
        var ammoWeakness = new Damageable.Weakness();
        ammoWeakness.weaponType = ammoType;
        ammoWeakness.multiplier = currentWeakness;
        _weaknessMapDebug.Add(ammoWeakness);
        //


        var keys = weaknessMap.GetKeysAsList();

        foreach (var key in keys)
        {
            if (key == ammoType)
            {
                continue;
            }

            var value = weaknessMap[key];
            if (value <= MAX_WEAKNESS)
            {
                value += 0.005f;
            }

            weaknessMap[key] = value;

            var weakness = new Damageable.Weakness();
            weakness.weaponType = key;
            weakness.multiplier = value;
            _weaknessMapDebug.Add(weakness);
        }

    }
}
