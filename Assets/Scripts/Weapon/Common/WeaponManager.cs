using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using AmmoType = Weapon.AmmoType;
using System;

public class WeaponManager : MonoBehaviour
{
    public event Action<Color> OnWeaponColorChange;

    [SerializeField] private AmmoSO[] _ammoScriptableObjects;

    [SerializeField]private Weapon[] _allWeapons;
    private Weapon _selectedWeapon;
    private Dictionary<AmmoType, Weapon> _weaponMapping;

    public Weapon[] AllWeapons => _allWeapons;

    private void Start()
    {
        _allWeapons = GetComponents<Weapon>();

        _weaponMapping = new();

        var ammoList = _ammoScriptableObjects.ToList();

        Debug.Log(_allWeapons.Length);

        foreach (var ammo in _ammoScriptableObjects)
        {
            var weapon = _allWeapons.First(w => w.Ammo == ammo.AmmoType);
            _weaponMapping.TryAdd(ammo.AmmoType, weapon);
        }

        _selectedWeapon = GetComponent<Flamethrower>();
        OnWeaponColorChange?.Invoke(_selectedWeapon.CapsuleColor);
        
        GetComponent<WeaponVisualsManager>().Init(this);
    }

    public void SelectWeapon(AmmoType weaponType)
    {
        if (weaponType == _selectedWeapon.Ammo)
            return;

        _selectedWeapon.StopShooting();
        _selectedWeapon = _weaponMapping[weaponType];
        OnWeaponColorChange?.Invoke(_selectedWeapon.CapsuleColor);
    }

    public void Shoot()
    {
        _selectedWeapon.Shoot();
    }

    public void StopShooting()
    {
        _selectedWeapon.StopShooting();
    }
}
