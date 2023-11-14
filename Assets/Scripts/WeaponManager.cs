using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using AmmoType = Weapon.AmmoType;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private AmmoSO[] _ammoScriptableObjects;

    [SerializeField]private Weapon[] _allWeapons;
    private Weapon _selectedWeapon;
    private Dictionary<AmmoType, Weapon> _weaponMapping;

    public Weapon[] AllWeapons => _allWeapons;

    private void Start()
    {
        _allWeapons = GetComponents<Weapon>();

        _weaponMapping = new();
        _selectedWeapon = GetComponent<RedFlamethrower>();

        var ammoList = _ammoScriptableObjects.ToList();

        Debug.Log(_allWeapons.Length);

        foreach (var ammo in _ammoScriptableObjects)
        {
            var weapon = _allWeapons.First(w => w.Ammo == ammo.AmmoType);
            _weaponMapping.TryAdd(ammo.AmmoType, weapon);
        }

        GetComponent<WeaponVisualsManager>().Init(this);

        // keep first weapon enabled
        //for (int i = 1; i < _allWeapons.Length; i++)
        //{
        //    _allWeapons[i].enabled = false;
        //}
    }

    public void SelectWeapon(AmmoType weaponType)
    {
        if (weaponType == _selectedWeapon.Ammo)
            return;

        _selectedWeapon.StopShooting();
        //_selectedWeapon.enabled = false;

        if (_weaponMapping.TryGetValue(weaponType, out var weapon))
        {
            _selectedWeapon = weapon;
        }

        //_selectedWeapon.enabled = true;
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
