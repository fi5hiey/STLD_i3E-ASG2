
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

public class Interactor : MonoBehaviour
{
    /// <summary>
    /// Interact Range
    /// </summary>
    public float range;

    private TextMeshProUGUI interacttext;
    private StarterAssetsInputs input;
    private GameManager gameManager;

    private void Awake()
    {
        interacttext = GameObject.Find("interacttext").GetComponent<TextMeshProUGUI>();
        input = GameObject.Find("PlayerCapsule").GetComponent<StarterAssetsInputs>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        interacttext.enabled = false;
    }

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

