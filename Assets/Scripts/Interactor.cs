/*
 * Author: Lim Wee Han
 * Date: 26/06/2024
 * Description: Interactions
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using StarterAssets;

/// <summary>
/// This class handles the player's interactions with objects in the game world.
/// </summary>
public class Interactor : MonoBehaviour
{
    /// <summary>
    /// The range within which the player can interact with objects.
    /// </summary>
    public float range;

    /// <summary>
    /// The UI text element that displays interaction prompts.
    /// </summary>
    public TextMeshProUGUI interacttext;

    /// <summary>
    /// The player's input handler.
    /// </summary>
    public StarterAssetsInputs input;

    /// <summary>
    /// The GameManager instance to handle game state updates.
    /// </summary>
    public GameManager gameManager;

    /// <summary>
    /// Initializes the Interactor by disabling the interaction text at the start.
    /// </summary>
    private void Awake()
    {
        interacttext.enabled = false;
    }

    /// <summary>
    /// Checks for interactive objects in front of the player and handles interactions.
    /// </summary>
    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, range))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out Collectible collectible))
            {
                collectible.Interact(gameManager, interacttext, input);
            }
            else
            {
                interacttext.enabled = false;
            }
        }
        else
        {
            interacttext.enabled = false;
        }
    }
}
