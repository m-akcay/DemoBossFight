using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobController : MonoBehaviour
{
    [SerializeField] private MobSO _mobSo;
    private MobHealth _mobHealth;

    void Start()
    {
        _mobHealth = GetComponent<MobHealth>();
        _mobHealth.Init(_mobSo);
    }
    
}
