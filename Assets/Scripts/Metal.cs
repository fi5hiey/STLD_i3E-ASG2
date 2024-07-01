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

public class Metal : Collectible
{
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
