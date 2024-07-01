/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: Game Objective Metals
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using StarterAssets;
using TMPro;

/// <summary>
/// This class represents a collectible metal object that the player can pick up.
/// Inherits from the Collectible class and overrides the Interact method.
/// </summary>
public class Metal : Collectible
{
    /// <summary>
    /// Handles the interaction with the metal object.
    /// Displays an interaction prompt and updates the game state when the player picks up the metal.
    /// </summary>
    /// <param name="gameManager">The GameManager instance to handle game state updates.</param>
    /// <param name="interacttext">The UI text element to display interaction prompts.</param>
    /// <param name="input">The player input handler.</param>
    public override void Interact(GameManager gameManager, TextMeshProUGUI interacttext, StarterAssetsInputs input)
    {
        base.Interact(gameManager, interacttext, input);
        interacttext.enabled = true;
        interacttext.text = "Press [E] to Pickup " + gameObject.name;

        if (input.interact)
        {
            input.interact = false;
            gameManager.metalsObtained += 1;
            Destroy(gameObject);
        }
    }
}
