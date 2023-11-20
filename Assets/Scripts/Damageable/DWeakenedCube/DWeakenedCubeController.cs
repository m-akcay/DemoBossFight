using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DWeakenedCubeController : MonoBehaviour
{
    [SerializeField] private DWeakenedCubeSo _weakenedCubeSo;
    private DWeakenedCube _weakenedCube;

    void Start()
    {
        _weakenedCube = GetComponent<DWeakenedCube>();
        _weakenedCube.Init(_weakenedCubeSo);
    }
    
}
