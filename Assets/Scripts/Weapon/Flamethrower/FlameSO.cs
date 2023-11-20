using UnityEngine;

[CreateAssetMenu(fileName = "FlameData", menuName = "ScriptableObjects/FlameSO")]
public class FlameSO : AmmoSO
{
    [SerializeField] private string _vfxStartEventName;
    [SerializeField] private string _vfxStopEventName;

    public string VfxStartEventName => _vfxStartEventName;
    public string VfxStopEventName => _vfxStopEventName;
}
