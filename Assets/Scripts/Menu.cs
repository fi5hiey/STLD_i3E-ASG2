/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: Menu Behaviours and Functions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;

public class Menu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject check;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (check.activeInHierarchy)
        {
            check.SetActive(false);
        } else
        {
            check.SetActive(true);
        }
    }
}
