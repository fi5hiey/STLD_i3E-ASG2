/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: SceneChangeAreas
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using StarterAssets;
using TMPro;

public class SceneChangeArea : Collectible
{
    public int changeSceneTo;
    public override void Interact(GameManager gameManager, TextMeshProUGUI interacttext, StarterAssetsInputs input)
    {
        base.Interact(gameManager, interacttext, input);
        interacttext.text = "[E] Enter";
        if (input.interact)
        {
            input.interact = false;
            gameManager.ChangeScene(changeSceneTo);
        }
    }
}
