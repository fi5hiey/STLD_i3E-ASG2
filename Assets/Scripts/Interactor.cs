
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

    public TextMeshProUGUI interacttext;
    public StarterAssetsInputs input;
    public GameManager gameManager;

    private void Awake()
    {
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

