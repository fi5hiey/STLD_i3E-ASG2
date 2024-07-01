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

/// <summary>
/// This class handles menu behaviors and functions, including scene transitions, volume control, and fullscreen settings.
/// </summary>
public class Menu : MonoBehaviour
{
    /// <summary>
    /// The AudioMixer used to control the game's audio volume.
    /// </summary>
    public AudioMixer audioMixer;

    /// <summary>
    /// The GameObject representing the fullscreen checkbox in the menu.
    /// </summary>
    public GameObject check;

    /// <summary>
    /// Loads the main game scene.
    /// </summary>
    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Quits the application.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Sets the audio volume for the game.
    /// </summary>
    /// <param name="volume">The desired audio volume level, typically from -80 to 20.</param>
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }

    /// <summary>
    /// Sets the fullscreen mode for the game and updates the fullscreen checkbox state.
    /// </summary>
    /// <param name="isFullscreen">A boolean indicating whether the game should be in fullscreen mode.</param>
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        if (check.activeInHierarchy)
        {
            check.SetActive(false);
        }
        else
        {
            check.SetActive(true);
        }
    }
}
