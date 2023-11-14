using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHitboxPool : MonoBehaviour
{
    public static BulletHitboxPool Instance { get; private set; }
    [SerializeField] private BulletSO _bombSo;
    [SerializeField] private int _poolSize;

    private ObjectPool<BulletHitbox> _pool;

    private void Awake()
    {
        Instance = this;
        _pool = new ObjectPool<BulletHitbox>(_bombSo.Prefab, _poolSize);
    }

    public BulletHitbox Get() => _pool.Get();

    public void Return(BulletHitbox obj) => _pool.Return(obj);
}
