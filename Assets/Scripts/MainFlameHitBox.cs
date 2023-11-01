using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainFlameHitBox : MonoBehaviour
{
    private FlameHitBox[] _flameHitBoxes;

    // Start is called before the first frame update
    void Start()
    {
        _flameHitBoxes = GetComponentsInChildren<FlameHitBox>();
    }

}