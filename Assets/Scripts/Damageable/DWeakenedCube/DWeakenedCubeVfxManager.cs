using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using WeaponType = Weapon.AmmoType;

public class DWeakenedCubeVfxManager : MonoBehaviour
{
    [SerializeField] [GradientUsage(true)] private Gradient _weaknessGradient;
    [SerializeField] private VisualEffect _defenseReductionVfx;
    [SerializeField] private VisualEffect _bulletHitVfx;
    private Material _mat;
    private bool _defenseReductionIsActive = false;

    private void OnEnable()
    {
        GetComponent<DWeakenedCubeWeaknessManager>().OnWeaknessChanged += UpdateVisuals;
        GetComponent<DamageableDefenseReductionHandler>().OnDefenseReductionChanged += UpdateDefenseReductionVfx;
        GetComponent<DWeakenedCube>().OnHit += WeaponHitVfx;
    }

    private void OnDisable()
    {
        GetComponent<DWeakenedCubeWeaknessManager>().OnWeaknessChanged -= UpdateVisuals;
        GetComponent<DamageableDefenseReductionHandler>().OnDefenseReductionChanged -= UpdateDefenseReductionVfx;
        GetComponent<DWeakenedCube>().OnHit -= WeaponHitVfx;
    }

    private void Start()
    {
        _mat = GetComponent<Renderer>().material;
        _mat.SetColor("_BaseColor", _weaknessGradient.colorKeys[1].color);
        _mat.SetColor("_EmissionColor", _weaknessGradient.colorKeys[1].color);
    }
    private void UpdateVisuals(float gradientTime)
    {
        var currentColor = _weaknessGradient.Evaluate(gradientTime);
        _mat.SetColor("_BaseColor", currentColor);
        _mat.SetColor("_EmissionColor", currentColor);
    }

    private void UpdateDefenseReductionVfx(float defenseReduction)
    {
        if (defenseReduction > 0 && !_defenseReductionIsActive)
        {
            _defenseReductionVfx.SendEvent("Start");
            _defenseReductionIsActive = true;
        }
        else if (_defenseReductionIsActive)
        {
            _defenseReductionIsActive = false;
            _defenseReductionVfx.SendEvent("Stop");
        }    
    }

    private void WeaponHitVfx(WeaponType weaponType)
    {
        if (weaponType == WeaponType.BULLET)
        {
            _bulletHitVfx.SendEvent("Start");
        }
    }

  
}
