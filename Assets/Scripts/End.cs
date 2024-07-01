/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: EndArea
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using StarterAssets;
using TMPro;

/// <summary>
/// Represents the end area where the player can finish the game if they have collected enough metals.
/// Inherits from the Collectible class and overrides the Interact method to handle endgame conditions.
/// </summary>
public class End : Collectible
{
    /// <summary>
    /// Handles the interaction with the end area.
    /// Displays a message based on whether the player has collected enough metals and triggers the win state if conditions are met.
    /// </summary>
    /// <param name="gameManager">The GameManager instance to handle game state updates.</param>
    /// <param name="interacttext">The UI text element to display interaction prompts.</param>
    /// <param name="input">The player input handler.</param>
    public override void Interact(GameManager gameManager, TextMeshProUGUI interacttext, StarterAssetsInputs input)
    {
        base.Interact(gameManager, interacttext, input);

        // Check if the player has collected at least 4 metals.
        if (gameManager.metalsObtained >= 4)
        {
            interacttext.text = "Take Off";
            // Trigger the win condition if the player interacts.
            if (input.interact)
            {
                input.interact = false;
                gameManager.Win();
            }
        }
        else
        {
            interacttext.text = "Ship is unusable";
        }
    }
}
