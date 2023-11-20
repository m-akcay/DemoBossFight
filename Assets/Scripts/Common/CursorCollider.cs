using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCollider : MonoBehaviour
{
    public event Action<bool> CursorOnDamageable;

    private int _damageableLayer;

    public void Init()
    {
        _damageableLayer = FindObjectOfType<DamageableObject>().gameObject.layer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == _damageableLayer)
        {
            CursorOnDamageable?.Invoke(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _damageableLayer)
        {
            CursorOnDamageable?.Invoke(false);
        }
    }
}
