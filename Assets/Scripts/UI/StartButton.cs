using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    [SerializeField] protected Button startButton;

    public void TaskOnClick()
    {
        StartCoroutine(ButtonClicked());
    }

    public IEnumerator ButtonClicked()
    {
        //Audio.Instance.AudioSource.PlayOneShot(Resources.Load("Audio/ButtonPress") as AudioClip);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadSceneAsync(1);
    }
}
