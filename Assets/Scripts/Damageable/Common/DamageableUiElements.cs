using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DamageableUiElements
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _hpBar;
    [SerializeField] private TextMeshProUGUI _hpText;
    public GameObject MainGameObject => _nameText.gameObject;


    public TextMeshProUGUI NameText => _nameText;
    public Image HpBar => _hpBar;
    public TextMeshProUGUI HpText => _hpText;

    public void Activate()
    {
        MainGameObject.SetActive(true);
    }

    public void Deactivate()
    {
        MainGameObject.SetActive(false);
    }
}
