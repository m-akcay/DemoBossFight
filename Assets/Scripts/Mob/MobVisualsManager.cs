using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MobVisualsManager : MonoBehaviour
{
    [SerializeField] [GradientUsage(true)] private Gradient _weaknessGradient;
    [SerializeField] private VisualEffect _defenseReductionVfx;
    private Material _mat;
    private bool _defenseReductionIsActive = false;

    private void OnEnable()
    {
        GetComponent<MobWeaknessManager>().OnWeaknessChanged += UpdateVisuals;
        GetComponent<MobDefenseReductionHandler>().OnDefenseReductionChanged += UpdateDefenseReductionVfx;
    }

    private void OnDisable()
    {
        GetComponent<MobWeaknessManager>().OnWeaknessChanged -= UpdateVisuals;
        GetComponent<MobDefenseReductionHandler>().OnDefenseReductionChanged -= UpdateDefenseReductionVfx;
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
}
