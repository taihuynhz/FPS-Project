using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : HealthUI
{
    protected static EnemyHealthUI instance;
    public static EnemyHealthUI Instance => instance;

    [SerializeField] protected Image healthBorderUI;
    [SerializeField] protected Transform monsterHolder;

    protected void Awake() => CreateSingleton();

    public void HideEnemyUI()
    {
        Color alpha = Color.white;
        alpha.a = 0f;

        healthUI.color = alpha;
        healthBorderUI.color = alpha;
    }

    public void ShowEnemyUI()
    {
        Color alpha = Color.white;
        alpha.a = 255f;

        healthUI.color = alpha;
        healthBorderUI.color = alpha;
    }

    public IEnumerator DeactivateUIAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        HideEnemyUI();
    }
    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
