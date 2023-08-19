using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] protected Image healthUI;

    public void DisplayHP(float value)
    {
        value /= 100f;

        if (value <= 0) value = 0;
        healthUI.fillAmount = value;
    }

    public void DisplayEnemyHP(float value)
    {
        value /= 100f;

        if (value <= 0) value = 0;
        healthUI.fillAmount = value;
    }
}