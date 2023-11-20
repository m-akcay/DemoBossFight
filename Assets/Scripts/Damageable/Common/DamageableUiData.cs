using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableUiData
{
    public string Name { get; set; }
    public float TotalHp { get; set; }
    public float CurrentHp { get; set; }

    public DamageableUiData(string name, float totalHp, float currentHp)
        => (Name, TotalHp, CurrentHp) = (name, totalHp, currentHp);
        
}
