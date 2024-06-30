/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: Checkpoint System
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager gameManager;

    private void Awake() {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            Debug.Log("Checkpoint Set at " + transform.position);
            gameManager.spawnCoords = transform.position;
        }
    }
}
