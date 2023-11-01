using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainStaticFlames : MonoBehaviour
{
    public static event Action<Vector3[]> FlamePositionUpdater;
    [SerializeField] private Transform[] _staticFlames;
    [SerializeField] private Vector3[] _staticFlamePositions;

    void Start()
    {
        _staticFlames = GetComponentsInChildren<Transform>().Skip(1).ToArray();
        _staticFlamePositions = new Vector3[_staticFlames.Length];
    }

    private void Update()
    {
        for (int i = 0; i < _staticFlames.Length; i++)
        {
            _staticFlamePositions[i] = _staticFlames[i].position;
        }

        FlamePositionUpdater?.Invoke(_staticFlamePositions);
    }
}
