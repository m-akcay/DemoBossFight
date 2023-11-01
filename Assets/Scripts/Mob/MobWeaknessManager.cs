using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmmoType = Weapon.AmmoType;

public class MobWeaknessManager : MonoBehaviour
{
    private const float MIN_WEAKNESS = 0.2f;
    private const float MAX_WEAKNESS = 1.0f;
    [SerializeField] private List<Damageable.Weakness> _weaknessMapDebug;
    [SerializeField] [GradientUsage(true)] private Gradient _weaknessGradient;
    private Material _mat;

    private void Start()
    {
        _weaknessMapDebug = new();
        _mat = GetComponent<Renderer>().material;
        _mat.SetColor("_BaseColor", _weaknessGradient.colorKeys[1].color);
        _mat.SetColor("_EmissionColor", _weaknessGradient.colorKeys[1].color);
    }

    public void UpdateWeakness(in Damageable damageable, in AmmoType ammoType)
    {
        var mapping = damageable.WeaknessMap;
        UpdateMapping(mapping, ammoType);
        UpdateMaterial(mapping[AmmoType.FLAME]);
    }

    private void UpdateMapping(Dictionary<AmmoType, float> weaknessMap, in AmmoType ammoType)
    {
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

    private void UpdateMaterial(float flameWeakness)
    {
        float gradientTime = flameWeakness.Remap(MIN_WEAKNESS, MAX_WEAKNESS, 0.0f, 1.0f);
        var currentColor = _weaknessGradient.Evaluate(gradientTime);
        _mat.SetColor("_BaseColor", currentColor);
        _mat.SetColor("_EmissionColor", currentColor);
    }
}
