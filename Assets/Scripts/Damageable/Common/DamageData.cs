using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AmmoType = Weapon.AmmoType;

[System.Serializable]
public class DamageData
{
    private static float DamageStartTime = 0;
    [SerializeField] private AmmoType _weaponType;
    [SerializeField] private float _directDamage;
    [SerializeField] private float _dot;
    [SerializeField] private float _totalDamage;
    [SerializeField] private float _totalDps;

    public float DirectDamage
    {
        get { return _directDamage; }
        set
        {
            _directDamage = value;
        }
    }

    public float Dot
    {
        get { return _dot; }
        set
        {
            _dot = value;
        }
    }

    public float TotalDps => _totalDps;

    public DamageData(AmmoType ammoType)
    {
        _weaponType = ammoType;
        _directDamage = _dot = _totalDamage = _totalDps = 0;
    }

    public void StartLogging(in DamageLog dlog)
    {
        DamageStartTime = Time.realtimeSinceStartup;
        dlog.StartCoroutine(UpdateDps());
    }

    private IEnumerator UpdateDps()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            _totalDamage = _directDamage + _dot;
            _totalDps = _totalDamage / (Time.realtimeSinceStartup - DamageStartTime);
        }
    }
}
