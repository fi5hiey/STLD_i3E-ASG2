/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: SceneChangeAreas
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using StarterAssets;
using TMPro;

/// <summary>
/// This class represents an area that allows the player to change scenes.
/// Inherits from the Collectible class and overrides the Interact method.
/// </summary>
public class SceneChangeArea : Collectible
{
    /// <summary>
    /// The index of the scene to change to when the player interacts.
    /// </summary>
    public int changeSceneTo;

    /// <summary>
    /// Handles the interaction with the scene change area.
    /// Displays an interaction prompt and changes the scene when the player interacts.
    /// </summary>
    /// <param name="gameManager">The GameManager instance to handle scene changes.</param>
    /// <param name="interacttext">The UI text element to display interaction prompts.</param>
    /// <param name="input">The player input handler.</param>
    public override void Interact(GameManager gameManager, TextMeshProUGUI interacttext, StarterAssetsInputs input)
    {
        base.Interact(gameManager, interacttext, input);
        interacttext.text = "[E] Enter";
        if (input.interact)
        {
            input.interact = false;
            gameManager.ChangeScene(changeSceneTo);
        }
    }
}
