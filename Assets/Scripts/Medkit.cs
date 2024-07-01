/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: Medkit to Heal Player
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using StarterAssets;
using TMPro;

/// <summary>
/// This class represents a medkit that can be collected to heal the player.
/// Inherits from the Collectible class and overrides the Interact method.
/// </summary>
public class Medkit : Collectible
{
    /// <summary>
    /// The amount of health restored when the medkit is picked up.
    /// </summary>
    public int heal = 15;

    /// <summary>
    /// Handles the interaction with the medkit.
    /// Displays an interaction prompt and heals the player when the medkit is picked up.
    /// </summary>
    /// <param name="gameManager">The GameManager instance to handle healing the player.</param>
    /// <param name="interacttext">The UI text element to display interaction prompts.</param>
    /// <param name="input">The player input handler.</param>
    public override void Interact(GameManager gameManager, TextMeshProUGUI interacttext, StarterAssetsInputs input)
    {
        base.Interact(gameManager, interacttext, input);
        interacttext.text = "Press [E] to Pickup " + gameObject.name;

        if (input.interact)
        {
            input.interact = false;
            gameManager.Heal(heal);
            Destroy(gameObject);
        }
    }
}
