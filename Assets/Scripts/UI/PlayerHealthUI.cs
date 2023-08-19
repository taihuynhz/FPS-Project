using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthUI : HealthUI
{
    [SerializeField] protected Health health;

    protected void Update()
    {
        UpdateHPBar();
    }

    protected void UpdateHPBar()
    {
        DisplayHP(health.hP);
    }
}
