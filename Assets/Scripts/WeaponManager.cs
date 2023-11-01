using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using AmmoType = Weapon.AmmoType;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private AmmoSO[] _ammoScriptableObjects;

    private Weapon _selectedWeapon;
    private Dictionary<AmmoType, Weapon> _weaponMapping;

    private void Start()
    {
        _weaponMapping = new();
        var allWeapons = GetComponents<Weapon>();
        _selectedWeapon = GetComponent<RedFlamethrower>();

        var ammoList = _ammoScriptableObjects.ToList();

        Debug.Log(allWeapons.Length);

        foreach (var ammo in _ammoScriptableObjects)
        {
            Debug.Log(ammo.AmmoType);
            var weapon = allWeapons.First(w => w.Ammo == ammo.AmmoType);
            _weaponMapping.TryAdd(ammo.AmmoType, weapon);
        }
    }

    public void SelectWeapon(AmmoType weaponType)
    {
        if (weaponType == _selectedWeapon.Ammo)
            return;

        _selectedWeapon.StopShooting();
        _selectedWeapon.enabled = false;

        if (_weaponMapping.TryGetValue(weaponType, out var weapon))
        {
            _selectedWeapon = weapon;
        }

        _selectedWeapon.enabled = true;
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
