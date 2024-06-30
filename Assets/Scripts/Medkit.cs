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

public class Medkit : Collectible
{
    public int heal = 15;
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
