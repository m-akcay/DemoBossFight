using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponSO")]
public class AmmoSO : ScriptableObject
{
    [SerializeField] protected string _name;
    [SerializeField] protected Weapon.AmmoType _ammoType;
    [SerializeField] protected GameObject _prefab;
    [SerializeField] protected float _speed;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _onHitDamage;
    

    public GameObject Prefab => _prefab;
    public Weapon.AmmoType AmmoType => _ammoType;

    public float Speed => _speed;
    public float MaxDistance => _maxDistance;
    public float OnHitDamage => _onHitDamage;
    
}
