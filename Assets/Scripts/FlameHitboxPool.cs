using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameHitboxPool : MonoBehaviour
{
    public static FlameHitboxPool Instance { get; private set; }
    [SerializeField] private FlameSO _flameSO;
    [SerializeField] private int _poolSize;

    private ObjectPool<FlameHitBox> _pool;

    private void Awake()
    {
        Instance = this;
        _pool = new ObjectPool<FlameHitBox>(_flameSO.Prefab, _poolSize);
    }

    public FlameHitBox Get() => _pool.Get();

    public void Return(FlameHitBox obj) => _pool.Return(obj);
}
