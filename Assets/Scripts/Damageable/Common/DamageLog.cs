using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmmoType = Weapon.AmmoType;

public class DamageLog : MonoBehaviour
{
    private Dictionary<AmmoType, DamageData> _damageByWeaponMapping;
    [SerializeField] private List<DamageData> _damageList;
    private bool _logStarted = false;

    public IReadOnlyDictionary<AmmoType, DamageData> DamageByWeapons => _damageByWeaponMapping;

    public void Init(in IReadOnlyDictionary<AmmoType, float> weaknessMap)
    {
        _damageList = new();
        _damageByWeaponMapping = new();
        foreach (var kvp in weaknessMap)
        {
            _damageByWeaponMapping.Add(kvp.Key, new DamageData(kvp.Key));
        }

    }

    public void Log(AmmoType ammoType, float directDamage)
    {
        if (!_logStarted)
        {
            StartLog();
        }
        _damageByWeaponMapping[ammoType].DirectDamage += directDamage;
    }

    public void LogDot(AmmoType ammoType, float dot)
    {
        _damageByWeaponMapping[ammoType].Dot += dot;
    }

    private void StartLog()
    {
        _logStarted = true;
        foreach (var kvp in _damageByWeaponMapping)
        {
            kvp.Value.StartLogging(this);
        }

        StartCoroutine(RefreshLog());
    }

    private IEnumerator RefreshLog()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);

            _damageList.Clear();
            foreach (var kvp in _damageByWeaponMapping)
            {
                var data = kvp.Value;
                _damageList.Add(data);
            }
        }
    }
}
