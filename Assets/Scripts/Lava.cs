/*
 * Author: Lim Wee Han
 * Date Created: 27/06/2024
 * Description: Lava Damage Script
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private GameManager gameManager;
    public int damage = 4;
    private bool canDamage = true;
    private float cd = 1f;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && canDamage)
        {
            canDamage = false;
            gameManager.PlayerDamage(damage);
            Invoke(nameof(ResetDamage), cd);
        }
    }

    private void ResetDamage()
    {
        canDamage = true;
    }
}
