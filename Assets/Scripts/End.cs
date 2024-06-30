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

public class End : Collectible
{
    public override void Interact(GameManager gameManager, TextMeshProUGUI interacttext, StarterAssetsInputs input)
    {
        base.Interact(gameManager, interacttext, input);
        if (gameManager.metalsObtained >= 4)
        {
            interacttext.text = "Take Off";
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
