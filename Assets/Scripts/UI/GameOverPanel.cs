using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] GameObject panel;

    protected void Update()
    {
        if (playerHealth.isDead)
        {
            StartCoroutine(ONGameOverPanel());
        }
    }

    protected IEnumerator ONGameOverPanel()
    {
        yield return new WaitForSeconds(2);
        panel.SetActive(true);
    }
}

