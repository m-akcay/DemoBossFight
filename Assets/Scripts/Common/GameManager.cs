using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<DamageableObject> _damageables;
    [SerializeField] private float _totalScore;
    [SerializeField] private bool _gameOver;

    protected override void Awake()
    {
        base.Awake();
        _damageables = new();
        Cursor.visible = false;
    }
    public void AddDamageable(DamageableObject damageable)
    {
        _damageables.Add(damageable);
        damageable.OnHpChange += UiManager.Instance.UpdateDamageableHp;
    }

    public void RemoveDamageable(DamageableObject damageable)
    {
        _totalScore += GetScoreForDamageable(damageable);
        _damageables.Remove(damageable);
        damageable.OnHpChange -= UiManager.Instance.UpdateDamageableHp;
        if (_damageables.Count == 0)
        {
            _gameOver = true;
        }
    }

    private float GetScoreForDamageable(DamageableObject damageable)
    {
        float score = 0;

        foreach (var ammoType_damage in damageable.DamageInfo.DamageByWeapons)
        {
            score += ammoType_damage.Value.TotalDps;
        }

        return score;
    }
}
