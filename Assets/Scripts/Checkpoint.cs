/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: Checkpoint System
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is responsible for managing checkpoints in the game.
/// When the player triggers a checkpoint, their spawn coordinates are updated.
/// </summary>
public class Checkpoint : MonoBehaviour
{
    private GameManager gameManager;

    /// <summary>
    /// Called when the script instance is being loaded.
    /// Finds and caches the GameManager component.
    /// </summary>
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Called when another collider enters the trigger collider attached to this object.
    /// Checks if the collider belongs to the player and updates the spawn coordinates.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint Set at " + transform.position);
            gameManager.spawnCoords = transform.position;
        }
    }
}
