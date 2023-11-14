using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponSO")]
public class AmmoSO : ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected Weapon.AmmoType _ammoType;
    [SerializeField] protected GameObject _prefab;
    [SerializeField] [ColorUsage(true, true)] protected Color _capsuleColor;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _totalAmmo;
    [SerializeField] protected int _ammoReductionAmount;
    [SerializeField] private float _shootingIcd;
    [SerializeField] private float _reloadIcd;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _onHitDamage;
    [SerializeField] private float _dot;
    [SerializeField] private float _dotDurationSeconds;
    [SerializeField] private float _defenseReduction;
    [SerializeField] private float _defenseReductionDuration;

    public GameObject Prefab => _prefab;
    public Weapon.AmmoType AmmoType => _ammoType;

    public Color CapsuleColor => _capsuleColor;
    public float Speed => _speed;
    public int TotalAmmo => _totalAmmo;
    public int AmmoReductionAmount => _ammoReductionAmount;
    public float ShootingIcd => _shootingIcd;
    public float ReloadIcd => _reloadIcd;
    public float MaxDistance => _maxDistance;
    public float OnHitDamage => _onHitDamage;
    public float Dot => _dot;
    public float DotDurationSeconds => _dotDurationSeconds;
    public float DefenseReduction => _defenseReduction;
    public float DefenseReductionDuration => _defenseReductionDuration;
    
}
