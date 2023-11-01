using UnityEngine;

[CreateAssetMenu(fileName = "FlameData", menuName = "ScriptableObjects/FlameSO")]
public class FlameSO : AmmoSO
{
    [SerializeField] private float _dot;
    [SerializeField] private float _dotLengthSeconds;
    [SerializeField] private string _vfxStartEventName;
    [SerializeField] private string _vfxStopEventName;

    public float Dot => _dot;
    public float DotLengthSeconds => _dotLengthSeconds;
    public string VfxStartEventName => _vfxStartEventName;
    public string VfxStopEventName => _vfxStopEventName;
}
