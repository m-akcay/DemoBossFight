using UnityEngine;

[CreateAssetMenu(fileName = "BombData", menuName = "ScriptableObjects/BombSO")]
public class BulletSO : AmmoSO
{
    [SerializeField] private float _size;
    [SerializeField] private float _firingAngle;
    public float Size => _size;
    public float FiringAngle => _firingAngle;
}
