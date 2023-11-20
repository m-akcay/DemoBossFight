using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : Singleton<UiManager>
{
    [SerializeField] private DamageableUiElements _mobUi;
    private bool _shouldResetMobUi = false;
    private void Start()
    {
        _mobUi.Deactivate();
        FindObjectOfType<CursorCollider>().Init();
        StartCoroutine(MobUiRefresher());
    }

    public void UpdateDamageableHp(DamageableUiData _mobUiData)
    {
        _shouldResetMobUi = false;

        if (!_mobUi.MainGameObject.activeInHierarchy)
        {
            _mobUi.MainGameObject.SetActive(true);
        }
        var hpBar = _mobUi.HpBar;
        float currentHp = _mobUiData.CurrentHp;
        float totalHp = _mobUiData.TotalHp;

        _mobUi.NameText.text = _mobUiData.Name;
        hpBar.fillAmount =  currentHp / totalHp;
        _mobUi.HpText.text = $"{currentHp:N0} / {totalHp:N0}";
    }

    private IEnumerator MobUiRefresher()
    {
        while (true)
        {
            _shouldResetMobUi = true;
            yield return new WaitForSeconds(10.0f);
            if (_shouldResetMobUi)
            {
                _mobUi.Deactivate();
            }
        }
    }
}
