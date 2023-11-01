using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] LayerMask _mouseTargetLayers;
    private Camera _mainCam;
    private Transform _mainCamTransform;
    private CharacterController _characterController;
    private WeaponManager _weaponManager;
    void Start()
    {
        _mainCam = Camera.main;
        _mainCamTransform = Camera.main.transform;
        _characterController = GetComponent<CharacterController>();
        _weaponManager = GetComponent<WeaponManager>();
    }

    void Update()
    {
        HandleMovementKeys();
        HandleMouse();
        HandleWeaponSelection();
    }

    private void HandleMovementKeys()
    {
        var movementVector = new Vector3();

        if (Input.GetKey(KeyCode.W))
        {
            var camFwd = _mainCamTransform.forward;
            camFwd.y = 0;
            movementVector += camFwd;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            var camFwd = -_mainCamTransform.forward;
            camFwd.y = 0;
            movementVector += camFwd;
        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 left = Vector3.Cross(_mainCamTransform.forward, Vector3.up);
            movementVector += left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 right = Vector3.Cross(Vector3.up, _mainCamTransform.forward);
            movementVector += right;
        }
        _characterController.Move(movementVector);
    }

    private void HandleMouse()
    {
        var mousePos = Input.mousePosition;
        var ray = _mainCam.ScreenPointToRay(mousePos);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, _mouseTargetLayers))
        {
            _characterController.LookAt(hitInfo.point);
        }

        if (Input.GetMouseButton(0))
        {
            _weaponManager.Shoot();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _weaponManager.StopShooting();
        }
    }

    private void HandleWeaponSelection()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _weaponManager.SelectWeapon(Weapon.AmmoType.FLAME);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _weaponManager.SelectWeapon(Weapon.AmmoType.FROST);
        }
    }
}
