using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmmoType = Weapon.AmmoType;

public class WeaponVisualsManager : MonoBehaviour
{
    [SerializeField] private GameObject _gun;
    private Material _gunMat;
    private Dictionary<AmmoType, Material> _capsuleMats;

    public void Init(in WeaponManager weaponManager)
    {
        _gunMat = _gun.GetComponent<MeshRenderer>().material;
        _capsuleMats = new();
        var weapons = weaponManager.AllWeapons;
        foreach (var wep in weapons)
        {
            var mat = wep.Capsule.GetComponent<MeshRenderer>().material;
            _capsuleMats.TryAdd(wep.Ammo, mat);
            mat.SetColor("_FillColor", wep.CapsuleColor);
            mat.SetFloat("_FillAmount", 1.0f);
            wep.OnAmmoChange += UpdateWeaponCapsule;
        }
    }

    private void OnEnable()
    {
        var weapons = GetComponent<WeaponManager>().AllWeapons;

        if (_capsuleMats != null)
        {
            foreach (var wep in weapons)
            {
                wep.enabled = true;
                wep.OnAmmoChange += UpdateWeaponCapsule;
                wep.enabled = false;
            }
        }
    }

    private void OnDisable()
    {
        var weapons = GetComponent<WeaponManager>().AllWeapons;

        foreach (var wep in weapons)
        {
            wep.enabled = true;
            wep.OnAmmoChange -= UpdateWeaponCapsule;
            wep.enabled = false;
        }
    }

    private void UpdateWeaponCapsule(AmmoType ammo, float remainingAmmoRatio)
    {
        _capsuleMats[ammo].SetFloat("_FillAmount", remainingAmmoRatio);
    }

    private void OnDestroy()
    {
        foreach (var kvp in _capsuleMats)
        {
            Destroy(kvp.Value);
        }
    }
}
