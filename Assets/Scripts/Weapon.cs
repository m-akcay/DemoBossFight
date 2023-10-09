using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour
{
    [SerializeField] private VisualEffect _vfx;
    private bool _vfxPlaying = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Y) && !_vfxPlaying)
        {
            _vfx.Play();
            _vfxPlaying = true;
        }

        if (Input.GetKeyUp(KeyCode.Y))
        {
            _vfx.Stop();
            _vfxPlaying = false;
        }
    }
}
