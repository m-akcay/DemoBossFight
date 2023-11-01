using UnityEngine;
using Weakness = MobHealth.Weakness;

[CreateAssetMenu(fileName = "FlameData", menuName = "ScriptableObjects/MobSO")]
public class MobSO : ScriptableObject
{
    [SerializeField] private float _totalHp;
    [SerializeField] private Weakness[] _weaknesses;

    public float TotalHp => _totalHp;
    public Weakness[] Weaknesses => _weaknesses;
}
