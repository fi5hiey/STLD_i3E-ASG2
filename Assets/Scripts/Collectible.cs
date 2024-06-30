/*
 * Author: Lim Wee Han
 * Date Created: 26/06/2024
 * Description: Collectible Parent Class
 */

using UnityEngine;
using TMPro;
using StarterAssets;
using UnityEngine.Windows;

public class Collectible : MonoBehaviour
{
    public virtual void Interact(GameManager gameManager, TextMeshProUGUI interacttext, StarterAssetsInputs input)
    {
        interacttext.enabled = true;
    }
}