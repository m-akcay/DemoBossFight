using UnityEngine;
using Weakness = DWeakenedCube.Weakness;

[CreateAssetMenu(fileName = "DWeakenedCubeData", menuName = "ScriptableObjects/DWeakenedCubeSo")]
public class DWeakenedCubeSo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private float _totalHp;
    [SerializeField] private Weakness[] _weaknesses;

    public string Name => _name;
    public float TotalHp => _totalHp;
    public Weakness[] Weaknesses => _weaknesses;
}
