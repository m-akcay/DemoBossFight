using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFX : MonoBehaviour
{
    private Vector3 _defaultCursorScale;
    private Transform _cursorTransform;
    private Material _cursorMat;
    private Color _cursorCurrentColor;
    private bool _isCursorOnDamageable = false;
    [SerializeField] private float _cursorSizeMultiplier = 1.5f;
    [SerializeField] private float _defaultCursorColorMultiplier = 1.0f;
    [SerializeField] private float _focusedCursorColorMultiplier = 2.0f;

    private void OnEnable()
    {
        FindObjectOfType<CursorCollider>().CursorOnDamageable += CursorFocusEffect;
        FindObjectOfType<WeaponManager>().OnWeaponColorChange += UpdateCursorColor;
    }

    private void OnDisable()
    {
        var cursorCollider = FindObjectOfType<CursorCollider>();
        if (cursorCollider != null)
        {
            cursorCollider.CursorOnDamageable += CursorFocusEffect;

        }

        var weaponManager = FindObjectOfType<WeaponManager>();
        if (weaponManager != null)
        {
            weaponManager.OnWeaponColorChange -= UpdateCursorColor;
        }
    }

    private void Start()
    {
        var cursorGo = GameObject.FindGameObjectWithTag("Cursor");
        _cursorTransform = cursorGo.transform;
        _cursorMat = cursorGo.GetComponent<MeshRenderer>().material;
        _defaultCursorScale = _cursorTransform.localScale;
    }

    private void CursorFocusEffect(bool focus)
    {
        if (focus)
        {
            _isCursorOnDamageable = true;
            _cursorTransform.localScale = _defaultCursorScale * _cursorSizeMultiplier;
            _cursorMat.SetColor("_Color", _cursorCurrentColor * _focusedCursorColorMultiplier);
        }
        else
        {
            _isCursorOnDamageable = false;
            _cursorTransform.localScale = _defaultCursorScale;
            _cursorMat.SetColor("_Color", _cursorCurrentColor * _defaultCursorColorMultiplier);
        }
    }

    private void UpdateCursorColor(Color color)
    {
        _cursorCurrentColor = color;
        float cursorColorMultiplier = _isCursorOnDamageable ? _defaultCursorColorMultiplier : _focusedCursorColorMultiplier;
        _cursorMat.SetColor("_Color", _cursorCurrentColor * cursorColorMultiplier);
    }

    private void OnDestroy()
    {
        Destroy(_cursorMat);
    }
}
