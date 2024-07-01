/*
 * Author: Lim Wee Han
 * Date Created: 26/06/2024
 * Description: Collectible Parent Class
 */

using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.Windows;

/// <summary>
/// The base class for all collectible items in the game.
/// Provides a virtual Interact method for derived classes to override and implement specific interactions.
/// </summary>
public class Collectible : MonoBehaviour
{
    /// <summary>
    /// Handles the interaction with the collectible item.
    /// This method can be overridden by derived classes to provide specific behavior for different collectibles.
    /// By default, it enables the interaction text.
    /// </summary>
    /// <param name="gameManager">The GameManager instance to handle game state updates.</param>
    /// <param name="interacttext">The UI text element to display interaction prompts.</param>
    /// <param name="input">The player input handler.</param>
    public virtual void Interact(GameManager gameManager, TextMeshProUGUI interacttext, StarterAssetsInputs input)
    {
        interacttext.enabled = true;
    }
}
